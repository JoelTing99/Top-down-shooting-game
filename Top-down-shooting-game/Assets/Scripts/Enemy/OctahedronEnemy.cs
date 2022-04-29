using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

public class OctahedronEnemy : MonoBehaviour
{
    private HealthSystem HealthSystem;
    private GameManager GameManager;
    private EnemyHealthBar EnemyHealthBar;
    private Animator Animator;
    private LineRenderer Line;
    private NavMeshAgent Agent;
    private LevelSystem LevelSystem;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private Transform FirePoint;
    [SerializeField] private GameObject Destroyed;
    [SerializeField] private VisualEffect AttackEffect;
    [SerializeField] private VisualEffect DeadEffect;
    [SerializeField] private LayerMask CollidableLayer;
    private int BulletCount;
    private GameObject bullet;
    private void Start()
    {
        AttackEffect.Stop();
        GameManager = FindObjectOfType<GameManager>();
        HealthSystem = new HealthSystem(GameManager.GetOctahedronHP());
        LevelSystem = GameManager.GetLevelSystem();
        EnemyHealthBar = transform.Find("HealthBar").GetComponent<EnemyHealthBar>();
        EnemyHealthBar.SetHealthSystem(HealthSystem);
        Line = GetComponent<LineRenderer>();
        Animator = GetComponent<Animator>();
        Agent = GetComponent<NavMeshAgent>();
        Agent.speed = GameManager.GetOctahedronSpeed();
        transform.Find("HealthBar").gameObject.SetActive(false);
    }
    private void Update()
    {
        AttackAnimation();
        Dead();
    }
    private void AttackAnimation()
    {
        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit HIT, 5f) && !HIT.collider.CompareTag("Wall"))
        {
            RaycastHit[] Hit = Physics.RaycastAll(transform.position, transform.forward, 5f);
            foreach (var hit in Hit)
            {
                if (hit.collider.CompareTag("Player"))
                {
                    Animator.SetBool("IsAttack", true);
                    DrawProjection();
                }
            }
        }
        else
        {
            Animator.SetBool("IsAttack", false);
            Line.positionCount = 0;
            AttackEffect.SendEvent("StartCharge");
        }
    }
    private void Charging()
    {
        if (BulletCount < 1)
        {
            bullet = Instantiate(Bullet, FirePoint.position, FirePoint.rotation, transform);
            AttackEffect.SendEvent("StartCharge");
            if (bullet != null)
            {
                bullet.GetComponent<EnemyBullet>().Charging = true;
                BulletCount++;
                Destroy(bullet, 5f);
            }
        }
        else
        {
            BulletCount = 0;
        }
    }
    private void FireAction()
    {
        AttackEffect.SendEvent("StopCharge");
        if (bullet != null)
        {
            bullet.GetComponent<EnemyBullet>().Fire();
            AttackEffect.SendEvent("Fire");
            BulletCount = 0;
        }
    }
    private void Dead()
    {
        if (HealthSystem.GetHealth() <= 0)
        {
            GameObject destory = Instantiate(Destroyed, transform.position, transform.rotation);
            VisualEffect deadeffect = Instantiate(DeadEffect, transform.position, transform.rotation);
            Collider[] Collider = Physics.OverlapSphere(transform.position, 2f);
            for (int i = 0; i < Random.Range(3, 5); i++)
            {
                Vector3 RandPos = new Vector3(Random.Range(-1f, 1f), Random.Range(0, 1f), Random.Range(-1f, 1f));
                Instantiate(GameManager.GetCoinsGameObject(), transform.position + RandPos, Quaternion.identity);
            }
            foreach (var collider in Collider)
            {
                if (collider.GetComponent<Rigidbody>() != null)
                {
                    collider.GetComponent<Rigidbody>().AddExplosionForce(400, transform.position, 1);
                }
            }
            LevelSystem.ObtainExp(GameManager.GetOctaedronExpAmount());
            Textpopup.Create(FindObjectOfType<Player>().transform.position + new Vector3(0, 2, 0), GameManager.GetOctaedronExpAmount(), Color.green);
            Destroy(deadeffect.gameObject, 3);
            Destroy(destory, 5);
            Destroy(gameObject);
        }
    }
    private void DrawProjection()
    {
        List<Vector3> points = new List<Vector3>();
        Vector3 StartingPosition = FirePoint.position;
        Vector3 StartingVelosity = transform.forward;
        for (float i = 0; i < 5; i++)
        {
            Vector3 NewPoint = StartingPosition + i * StartingVelosity;
            points.Add(NewPoint);
            if (Physics.OverlapSphere(NewPoint, 0.3f, CollidableLayer).Length > 0)
            {
                Line.positionCount = points.Count;
                break;
            }
        }
        Line.SetPositions(points.ToArray());
    }
    public void TakeDamage(float Damage)
    {
        transform.Find("HealthBar").gameObject.SetActive(true);
        HealthSystem.Damage(Damage);
    }
    private void OnTriggerEnter(Collider other)
    {

    }
}

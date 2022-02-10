using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OctahedronEnemy : MonoBehaviour
{
    private HealthSystem HealthSystem;
    private GameManager GameManager;
    private EnemyHealthBar EnemyHealthBar;
    private Animator Animator;
    private LineRenderer Line;
    private NavMeshAgent Agent;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private Transform FirePoint;
    [SerializeField] private GameObject Destroyed;
    private int BulletCount;
    private GameObject bullet;
    private void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
        HealthSystem = new HealthSystem(GameManager.GetOctahedronHP());
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
            RaycastHit[] Hit = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), 5f);
            foreach (var hit in Hit)
            {
                Animator.SetBool("IsAttack", true);
            }
        }
        else
        {
            Animator.SetBool("IsAttack", false);
        }
    }
    private void Charging()
    {
        if (BulletCount < 1 && bullet != null)
        {
            bullet = Instantiate(Bullet, FirePoint.position, transform.rotation, transform);
            StartCoroutine(bullet.GetComponent<EnemyBullet>().Charge());
            BulletCount++;
            Destroy(bullet, 5f);
        }
        else
        {
            BulletCount = 0;
        }
    }
    private void FireAction()
    {
        if (bullet != null)
        {
            bullet.GetComponent<EnemyBullet>().Fire();
            BulletCount = 0;
            bullet = null;
        }
    }
    private void Dead()
    {
        if (HealthSystem.GetHealth() <= 0)
        {
            GameObject destory = Instantiate(Destroyed, transform.position, transform.rotation);
            Collider[] Collider = Physics.OverlapSphere(transform.position, 2f);
            for (int i = 0; i < Random.Range(3, 5); i++)
            {
                Instantiate(GameManager.GetCoinsGameObject(), transform.position, Quaternion.identity);
            }
            foreach (var collider in Collider)
            {
                if (collider.GetComponent<Rigidbody>() != null)
                {
                    collider.GetComponent<Rigidbody>().AddExplosionForce(400, transform.position, 1);
                }
            }
            Destroy(gameObject);
            Destroy(destory, 5);
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
            if (Physics.OverlapSphere(NewPoint, 0.3f, 6).Length > 0)
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctahedronEnemy : MonoBehaviour
{
    private HealthSystem HealthSystem;
    private GameManager GameManager;
    private EnemyHealthBar EnemyHealthBar;
    private Animator Animator;
    [SerializeField]
    private GameObject Bullet;
    [SerializeField]
    private Transform FirePoint;
    [SerializeField]
    private bool Fire;
    [SerializeField]
    private bool Charging;
    [SerializeField]
    private GameObject DeStroy;
    private int BulletCount;
    private GameObject bullet;
    private void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
        HealthSystem = new HealthSystem(GameManager.GetOctahedronHP());
        EnemyHealthBar = transform.Find("HealthBar").GetComponent<EnemyHealthBar>();
        EnemyHealthBar.SetHealthSystem(HealthSystem);
        Animator = GetComponent<Animator>();
        transform.Find("HealthBar").gameObject.SetActive(false);
    }


    private void Update()
    {
        Attack();
        FireAction();
        Dead();
    }
    private void Attack()
    {
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit HIT, 5f) && !HIT.collider.CompareTag("Wall"))
        {
            bool HitPlayer = false;
            RaycastHit[] Hit = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), 5f);
            foreach (var hit in Hit)
            {
                HitPlayer = true;
            }
            if (HitPlayer)
            {
                Animator.SetBool("IsAttack", true);
                if (BulletCount < 1 && Charging)
                {
                    bullet = Instantiate(Bullet, FirePoint.position, transform.rotation, transform);
                    BulletCount++;
                }
            }
            else
            {
                Animator.SetBool("IsAttack", false);
                Destroy(bullet, 5f);
                BulletCount = 0;
            }
        }
        else
        {
            Animator.SetBool("IsAttack", false);
            Destroy(bullet, 5f);
            BulletCount = 0;
        }
    }
    private void FireAction()
    {
        if (bullet != null)
        {
            if (Charging)
            {
                bullet.GetComponent<EnemyBullet>().IsCharging = true;
            }
            else
            {
                bullet.GetComponent<EnemyBullet>().IsCharging = false;
            }
            if (Fire)
            {
                bullet.GetComponent<EnemyBullet>().Fire();
                BulletCount = 0;
            }
        }
    }
    private void Dead()
    {
        if (HealthSystem.GetHealth() <= 0)
        {
            GameObject destory = Instantiate(DeStroy, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(destory, 5);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            transform.Find("HealthBar").gameObject.SetActive(true);
            HealthSystem.Damage(GameManager.GetPlayerDamage());
        }
    }
}

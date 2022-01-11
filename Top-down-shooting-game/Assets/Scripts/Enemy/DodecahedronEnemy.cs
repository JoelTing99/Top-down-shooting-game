using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DodecahedronEnemy : MonoBehaviour
{
    private HealthSystem HealthSystem;
    private HealthSystem PlayerHealthSystem;
    private GameManager GameManager;
    private EnemyHealthBar EnemyHealthBar;
    private Animator Animator;
    private int AttackCount;
    [SerializeField]
    private bool IsAttacking;
    [SerializeField]
    private GameObject Destroyed;
    private void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
        HealthSystem = new HealthSystem(GameManager.GetDodecahedronHP());
        EnemyHealthBar = transform.Find("HealthBar").GetComponent<EnemyHealthBar>();
        EnemyHealthBar.SetHealthSystem(HealthSystem);
        PlayerHealthSystem = GameManager.GetPlayerHealth();
        Animator = GetComponent<Animator>();
        transform.Find("HealthBar").gameObject.SetActive(false);
    }


    private void Update()
    {
        Attack();
        Dead();
    }
    private void Attack()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, 0.7f) && hit.collider.CompareTag("Player"))
        {
            Animator.SetBool("IsAttack", true);
        }
        else
        {
            Animator.SetBool("IsAttack", false);
        }
        if (IsAttacking)
        {
            Collider[] Collide = Physics.OverlapSphere(transform.position, 1.5f);
            foreach (var collide in Collide)
            {
                if (collide.CompareTag("Player") && AttackCount > 0)
                {
                    PlayerHealthSystem.Damage(GameManager.GetDodecahedronDamage());
                    FindObjectOfType<Player>().isStun = true;
                }
            }
            AttackCount--;
        }
        else
        {
            AttackCount = 1;
            FindObjectOfType<Player>().isStun = false;
        }
    }
    private void Dead()
    {
        if(HealthSystem.GetHealth() <= 0)
        {
            GameObject destory = Instantiate(Destroyed, transform.position, transform.rotation);
            Collider[] Collider = Physics.OverlapSphere(transform.position, 2f);
            for (int i = 0; i < Random.Range(4, 8); i++)
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
    public void TakeDamage(float Damage)
    {
        transform.Find("HealthBar").gameObject.SetActive(true);
        HealthSystem.Damage(Damage);
    }
    private void OnTriggerEnter(Collider other)
    {

    }
}

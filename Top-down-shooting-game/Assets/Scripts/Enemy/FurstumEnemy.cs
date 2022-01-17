using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FurstumEnemy : MonoBehaviour
{
    private HealthSystem HealthSystem;
    private GameManager GameManager;
    private EnemyHealthBar EnemyHealthBar;
    private Animator Animator;
    private NavMeshAgent Agent;
    private int AttackCount;
    [SerializeField] private bool IsAttacking;
    [SerializeField] private GameObject Destroyed;
    private void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
        HealthSystem = new HealthSystem(GameManager.GetFurstumHP());
 
        EnemyHealthBar = transform.Find("HealthBar").GetComponent<EnemyHealthBar>();
        EnemyHealthBar.SetHealthSystem(HealthSystem);
        Animator = GetComponent<Animator>();
        Agent = GetComponent<NavMeshAgent>();
        Agent.speed = GameManager.GetFurstumSpeed();
        transform.Find("HealthBar").gameObject.SetActive(false);
    }


    private void Update()
    {
        Attack();
        Dead();
    }
    private void Attack()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, 1f) && hit.collider.CompareTag("Player"))
        {
            Animator.SetBool("IsAttack", true);
        }
        else
        {
            Animator.SetBool("IsAttack", false);
        }
        if (IsAttacking)
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit Hit, 1f) && hit.collider.CompareTag("Player") && AttackCount > 0)
            {
                GameManager.AttackPlayer(GameManager.GetFurstumDamage());
                AttackCount--;
            }
        }
        else
        {
            AttackCount = 1;
        }
    }
    private void Dead()
    {
        if(HealthSystem.GetHealth() <= 0)
        {
            GameObject destory = Instantiate(Destroyed, transform.position, transform.rotation);
            Collider[] Collider = Physics.OverlapSphere(transform.position, 2f);
            for (int i = 0; i < Random.Range(1, 4); i++)
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

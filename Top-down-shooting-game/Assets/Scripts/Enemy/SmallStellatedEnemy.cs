using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmallStellatedEnemy : MonoBehaviour
{
    private HealthSystem HealthSystem;
    private GameManager GameManager;
    private EnemyHealthBar EnemyHealthBar;
    private Animator Animator;
    private NavMeshAgent NavMeshAgent;
    [SerializeField]
    private bool IsAttacking;
    [SerializeField]
    private GameObject Destroyed;
    private void Start()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        GameManager = FindObjectOfType<GameManager>();
        HealthSystem = new HealthSystem(GameManager.GetSmallStellatedHP());
        EnemyHealthBar = transform.Find("HealthBar").GetComponent<EnemyHealthBar>();
        EnemyHealthBar.SetHealthSystem(HealthSystem);
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
            Instantiate(Destroyed, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
    private void Dead()
    {
        if (HealthSystem.GetHealth() <= 0)
        {
            for (int i = 0; i < Random.Range(6, 10); i++)
            {
                Instantiate(GameManager.GetCoinsGameObject(), transform.position, Quaternion.identity);
            }
            GameObject destory = Instantiate(Destroyed, transform.position, transform.rotation);
            Destroy(gameObject);
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

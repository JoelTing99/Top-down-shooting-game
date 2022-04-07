using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class DodecahedronEnemy : MonoBehaviour
{
    private HealthSystem HealthSystem;
    private GameManager GameManager;
    private EnemyHealthBar EnemyHealthBar;
    private Animator Animator;
    private NavMeshAgent Agent;
    private LevelSystem LevelSystem;
    private int AttackCount;
    [SerializeField] private GameObject Destroyed;
    private void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
        HealthSystem = new HealthSystem(GameManager.GetDodecahedronHP());
        LevelSystem = GameManager.GetLevelSystem();
        EnemyHealthBar = transform.Find("HealthBar").GetComponent<EnemyHealthBar>();
        EnemyHealthBar.SetHealthSystem(HealthSystem);
        Animator = GetComponent<Animator>();
        Agent = GetComponent<NavMeshAgent>();
        Agent.speed = GameManager.GetDodecahedronSpeed();
        transform.Find("HealthBar").gameObject.SetActive(false);
    }


    private void Update()
    {
        AttackAnimation();
        Dead();
    }
    private void AttackAnimation()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, 2f) && hit.collider.CompareTag("Player"))
        {
            Animator.SetBool("IsAttack", true);
        }
        else
        {
            Animator.SetBool("IsAttack", false);
        }
    }
    private void StartAttack()
    {
        Collider[] Collide = Physics.OverlapSphere(transform.position, 2f);
        foreach (var collide in Collide)
        {
            if (collide.CompareTag("Player") && AttackCount > 0)
            {
                GameManager.AttackPlayer(GameManager.GetDodecahedronDamage());
                if (FindObjectOfType<Player>() != null)
                {
                    FindObjectOfType<Player>().isStun = true;
                }
            }
        }
        AttackCount--;
    }
    private void StopAttack()
    {
        AttackCount = 1;
        if (FindObjectOfType<Player>() != null)
        {
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
                    collider.GetComponent<Rigidbody>().AddExplosionForce(400, transform.position, 2);
                }
            }
            LevelSystem.ObtainExp(GameManager.GetDodecahedronExpAmount());
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

public class CubeEnemy : MonoBehaviour
{
    private HealthSystem HealthSystem;
    private EnemyHealthBar EnemyHealthBar;
    private Animator Animator;
    private GameManager GameManager;
    private NavMeshAgent Agent;
    [SerializeField] private GameObject Destroyed;
    [SerializeField] private VisualEffect AttactEffect;
    private void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
        HealthSystem = new HealthSystem(GameManager.GetCubeHP());
        EnemyHealthBar = transform.Find("HealthBar").GetComponent<EnemyHealthBar>();
        EnemyHealthBar.SetHealthSystem(HealthSystem);
        Agent = GetComponent<NavMeshAgent>();
        Animator = GetComponent<Animator>();
        Agent.speed = GameManager.GetCubeSpeed();
        transform.Find("HealthBar").gameObject.SetActive(false);
    }

    private void Update()
    {
        AttackAnimation();
        Dead();
    }
    private void AttackAnimation()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 1.5f) && hit.collider.CompareTag("Player"))
        {
            Animator.SetBool("IsAttack", true);
            if (Physics.SphereCast(transform.position, 1f, transform.forward, out RaycastHit Hit, 1.5f) && Hit.transform.CompareTag("Player"))
            {
                GameManager.AttackPlayer(GameManager.GetCubeDamage());
            }
        }
        else
        {
            Animator.SetBool("IsAttack", false);
        }
    }
    private void StartAttackEffect()
    {
        AttactEffect.SendEvent("Attacking");
    }
    private void StopAttackEffect()
    {
        AttactEffect.SendEvent("StopAttacking");
    }
    private void Dead()
    {
        if (HealthSystem.GetHealth() <= 0)
        {
            GameObject destroy = Instantiate(Destroyed, transform.position, transform.rotation);
            Collider[] Collider = Physics.OverlapSphere(transform.position, 2f);
            for (int i = 0; i < Random.Range(2, 5); i++)
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
            Destroy(gameObject);
            Destroy(destroy, 5);
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

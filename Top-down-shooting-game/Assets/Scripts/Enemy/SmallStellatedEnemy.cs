using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

public class SmallStellatedEnemy : MonoBehaviour
{
    private HealthSystem HealthSystem;
    private GameManager GameManager;
    private EnemyHealthBar EnemyHealthBar;
    private Animator Animator;
    private NavMeshAgent Agent;
    private LevelSystem LevelSystem;
    [SerializeField] private GameObject Destroyed;
    [SerializeField] private VisualEffect AttackEffeck;
    [SerializeField] private VisualEffect WalkEffeck;
    [SerializeField] private VisualEffect DeadEffect;

    private void Start()
    {
        AttackEffeck.Stop();
        GameManager = FindObjectOfType<GameManager>();
        HealthSystem = new HealthSystem(GameManager.GetSmallStellatedHP());
        LevelSystem = GameManager.GetLevelSystem();
        EnemyHealthBar = transform.Find("HealthBar").GetComponent<EnemyHealthBar>();
        EnemyHealthBar.SetHealthSystem(HealthSystem);
        Animator = GetComponent<Animator>();
        Agent = GetComponent<NavMeshAgent>();
        Agent.speed = GameManager.GetSmallStellatedSpeed();
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
            WalkEffeck.Stop();
            AttackEffeck.Play();
        }
        else
        {
            Animator.SetBool("IsAttack", false);
            AttackEffeck.Stop();
            WalkEffeck.Play();
        }
    }
    private void Attack()
    {
        Instantiate(Destroyed, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    private void Dead()
    {
        if (HealthSystem.GetHealth() <= 0)
        {
            VisualEffect deadeffect = Instantiate(DeadEffect, transform.position, transform.rotation);
            for (int i = 0; i < Random.Range(6, 10); i++)
            {
                Instantiate(GameManager.GetCoinsGameObject(), transform.position, Quaternion.identity);
            }
            GameObject destory = Instantiate(Destroyed, transform.position, transform.rotation);
            LevelSystem.ObtainExp(GameManager.GetSmallStellatedExpAmount());
            Destroy(deadeffect, 3);
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

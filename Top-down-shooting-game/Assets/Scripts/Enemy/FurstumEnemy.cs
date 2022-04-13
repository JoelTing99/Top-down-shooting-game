using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

public class FurstumEnemy : MonoBehaviour
{
    private HealthSystem HealthSystem;
    private GameManager GameManager;
    private EnemyHealthBar EnemyHealthBar;
    private Animator Animator;
    private NavMeshAgent Agent;
    private LevelSystem LevelSystem;
    private int AttackCount;
    [SerializeField] private GameObject Destroyed;
    [SerializeField] private VisualEffect AttackEffect;
    [SerializeField] private VisualEffect WalkEffect;
    [SerializeField] private VisualEffect DeadEffect;

    private void Start()
    {
        StopAttackEffect();
        GameManager = FindObjectOfType<GameManager>();
        HealthSystem = new HealthSystem(GameManager.GetFurstumHP());
        LevelSystem = GameManager.GetLevelSystem();
        EnemyHealthBar = transform.Find("HealthBar").GetComponent<EnemyHealthBar>();
        EnemyHealthBar.SetHealthSystem(HealthSystem);
        Animator = GetComponent<Animator>();
        Agent = GetComponent<NavMeshAgent>();
        Agent.speed = GameManager.GetFurstumSpeed();
        transform.Find("HealthBar").gameObject.SetActive(false);
    }


    private void Update()
    {
        AttackAnimation();
        Dead();
    }
    private void AttackAnimation()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 2f) && hit.collider.CompareTag("Player"))
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
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit Hit, 2f) && Hit.collider.CompareTag("Player") && AttackCount > 0)
        {
            GameManager.AttackPlayer(GameManager.GetFurstumDamage());
            AttackCount--;
        }
    }
    private void StopAttack()
    {
        AttackCount = 1;
    }
    private void PlayAttackEffect()
    {
        AttackEffect.Play();
    }
    private void StopAttackEffect()
    {
        AttackEffect.Stop();
    }
    private void PlayWalkEffect_1()
    {
        WalkEffect.transform.localPosition = new Vector3(-0.5f, -1, 0.3f);
        WalkEffect.transform.localRotation = Quaternion.Euler(0, 30, 0);
        WalkEffect.Play();
    }
    private void PlayWalkEffect_2()
    {
        WalkEffect.transform.localPosition = new Vector3(0.5f, -1, 0.3f);
        WalkEffect.transform.localRotation = Quaternion.Euler(0, -30, 0);
        WalkEffect.Play();
    }
    private void Dead()
    {
        if(HealthSystem.GetHealth() <= 0)
        {
            GameObject destory = Instantiate(Destroyed, transform.position, transform.rotation);
            VisualEffect deadeffect = Instantiate(DeadEffect, transform.position, transform.rotation);
            Collider[] Collider = Physics.OverlapSphere(transform.position, 2f);
            for (int i = 0; i < Random.Range(1, 4); i++)
            {
                Instantiate(GameManager.GetCoinsGameObject(), transform.position, Quaternion.identity);
            }
            foreach (var collider in Collider)
            {
                if (collider.GetComponent<Rigidbody>() != null)
                {
                    collider.GetComponent<Rigidbody>().AddExplosionForce(400, transform.position, 2f);
                }
            }
            LevelSystem.ObtainExp(GameManager.GetFurstumExpAmount());
            Destroy(deadeffect, 3);
            Destroy(destory, 5);
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

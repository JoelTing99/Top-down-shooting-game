using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;
public class DodecahedronEnemy : MonoBehaviour
{
    private HealthSystem HealthSystem;
    private GameManager GameManager;
    private EnemyHealthBar EnemyHealthBar;
    private Animator Animator;
    private NavMeshAgent Agent;
    private LevelSystem LevelSystem;
    private int AttackCount;
    private int EffectCount = 0;
    [SerializeField] private GameObject Destroyed;
    [SerializeField] private VisualEffect AttackEffect;
    [SerializeField] private VisualEffect DeadEffect;
    private void Start()
    {
        AttackEffect.Stop();
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
        Collider[] Collide = Physics.OverlapSphere(transform.position, 2.5f);
        foreach (var collide in Collide)
        {
            if (collide.CompareTag("Player") && AttackCount > 0)
            {
                GameManager.AttackPlayer(GameManager.GetDodecahedronDamage());
                if (collide.GetComponent<Player>() != null)
                {
                    collide.GetComponent<Player>().isStun = true;
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
    private void PlayAttackEffect()
    {
        AttackEffect.Play();
    }
    private void SetAttackEffectHeight()
    {
        EffectCount++;
        switch (EffectCount)
        {
            case 1:
                AttackEffect.SetFloat("Height", 1f);
                break;
            case 2:
                AttackEffect.SetFloat("Height", 2f);
                break;
            case 3:
                AttackEffect.SendEvent("Explode");
                EffectCount = 0;
                AttackEffect.Stop();
                break;
        }
    }
    private void Dead()
    {
        if(HealthSystem.GetHealth() <= 0)
        {
            GameObject destory = Instantiate(Destroyed, transform.position, transform.rotation);
            VisualEffect deadeffect = Instantiate(DeadEffect, transform.position, transform.rotation);
            Collider[] Collider = Physics.OverlapSphere(transform.position, 2f);
            for (int i = 0; i < Random.Range(4, 8); i++)
            {
                Vector3 RandPos = new Vector3(Random.Range(-1f, 1f), Random.Range(0f, 1f), Random.Range(-1f, 1f));
                Instantiate(GameManager.GetCoinsGameObject(), transform.position + RandPos, Quaternion.identity);
            }
            foreach (var collider in Collider)
            {
                if (collider.GetComponent<Rigidbody>() != null)
                {
                    collider.GetComponent<Rigidbody>().AddExplosionForce(400, transform.position, 2);
                }
            }
            LevelSystem.ObtainExp(GameManager.GetDodecahedronExpAmount());
            Textpopup.Create(FindObjectOfType<Player>().transform.position + new Vector3(0, 2, 0), GameManager.GetDodecahedronExpAmount(), Color.green);
            Destroy(deadeffect.gameObject, 3);
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

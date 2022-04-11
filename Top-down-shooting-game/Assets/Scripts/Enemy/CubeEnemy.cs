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
    private LevelSystem LevelSystem;
    [SerializeField] private GameObject Destroyed;
    [SerializeField] private VisualEffect AttactEffect;
    [SerializeField] private VisualEffect WalkEffect;
    private void Start()
    {
        AttactEffect.Stop();
        GameManager = FindObjectOfType<GameManager>();
        HealthSystem = new HealthSystem(GameManager.GetCubeHP());
        LevelSystem = GameManager.GetLevelSystem();
        EnemyHealthBar = GetComponentInChildren<EnemyHealthBar>();
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
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 2f) && hit.collider.CompareTag("Player"))
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
        AttactEffect.Play();
    }
    private void StopAttackEffect()
    {
        AttactEffect.Stop();
    }
    private void PlayerWalkEffect_1()
    {
        WalkEffect.transform.localPosition = new Vector3(-1, -0.8f, 0);
        WalkEffect.transform.Rotate(new Vector3(0, 90, 0));
        WalkEffect.Play();
    }
    private void PlayerWalkEffect_2()
    {
        WalkEffect.transform.localPosition = new Vector3(0, -0.8f, 1);
        WalkEffect.transform.Rotate(new Vector3(0, 90, 0));
        WalkEffect.Play();
    }
    private void PlayerWalkEffect_3()
    {

        WalkEffect.transform.localPosition = new Vector3(1, -0.8f, 0);
        WalkEffect.transform.Rotate(new Vector3(0, 90, 0));
        WalkEffect.Play();
    }
    private void PlayerWalkEffect_4()
    {

        WalkEffect.transform.localPosition = new Vector3(0, -0.8f, -1);
        WalkEffect.transform.Rotate(new Vector3(0, 90, 0));
        WalkEffect.Play();
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
            LevelSystem.ObtainExp(GameManager.GetCubeExpAmount());
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

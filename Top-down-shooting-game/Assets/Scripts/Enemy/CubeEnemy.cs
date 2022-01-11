using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class CubeEnemy : MonoBehaviour
{
    private HealthSystem HealthSystem;
    private HealthSystem PlayerHealthSystem;
    private EnemyHealthBar EnemyHealthBar;
    private Animator Animator;
    private GameManager GameManager;
    [SerializeField]
    private GameObject Destroyed;
    [SerializeField]
    private VisualEffect AttactEffect;
    [SerializeField]
    private bool IsAttacking;
    private void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
        HealthSystem = new HealthSystem(GameManager.GetCubeHP());
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
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 0.7f) && hit.collider.CompareTag("Player"))
        {
            Animator.SetBool("IsAttack", true);
        }
        else
        {
            Animator.SetBool("IsAttack", false);
        }
        if (IsAttacking)
        {
            AttactEffect.SendEvent("Attacking");
            if (Physics.SphereCast(transform.position, 0.05f, transform.forward, out RaycastHit Hit, 1f) && Hit.transform.CompareTag("Player"))
            {
                PlayerHealthSystem.Damage(GameManager.GetCubeDamage());
            }
        }
        else
        {
            AttactEffect.SendEvent("StopAttacking");
        }
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
                    collider.GetComponent<Rigidbody>().AddExplosionForce(400, transform.position, 1);
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

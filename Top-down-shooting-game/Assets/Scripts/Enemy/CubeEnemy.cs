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
    private GameObject Destory;
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
        Attact();
        Dead();
    }
    private void Attact()
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
            GameObject destroy = Instantiate(Destory, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(destroy, 5);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            transform.Find("HealthBar").gameObject.SetActive(true);
            HealthSystem.Damage(GameManager.GetPlayerDamage());
        }
    }
}

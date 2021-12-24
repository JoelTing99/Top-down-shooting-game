using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallStellatedEnemy : MonoBehaviour
{
    private HealthSystem HealthSystem;
    private GameManager GameManager;
    private EnemyHealthBar EnemyHealthBar;
    private Animator Animator;
    [SerializeField]
    private bool IsAttacking;
    private void Awake()
    {
        GameManager = FindObjectOfType<GameManager>();
        HealthSystem = new HealthSystem(GameManager.GetSmallStellatedHP());
        EnemyHealthBar = transform.Find("HealthBar").GetComponent<EnemyHealthBar>();
        EnemyHealthBar.SetHealthSystem(HealthSystem);
        Animator = GetComponent<Animator>();
        transform.Find("HealthBar").gameObject.SetActive(false);
    }
    private void Update()
    {
        Attact();
    }
    private void Attact()
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

        }
        else
        {

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

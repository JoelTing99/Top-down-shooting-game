using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallStellatedAttack : MonoBehaviour
{
    private GameManager GameManager;
    private HealthSystem PlayerHealthSystem;
    void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
        PlayerHealthSystem = GameManager.GetPlayerHealth();
        Collider[] Collider = Physics.OverlapSphere(transform.position, 2f);
        foreach (var collider in Collider)
        {
            if(collider.GetComponent<Rigidbody>() != null)
            {
                collider.GetComponent<Rigidbody>().AddExplosionForce(400, transform.position, 1);
            }
            if (collider.CompareTag("Player"))
            {
                PlayerHealthSystem.Damage(GameManager.GetSmallStellatedDamage());
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallStellatedAttack : MonoBehaviour
{
    private GameManager GameManager;
    void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
        Collider[] Collider = Physics.OverlapSphere(transform.position, 2f);
        foreach (var collider in Collider)
        {
            if(collider.GetComponent<Rigidbody>() != null)
            {
                collider.GetComponent<Rigidbody>().AddExplosionForce(500, transform.position, 1.5f);
            }
            if (collider.CompareTag("Player"))
            {
                GameManager.AttackPlayer(GameManager.GetSmallStellatedDamage());
            }
        }
        Destroy(gameObject, 5);
    }

}

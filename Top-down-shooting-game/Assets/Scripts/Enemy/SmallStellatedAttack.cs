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
                Textpopup DamagePopup = Textpopup.Create(collider.transform.position + new Vector3(0, 2, 0), (int)GameManager.GetSmallStellatedDamage(), Color.red);
                DamagePopup.SetFontSize(5);
            }
        }
        Destroy(gameObject, 5);
    }

}

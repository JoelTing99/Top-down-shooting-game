using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePack : MonoBehaviour
{
    private GameManager GameManager;
    void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.StartDamageBoost();
            Destroy(gameObject);
        }
    }
}

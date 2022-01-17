using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPack : MonoBehaviour
{
    private GameManager GameManager;
    private HealthSystem HealthSystem; 
    void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
        HealthSystem = GameManager.GetPlayerHealthSystem();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HealthSystem.Heal(GameManager.GetHealPackAmount());
            Destroy(gameObject);
        }
    }
}

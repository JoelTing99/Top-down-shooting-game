using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private HealthSystem PlayerHealth;
    private Player Player;
    private HealthBar HealthBar;

    private void Awake()
    {
        PlayerHealth = new HealthSystem(100);
        Player = GameObject.FindWithTag("Player").GetComponent<Player>();
        HealthBar = FindObjectOfType<HealthBar>();
        Player.SetHealthSystem(PlayerHealth);
        HealthBar.SetHealthSystem(PlayerHealth);
    }

    private void Update()
    {
        Debug.Log(PlayerHealth.GetHealth());
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerHealth.Damage(10);
        }
        
    }
}

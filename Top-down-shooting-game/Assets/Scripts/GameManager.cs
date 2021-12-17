using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private HealthSystem HealthSystem;
    private Player Player;

    private void Awake()
    {
        HealthSystem = new HealthSystem(100);
        Player = GameObject.FindWithTag("Player").GetComponent<Player>();
        Player.SetHealthSystem(HealthSystem);
    }

    private void Update()
    {
        Debug.Log(HealthSystem.GetHealth());
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HealthSystem.Damage(10);
        }
    }
}

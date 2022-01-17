using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event EventHandler OnCollectedCoin;

    private HealthSystem PlayerHealth;
    private PlayerHealthBar PlayerHealthBar;
    [SerializeField] GameObject Coins;
    private int CoinsAmount;
    //Player
    private float PlayerHP = 300;
    private float PlayerDamage = 60;
    private float PlayerSpeed = 2;
    private float AttackSpeed = 1;
    private float Defense = 3;
    private float DodgeRate = 0;

    //Ability
    private float ThrowGrenadeDistance = 3;
    private float GrenadeDamage = 150;
    private float GrendaeCoolDownTime = 10; 
    private float RollCoolDownTime = 3;
    private float RollDistance = 3;

    //Enemy Health
    private float CubeHP = 280;
    private float DodecahedronHP = 140;
    private float FurstumHP = 140;
    private float OctahedronHP = 80;
    private float SmallStellatedHP = 80;

    //Enemy Damage
    private float CubeDamage = 0.11f; // * 91
    private float DodecahedronDamage = 30;
    private float FurstumDamage = 30;
    private float OctahedronDamage = 50;
    private float SmallStellatedDamage = 80;

    //Enemy Speed
    private float CubeSpeed = 1.5f;
    private float DodecahedronSpeed = 2;
    private float FurstumSpeed = 3;
    private float OctahedronSpeed = 2;
    private float SmallStellatedSpeed = 5;

    //Item
    private float HealPackAmount = 40;
    private void Awake()
    {
        PlayerHealth = new HealthSystem(PlayerHP);
        PlayerHealthBar = FindObjectOfType<PlayerHealthBar>();
        PlayerHealthBar.SetHealthSystem(PlayerHealth);
    }
    private void Update()
    {
        if(Input.touchCount >= 1)
        {
            Debug.Log("Touch");
        }
        if(PlayerHealth.GetHealth() <= 0)
        {
            Destroy(GameObject.FindWithTag("Player"));
        }
    }
    public GameObject GetCoinsGameObject()
    {
        return Coins;
    }
    public int GetCoinAmount()
    {
        return CoinsAmount;
    }
    public void AddCoin(int Amount)
    {
        CoinsAmount += Amount;
        if(OnCollectedCoin != null)
        {
            OnCollectedCoin(this, EventArgs.Empty);
        }
    }
    //Player
    public HealthSystem GetPlayerHealthSystem()
    {
        return PlayerHealth;
    }
    public void AttackPlayer(float damage)
    {
        if (UnityEngine.Random.value > DodgeRate)
        {
            if(damage <= Defense)
            {
                PlayerHealth.Damage(damage);
            }
            else
            {
                PlayerHealth.Damage(damage - Defense);
            }
        }
        else
        {

        }
    }
    public float GetPlayerDamage()
    {
        return PlayerDamage;
    }
    public float GetPlayerSpeed()
    {
        return PlayerSpeed;
    }
    public float GetAttackSpeed()
    {
        return AttackSpeed;
    }
    public float GetDodgeRate()
    {
        return DodgeRate;
    }
    //Item
    public float GetHealPackAmount()
    {
        return HealPackAmount;
    }
    //Enemy Health
    public float GetCubeHP()
    {
        return CubeHP;
    }
    public float GetDodecahedronHP()
    {
        return DodecahedronHP;
    }
    public float GetFurstumHP()
    {
        return FurstumHP;
    }
    public float GetOctahedronHP()
    {
        return OctahedronHP;
    }
    public float GetSmallStellatedHP()
    {
        return SmallStellatedHP;
    }
    //Enemy Damage
    public float GetCubeDamage()
    {
        return CubeDamage;
    }
    public float GetDodecahedronDamage()
    {
        return DodecahedronDamage;
    }
    public float GetFurstumDamage()
    {
        return FurstumDamage;
    }
    public float GetOctahedronDamage()
    {
        return OctahedronDamage;
    }
    public float GetSmallStellatedDamage()
    {
        return SmallStellatedDamage;
    }
    //Enemy Speed
    public float GetCubeSpeed()
    {
        return CubeSpeed;
    }
    public float GetDodecahedronSpeed()
    {
        return DodecahedronSpeed;
    }
    public float GetFurstumSpeed()
    {
        return FurstumSpeed;
    }
    public float GetOctahedronSpeed()
    {
        return OctahedronSpeed;
    }
    public float GetSmallStellatedSpeed()
    {
        return SmallStellatedSpeed;
    }
    //Ability
    public float GetGrenadeDamage()
    {
        return GrenadeDamage;
    }
    public float GetThrowGrenadeDistance()
    {
        return ThrowGrenadeDistance;
    }
    public float GetGrendaeCoolDownTime()
    {
        return GrendaeCoolDownTime;
    }
    public float GetRollCoolDownTime()
    {
        return RollCoolDownTime;
    }
    public float GetRollDistance()
    {
        return RollDistance;
    }
}

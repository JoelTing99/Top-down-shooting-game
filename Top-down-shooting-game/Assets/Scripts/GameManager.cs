using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event EventHandler OnCollectedCoin;

    private HealthSystem PlayerHealth;
    private PlayerHealthBar PlayerHealthBar;
    [SerializeField] private GameObject Coins;
    //Player
    private float PlayerHP = 640;
    private float PlayerDamage = 60;
    private float PlayerDamage_Return;
    private float PlayerSpeed = 320;
    private float PlayerSpeed_Return;
    private float AttackSpeed = 1;
    private float Armor = 3;
    private float Armor_Return;
    private float DodgeRate = 0;

    //Ability
    private float ThrowGrenadeDistance = 3;
    private float GrenadeDamage = 150;
    private float GrendaeCoolDownTime = 25; 
    private float RollCoolDownTime = 3;
    private float RollDistance = 45;

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
    private int CoinsAmount;
    private float HealPackAmount = 70;
    private float BoostDuration = 5;
    private float DamageBoostRate = 0.3f;
    private float DefenseBoostRate = 0.5f;
    private float SpeedUpRate = 0.2f;
    private void Awake()
    {
        PlayerHealth = new HealthSystem(PlayerHP);
        PlayerHealthBar = FindObjectOfType<PlayerHealthBar>();
        PlayerHealthBar.SetHealthSystem(PlayerHealth);
        PlayerDamage_Return = PlayerDamage;
        PlayerSpeed_Return = PlayerSpeed;
        Armor_Return = Armor;
        PlayerSpeed_Return = PlayerSpeed;
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
        Debug.Log($"HP = {PlayerHP}");
        Debug.Log($"Damge = {PlayerDamage_Return}");
        Debug.Log($"Speed = {PlayerSpeed_Return}");
        Debug.Log($"Armor = {Armor_Return}");
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
            if(damage <= Armor_Return)
            {
                PlayerHealth.Damage(damage);
            }
            else
            {
                PlayerHealth.Damage(damage - Armor_Return);
            }
        }
        else
        {

        }
    }
    public float GetPlayerDamage()
    {
        return PlayerDamage_Return;
    }
    public float GetPlayerSpeed()
    {
        return PlayerSpeed_Return;
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
    public float GetBoostDuration()
    {
        return BoostDuration;
    }
    public void StartDamageBoost()
    {
        StartCoroutine(DamageBoost(BoostDuration));
    }
    private IEnumerator DamageBoost(float time)
    {
        PlayerDamage_Return = PlayerDamage + PlayerDamage * DamageBoostRate;
        while (time >= 0)
        {
            time -= Time.deltaTime;
            yield return null;
        }
        PlayerDamage_Return = PlayerDamage;
    }
    public void StartDefenseBoost()
    {
        StartCoroutine(DefenseBoost(BoostDuration));
    }
    private IEnumerator DefenseBoost(float time)
    {
        Armor_Return = Armor + Armor * DefenseBoostRate;
        while (time >= 0)
        {
            time -= Time.deltaTime;
            yield return null;
        }
        Armor_Return = Armor;
    }
    public void StartSpeedUp()
    {
        StartCoroutine(SpeedUp(BoostDuration));
    }
    private IEnumerator SpeedUp(float time)
    {
        PlayerSpeed_Return = PlayerSpeed + PlayerSpeed * SpeedUpRate;
        while(time >= 0)
        {
            time -= Time.deltaTime;
            yield return null;
        }
        PlayerSpeed_Return = PlayerSpeed;
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

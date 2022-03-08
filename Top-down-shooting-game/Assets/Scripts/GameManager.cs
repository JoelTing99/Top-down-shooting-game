using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event EventHandler OnCollectedCoin;

    [SerializeField] private GameObject Coins;
    private HealthSystem PlayerHealth;
    private PlayerHealthBar PlayerHealthBar;
    private LevelSystem LevelSystem;
    //Player
    private float PlayerHP = 640;
    private float PlayerDamage = 30;
    private float PlayerDamage_Return;
    private float PlayerSpeed = 320;
    private float PlayerSpeed_Return;
    private float PlayerAttackSpeed = 1;
    private float PlayerAttackSpeed_Return;
    private float PlayerReloadSpeed = 1;
    private float PlayerReloadSpeed_Return;
    private int PlayerBulletCount = 12;
    private int PlayerBulletCount_Return;
    private float PlayerArmor = 3;
    private float PlayerArmor_Return;
    private float PlayerDodgeRate = 0;
    private float PlayerDodgeRate_Return;
    private float PlayerCritRate = 0;
    private float PlayerCritRate_Return;
    private float PlayerCritDamageRate = 2;
    private float PlayerCritDamageRate_Return;
    private float PlayerHeadShotRate = 0;
    private float PlayerHeadShotRate_Return;

    //Ability
    private float ThrowGrenadeDistance = 3;
    private float GrenadeDamage = 150;
    private float GrenadeCoolDownTime = 25;
    private float GrenadeExplodeRadius = 5;
    private float RollCoolDownTime = 5;
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

    //Experience amount
    private int CubeExpAmount = 20;
    private int DodecahedronExpAmount = 25;
    private int FurstumExpAmount = 12;
    private int OctahedronExpAmount = 18;
    private int SmallStellatedExpAmount = 30;

    //Item
    [SerializeField] private GameObject[] BonusItem;
    private int CoinsAmount;
    private float HealPackAmount = 70;
    private float BoostDuration = 5;
    private float DamageBoostRate = 0.3f;
    private float DefenseBoostRate = 0.5f;
    private float SpeedUpRate = 0.2f;
    private void Awake()
    {
        PlayerHealth = new HealthSystem(PlayerHP);
        LevelSystem = new LevelSystem(300);
        PlayerHealthBar = FindObjectOfType<PlayerHealthBar>();
        PlayerHealthBar.SetHealthSystem(PlayerHealth);
        PlayerDamage_Return = PlayerDamage;
        PlayerSpeed_Return = PlayerSpeed;
        PlayerArmor_Return = PlayerArmor;
        PlayerSpeed_Return = PlayerSpeed;
        PlayerAttackSpeed_Return = PlayerAttackSpeed;
        PlayerReloadSpeed_Return = PlayerReloadSpeed;
        PlayerBulletCount_Return = PlayerBulletCount;
        PlayerDodgeRate_Return = PlayerDodgeRate;
        PlayerCritRate_Return = PlayerCritRate;
        PlayerCritDamageRate_Return = PlayerCritDamageRate;
        PlayerHeadShotRate_Return = PlayerHeadShotRate;
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
        Debug.Log($"Armor = {PlayerArmor_Return}");
        Debug.Log($"Exp = {LevelSystem.GetExp()}");
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
    //Level
    public LevelSystem GetLevelSystem()
    {
        return LevelSystem;
    }
    //Player
    public HealthSystem GetPlayerHealthSystem()
    {
        return PlayerHealth;
    }
    public void AttackPlayer(float damage)
    {
        if (UnityEngine.Random.value > PlayerDodgeRate)
        {
            if(damage <= PlayerArmor_Return)
            {
                PlayerHealth.Damage(damage);
            }
            else
            {
                PlayerHealth.Damage(damage - PlayerArmor_Return);
            }
        }
        else
        {

        }
    }
    public float GetPlayerCritRate()
    {
        return PlayerCritRate_Return;
    }
    public float GetPlayerCritDamageRate()
    {
        return PlayerCritDamageRate_Return;
    }
    public float GetPlayerHeadShotRate()
    {
        return PlayerHeadShotRate_Return;
    }
    public float GetPlayerDamage()
    {
        return PlayerDamage_Return;
    }
    public float GetPlayerSpeed()
    {
        return PlayerSpeed_Return;
    }
    public float GetPlayerAttackSpeed()
    {
        return PlayerAttackSpeed_Return;
    }
    public float GetPlayerDodgeRate()
    {
        return PlayerDodgeRate_Return;
    }
    public float GetPlayerReloadSpeed()
    {
        return PlayerReloadSpeed_Return;
    }
    public int GetPlayerBulletCount()
    {
        return PlayerBulletCount_Return;
    }
    //Item
    public GameObject GetRandomBonusItem()
    {
        return BonusItem[UnityEngine.Random.Range(0, BonusItem.Length)];
    }
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
        PlayerArmor_Return = PlayerArmor + PlayerArmor * DefenseBoostRate;
        while (time >= 0)
        {
            time -= Time.deltaTime;
            yield return null;
        }
        PlayerArmor_Return = PlayerArmor;
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
    //Experience Amount
    public int GetCubeExpAmount()
    {
        return CubeExpAmount;
    }
    public int GetDodecahedronExpAmount()
    {
        return DodecahedronExpAmount;
    }
    public int GetFurstumExpAmount()
    {
        return FurstumExpAmount;
    }
    public int GetOctaedronExpAmount()
    {
        return OctahedronExpAmount;
    }
    public int GetSmallStellatedExpAmount()
    {
        return SmallStellatedExpAmount;
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
    public float GetGrenadeExplodeRadius()
    {
        return GrenadeExplodeRadius;
    }
    public float GetGrendaeCoolDownTime()
    {
        return GrenadeCoolDownTime;
    }
    public float GetRollCoolDownTime()
    {
        return RollCoolDownTime;
    }
    public float GetRollDistance()
    {
        return RollDistance;
    }
    //Upgrade
    
}

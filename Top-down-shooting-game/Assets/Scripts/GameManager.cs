using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event EventHandler OnCollectedCoin;

    public Mesh _mesh;
    public Material material;

    [SerializeField] private GameObject Coins;
    private HealthSystem PlayerHealth;
    private PlayerHealthBar PlayerHealthBar;
    private LevelSystem LevelSystem;
    //Player
    private float PlayerHP = 640;
    //private float PlayerHP_Return;
    private float PlayerDamage = 30;
    private float PlayerDamage_Return;
    private float PlayerSpeed = 320;
    private float PlayerSpeed_Return;
    private float PlayerAttackSpeed = 1;

    private float PlayerAttackSpeed_Return;
    private float PlayerReloadSpeed = 1;
    private float PlayerReloadSpeed_Return;
    private float PlayerLifeStealRate = 0.05f;
    private float PlayerLifeStealRate_Return;
    private int PlayerBulletCount = 12;
    private int PlayerBulletCount_Return;
    private float PlayerArmor = 3;
    private float PlayerArmor_Return;
    private float PlayerDodgeRate = 0;
    private float PlayerDodgeRate_Return;
    private float PlayerCritRate = 0.1f;
    private float PlayerCritRate_Return;
    private float PlayerCritDamageRate = 2;
    private float PlayerCritDamageRate_Return;
    private float PlayerHeadShootRate = 0;
    private float PlayerHeadShootRate_Return;
    private bool PlayerHavdLifeSteal = false;
    private bool PlayerHavdLifeSteal_Return;
    private bool IsTripleShoot = false;
    private bool IsTripleShoot_Return;
    private bool IsAutoShoot = false;
    private bool IsAutoShoot_Return;

    //Ability
    private float ThrowGrenadeDistance = 3;
    private float GrenadeDamage = 150;
    private float GrenadeDamage_Return;
    private float GrenadeCoolDownTime = 25;
    private float GrenadeCoolDownTime_Return;
    private float GrenadeExplodeRadius = 5;
    private float GrenadeExplodeRadius_Return;
    private float RollCoolDownTime = 5;
    private float RollCoolDownTime_Return;
    private float RollDistance = 45;
    private float RollDistance_Return;

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
    private int CubeExpAmount = 40;
    private int DodecahedronExpAmount = 50;
    private int FurstumExpAmount = 24;
    private int OctahedronExpAmount = 36;
    private int SmallStellatedExpAmount = 60;

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
        PlayerHeadShootRate_Return = PlayerHeadShootRate;
        PlayerLifeStealRate_Return = PlayerLifeStealRate;
        PlayerHavdLifeSteal_Return = PlayerHavdLifeSteal;
        IsTripleShoot_Return = IsTripleShoot;
        IsAutoShoot_Return = IsAutoShoot;

        RollDistance_Return = RollDistance;
        RollCoolDownTime_Return = RollCoolDownTime;
        GrenadeExplodeRadius_Return = GrenadeExplodeRadius;
        GrenadeDamage_Return = GrenadeDamage;
        GrenadeCoolDownTime_Return = GrenadeCoolDownTime;
    }
    private void Update()
    {
        if(Input.touchCount >= 1)
        {
            Debug.Log("Touch");
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
    public float GetPlayerHeadShootRate()
    {
        return PlayerHeadShootRate_Return;
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
    public float GetPlayerLifeStealRate()
    {
        return PlayerLifeStealRate_Return;
    }
    public bool GetIsTripleShot()
    {
        return IsTripleShoot_Return;
    }
    public bool GetIsAutoShot()
    {
        return IsAutoShoot_Return;
    }
    public bool GetHaveLifeSteal()
    {
        return PlayerHavdLifeSteal_Return;
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
        return GrenadeDamage_Return;
    }
    public float GetThrowGrenadeDistance()
    {
        return ThrowGrenadeDistance;
    }
    public float GetGrenadeExplodeRadius()
    {
        return GrenadeExplodeRadius_Return;
    }
    public float GetGrenadeCoolDownTime()
    {
        return GrenadeCoolDownTime_Return;
    }
    public float GetRollCoolDownTime()
    {
        return RollCoolDownTime_Return;
    }
    public float GetRollDistance()
    {
        return RollDistance_Return;
    }
    //Upgrade
    public void DamageUpgrade()
    {
        PlayerDamage_Return += PlayerDamage * 0.05f;
    }
    public void AttackSpeedUpgrade()
    {
        PlayerAttackSpeed_Return += PlayerAttackSpeed * 0.05f;
    }
    public void CritRateUpgrade()
    {
        PlayerCritRate_Return += 0.05f;
    }
    public void GetLifeSteal()
    {
        PlayerHavdLifeSteal_Return = true;
    }
    public void LifeStealRateUpgrade()
    {
        PlayerLifeStealRate_Return += 0.04f;
    }
    public void TripleShoot()
    {
        IsTripleShoot_Return = true;
        PlayerAttackSpeed_Return = 1;
    }
    public void AutoShoot()
    {
        IsAutoShoot_Return = true;
        PlayerAttackSpeed_Return = 1;
    }
    public void HeadShotRateUpgrade()
    {
        PlayerHeadShootRate_Return += 0.02f;
    }
    public void CritDamageRateUpgrade()
    {
        PlayerCritDamageRate_Return += PlayerCritDamageRate * 0.25f;
    }
    public void MoveSpeedUpgrade()
    {
        PlayerSpeed_Return += PlayerSpeed * 0.03f;
    }
    public void ReloatSpeedUpgrade()
    {
        PlayerReloadSpeed_Return += PlayerReloadSpeed * 20;
    }
    public void BulletAmountUpgrade()
    {
        PlayerBulletCount_Return += 5;
    }
    public void HealthUpgrade()
    {
        PlayerHealth.UpgradeHealth(PlayerHP * 0.05f);
    }
    public void ArmorUpgrade()
    {
        PlayerArmor_Return += 2;
    }
    public void DodgeRateUpgrade()
    {
        PlayerDodgeRate_Return += PlayerDodgeRate * 0.06f;
    }
    public void RollDistanceUpgrade()
    {
        RollDistance_Return += RollDistance * 0.05f;
    }
    public void RollCoonDownUpgrade()
    {
        RollCoolDownTime_Return -= RollCoolDownTime * 0.1f;
    }
    public void GrenadeDamageUpgrade()
    {
        GrenadeDamage_Return += GrenadeDamage * 0.1f;
    }
    public void GrenadeExplodeRadiusUpgrade()
    {
        GrenadeExplodeRadius_Return += GrenadeExplodeRadius * 0.05f;
    }
    public void GrenadeCoonDownUpgrade()
    {
        GrenadeCoolDownTime_Return -= GrenadeCoolDownTime * 0.3f;
    }
}

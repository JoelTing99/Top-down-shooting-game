using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private HealthSystem PlayerHealth;
    private PlayerHealthBar PlayerHealthBar;
    //Player
    private float PlayerHP = 200;
    private float PlayerDamage = 10;
    private float PlayerSpeed = 3;
    private float AttackSpeed = 1;

    //Ability
    private float ThrowGrenadeDistance = 3;
    private float GrenadeDamage = 20;
    private float GrendaeCoolDownTime = 10; 
    private float RollCoolDownTime = 3;
    private float RollDistance = 3;

    //Enemy Health
    private float CubeHP = 50;
    private float DodecahedronHP = 50;
    private float FurstumHP = 50;
    private float OctahedronHP = 50;
    private float SmallStellatedHP = 50;

    //Enemy Damage
    private float CubeDamage = 0.11f;
    private float DodecahedronDamage = 10;
    private float FurstumDamage = 10;
    private float OctahedronDamage = 10;
    private float SmallStellatedDamage = 10;

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
    }
    //Player
    public HealthSystem GetPlayerHealth()
    {
        return PlayerHealth;
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

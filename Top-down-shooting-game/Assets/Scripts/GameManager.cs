using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private HealthSystem PlayerHealth;
    private PlayerHealthBar PlayerHealthBar;

    private float PlayerHP = 100;
    private float CubeHP = 50;
    private float DodecahedronHP = 50;
    private float FurstumHP = 50;
    private float OctahedronHP = 50;
    private float SmallStellatedHP = 50;

    private float PlayerDamage = 10;
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
    public HealthSystem GetPlayerHealth()
    {
        return PlayerHealth;
    }
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


    public float GetCubeDamage()
    {
        return CubeDamage;
    }
    public float GetPlayerDamage()
    {
        return PlayerDamage;
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
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem
{
    public event EventHandler OnDamaged;
    public event EventHandler OnHealed;

    private float Health;
    private float HealthMax;

    public HealthSystem(float HealthMax)
    {
        this.HealthMax = HealthMax;
        Health = HealthMax;
    }

    public float GetHealth()
    {
        return Health;
    }

    public float GetHealthPercent()
    {
        return Health / HealthMax;
    }

    public void Damage(float DamageAmount)
    {
        Health -= DamageAmount;
        if(Health < 0)
        {
            Health = 0;
        }
        if(OnDamaged != null)
        {
            OnDamaged(this, EventArgs.Empty);
        }
    }
    public void Heal(float HealAmount)
    {
        Health += HealAmount;
        if(Health >= HealthMax)
        {
            Health = HealthMax;
        }
        if(OnHealed != null)
        {
            OnHealed(this, EventArgs.Empty);
        }
    }
    public void UpgradeHealth(float HealAmount)
    {
        HealthMax += HealAmount;
        Heal(HealAmount);
    }
}

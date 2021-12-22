using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem
{
    public event EventHandler OnDamaged;

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
}

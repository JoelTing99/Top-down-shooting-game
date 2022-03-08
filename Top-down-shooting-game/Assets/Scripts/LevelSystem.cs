using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem
{
    public event EventHandler OnGetExp;
    public event EventHandler OnLevelUp;
    public event EventHandler OnUsedUpgradePoint;

    private float Exp;
    private float ExpMax;
    private int Level;
    private int UpgradePoint;

    public LevelSystem(float ExpMax)
    {
        this.ExpMax = ExpMax;
        Exp = 0;
        Level = 1;
    }
    public float GetExp()
    {
        return Exp;
    }
    public int GetLevel()
    {
        return Level;
    }
    public float GetExpPercant()
    {
        return Exp / ExpMax;
    }
    public int GetUpgradePoint()
    {
        return UpgradePoint;
    }
    public void ObtainExp(float amount)
    {
        Exp += amount;
        if(Exp >= ExpMax)
        {
            Exp = 0;
            LevelUp();
        }
        if(OnGetExp != null)
        {
            OnGetExp(this, EventArgs.Empty);
        }
    }
    public void UseUpgradePoint(int amount)
    {
        UpgradePoint -= amount;
        if(OnUsedUpgradePoint != null)
        {
            OnUsedUpgradePoint(this, EventArgs.Empty);
        }
    }
    private void LevelUp()
    {
        Level++;
        UpgradePoint++;
        ExpMax *= 1.035f;
        if(OnLevelUp != null)
        {
            OnLevelUp(this, EventArgs.Empty);
        }
    }
}

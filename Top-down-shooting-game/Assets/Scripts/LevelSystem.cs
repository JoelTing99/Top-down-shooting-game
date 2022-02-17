using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem
{
    public event EventHandler OnGetExp;
    public event EventHandler OnLevelUp;

    private float Exp;
    private float ExpMax;
    private int Level;

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
    private void LevelUp()
    {
        Level++;
        if(OnLevelUp != null)
        {
            OnLevelUp(this, EventArgs.Empty);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasteryUpgrade
{
    public float SpeedUpgrade(float Target, float Rate, int Level)
    {
        return Target + Target * Rate * Level;
    }
}

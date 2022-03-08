using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New mastery", menuName = "Mastery")]
public class Mastery : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    public int Cost;
    public int MaxLevel;
}

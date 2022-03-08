using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class MasteryDisplay : MonoBehaviour
{
    private GameManager GameManager;
    private LevelSystem LevelSystem;
    private int CurrentLevel = 0;
    [SerializeField] private Mastery Mastery;
    [SerializeField] private GameObject CheckBox;
    [SerializeField] private Image Icon;
    [SerializeField] private Text Level;
    [SerializeField] private Text Cost;
    private void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
        LevelSystem = GameManager.GetLevelSystem();
        LevelSystem.OnUsedUpgradePoint += LevelSystem_OnUsedUpgradePoint;
        Cost.text = Mastery.Cost.ToString();
        Icon.sprite = Mastery.Icon;
        CheckCost();
    }
    private void Update()
    {
        Level.text = CurrentLevel.ToString() + "/" + Mastery.MaxLevel.ToString();
        
    }
    private void LevelSystem_OnUsedUpgradePoint(object sender, System.EventArgs e)
    {
        CheckCost();
    }

    private void CheckCost()
    {
        if (LevelSystem.GetUpgradePoint() >= Mastery.Cost && CurrentLevel <= Mastery.MaxLevel)
        {
            CheckBox.SetActive(false);
        }
        else
        {
            CheckBox.SetActive(true);
        }
    }
    public void UpGrade()
    {
        LevelSystem.UseUpgradePoint(Mastery.Cost);
        CurrentLevel++;
    }
}

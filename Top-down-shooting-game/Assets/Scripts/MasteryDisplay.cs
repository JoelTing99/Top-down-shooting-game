using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class MasteryDisplay : MonoBehaviour
{
    private GameManager GameManager;
    private LevelSystem LevelSystem;
    [SerializeField] private UnityEvent Upgrade;
    [SerializeField] private Mastery Mastery;
    [SerializeField] private GameObject CheckBox;
    [SerializeField] private Image Icon;
    [SerializeField] private Text Level;
    [SerializeField] private Text Cost;
    private void Start()
    {
        Mastery.CurrentLevel = 0;
        GameManager = FindObjectOfType<GameManager>();
        LevelSystem = GameManager.GetLevelSystem();
        LevelSystem.OnUsedUpgradePoint += LevelSystem_OnUsedUpgradePoint;
        Cost.text = Mastery.Cost.ToString();
        Level.text = Mastery.CurrentLevel.ToString() + "/" + Mastery.MaxLevel.ToString();
        Icon.sprite = Mastery.Icon;
        UpgradeCheck();
    }
    private void Update()
    {
        
    }
    private void LevelSystem_OnUsedUpgradePoint(object sender, System.EventArgs e)
    {
        UpgradeCheck();
        Level.text = Mastery.CurrentLevel.ToString() + "/" + Mastery.MaxLevel.ToString();
    }

    private void UpgradeCheck()
    {
        if (Mastery.Condition != null)
        {
            if (LevelSystem.GetUpgradePoint() >= Mastery.Cost &&
                Mastery.CurrentLevel < Mastery.MaxLevel &&
                Mastery.Condition.CurrentLevel >= Mastery.RequiredLevel)
            {
                CheckBox.SetActive(false);
            }
            else
            {
                CheckBox.SetActive(true);
            }
        }
        else
        {
            if (LevelSystem.GetUpgradePoint() >= Mastery.Cost && Mastery.CurrentLevel < Mastery.MaxLevel)
            {
                CheckBox.SetActive(false);
            }
            else
            {
                CheckBox.SetActive(true);
            }
        }
    }
    public void UpGrade()
    {
        Mastery.CurrentLevel++;
        LevelSystem.UseUpgradePoint(Mastery.Cost);
        Upgrade.Invoke();
    }
}

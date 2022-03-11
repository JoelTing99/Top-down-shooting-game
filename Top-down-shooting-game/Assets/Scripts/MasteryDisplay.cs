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
    [SerializeField] private GameObject DescriptionImage;
    private Text Description;
    private Text Title;
    private Text Requirement;
    CanvasGroup Canvasgroup;
    private float Count = 0.5f;
    private void Start()
    {
        Mastery.CurrentLevel = 0;
        Canvasgroup = DescriptionImage.GetComponent<CanvasGroup>();
        Description = DescriptionImage.transform.Find("Text").GetComponent<Text>();
        Title = DescriptionImage.transform.Find("Title").GetComponent<Text>();
        Requirement = DescriptionImage.transform.Find("Requirement").GetComponent<Text>();
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
    public void SetDescriptionactive(bool Condition)
    {
        StartCoroutine(SetDescriptionActive(Condition));
    }
    public IEnumerator SetDescriptionActive(bool Condition)
    {
        float count = Count;
        if (Condition)
        {
            yield return new WaitForSecondsRealtime(1);
            DescriptionImage.SetActive(true);
            Canvasgroup.alpha = 0;
            Title.text = Mastery.Title;
            Description.text = Mastery.Description;
            if(Mastery.Requirement != null)
            {
                Requirement.text = Mastery.Requirement;
            }
            while (count >= 0)
            {
                if (!Condition)
                {
                    break;
                }
                count -= Time.deltaTime;
                Canvasgroup.alpha += 3 * Time.deltaTime;
                yield return null;
            }
        }
        else
        {
            while (count >= 0)
            {
                count -= Time.deltaTime;
                Canvasgroup.alpha -= 5 * Time.deltaTime;
                yield return null;
            }
            DescriptionImage.SetActive(false);
        }
    }
}

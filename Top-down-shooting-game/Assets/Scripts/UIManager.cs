using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private Player Player;
    private LevelSystem LevelSystem;
    private SpawnManager SpawnManager;
    private GameManager GameManager;
    private Text WaveNum;
    private Text CoinAmount;
    private Text LevelText;
    [SerializeField] private Text UpgradePoint;
    [SerializeField] private GameObject MasteryTree;
    [SerializeField] private GameObject QuitConfirmMenu;
    [SerializeField] private GameObject Arrow;
    private GameObject GrenadeCoolDownImage;
    private GameObject RollCoolDownImage;
    private Image LevelImage;
    private List<Transform> Spawners;
    private bool MasteryTreeIsopened;
    private bool QuitConfirmIsopened;

    void Start()
    {
        //UI Elemnet
        Player = FindObjectOfType<Player>();
        SpawnManager = FindObjectOfType<SpawnManager>();
        GameManager = FindObjectOfType<GameManager>();
        LevelSystem = GameManager.GetLevelSystem();

        GameManager.OnCollectedCoin += GameManager_OnCollectedCoin;
        LevelSystem.OnGetExp += LevelSystem_OnGetExp;
        LevelSystem.OnLevelUp += LevelSystem_OnLevelUp;
        LevelSystem.OnUsedUpgradePoint += LevelSystem_OnUsedUpgradePoint;

        WaveNum = transform.Find("Wave Counter").Find("Wave").GetComponent<Text>();
        CoinAmount = transform.Find("Coins").Find("Amount").GetComponent<Text>();
        GrenadeCoolDownImage = transform.Find("Grenade").Find("GrenadeCooldown").gameObject;
        RollCoolDownImage = transform.Find("Roll").Find("RollCooldown").gameObject;
        LevelImage = transform.Find("Level").Find("Bar").GetComponent<Image>();
        LevelText = transform.Find("Level").Find("Level").GetComponent<Text>();
        Spawners = SpawnManager.GetSpawners();

        UpgradePoint.text = LevelSystem.GetUpgradePoint().ToString();

        SetExpBar();
        SetExpText();

        Invoke("SetArrow", 5);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) OpenMasteryTree();
        if (Input.GetKeyDown(KeyCode.Escape)) OpenQuitConfirm();
        //UI Elemnet
        GrenadeCoolDownImage.SetActive(Player.GetGrenadeCoolDownImageActive());
        GrenadeCoolDownImage.GetComponent<Image>().fillAmount = Player.GetGrenadeCoolDownImagefillAmount();
        RollCoolDownImage.SetActive(Player.GetRollCoolDownImageActive());
        RollCoolDownImage.GetComponent<Image>().fillAmount = Player.GetRollCoolDownImagefillAmount();
        WaveNum.text = SpawnManager.GetWaveCount();
        
    }
    private void SetExpBar()
    {
        LevelImage.fillAmount = LevelSystem.GetExpPercant();
    }
    private void SetExpText()
    {
        LevelText.text = "Lv " + LevelSystem.GetLevel().ToString();
    }
    private void LevelSystem_OnGetExp(object sender, System.EventArgs e)
    {
        SetExpBar();
    }
    private void LevelSystem_OnLevelUp(object sender, System.EventArgs e)
    {
        SetExpText();
        SetExpBar();
    }
    private void LevelSystem_OnUsedUpgradePoint(object sender, System.EventArgs e)
    {
        UpgradePoint.text = LevelSystem.GetUpgradePoint().ToString();
    }
    private void GameManager_OnCollectedCoin(object sender, System.EventArgs e)
    {
        CoinAmount.text = GameManager.GetCoinAmount().ToString();
    }
    public void OpenMasteryTree()
    {
        if (MasteryTreeIsopened)
        {
            Time.timeScale = 1f;
            MasteryTreeIsopened = false;
            MasteryTree.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            MasteryTreeIsopened = true;
            MasteryTree.SetActive(true);
        }
    }
    public void OpenQuitConfirm()
    {
        if (QuitConfirmIsopened)
        {
            Time.timeScale = 1f;
            QuitConfirmIsopened = false;
            QuitConfirmMenu.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            QuitConfirmIsopened = true;
            QuitConfirmMenu.SetActive(true);
        }
    }
    public void Quit()
    {
        SceneManager.LoadScene(0);
    }
    private void SetArrow()
    {
        Spawners = SpawnManager.GetSpawners();
        if(Spawners != null)
        {
            foreach (var spawner in Spawners)
            {
                GameObject arrow = Instantiate(Arrow, transform);
                arrow.GetComponent<GuideArrow>().SetSpawner(spawner);
            }
        }
    }
}

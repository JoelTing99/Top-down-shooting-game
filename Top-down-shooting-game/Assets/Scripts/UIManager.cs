using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Player Player;
    private LevelSystem LevelSystem;
    private SpawnManager SpawnManager;
    private GameManager GameManager;
    private Text WaveNum;
    private Text CoinAmount;
    private GameObject GrenadeCoolDownImage;
    private GameObject RollCoolDownImage;
    private Image ReloadImage;
    private Image LevelImage;

    void Start()
    {
        //UI Elemnet
        Player = FindObjectOfType<Player>();
        SpawnManager = FindObjectOfType<SpawnManager>();
        GameManager = FindObjectOfType<GameManager>();
        GameManager.OnCollectedCoin += GameManager_OnCollectedCoin;
        WaveNum = transform.Find("Wave Counter").Find("Wave").GetComponent<Text>();
        CoinAmount = transform.Find("Coins").Find("Amount").GetComponent<Text>();
        GrenadeCoolDownImage = transform.Find("Grenade").Find("GrenadeCooldown").gameObject;
        RollCoolDownImage = transform.Find("Roll").Find("RollCooldown").gameObject;
        ReloadImage = transform.Find("Right joystick").Find("Reload").GetComponent<Image>();
    }


    void Update()
    {
        //UI Elemnet
        GrenadeCoolDownImage.SetActive(Player.GetGrenadeCoolDownImageActive());
        GrenadeCoolDownImage.GetComponent<Image>().fillAmount = Player.GetGrenadeCoolDownImagefillAmount();
        RollCoolDownImage.SetActive(Player.GetRollCoolDownImageActive());
        RollCoolDownImage.GetComponent<Image>().fillAmount = Player.GetRollCoolDownImagefillAmount();
        ReloadImage.fillAmount = Player.GetReloadImagefillAmount();
        WaveNum.text = SpawnManager.GetWaveCount();
    }
    public void SetLevelSystem(LevelSystem levelSystem)
    {
        this.LevelSystem = levelSystem;
    }
    private void GameManager_OnCollectedCoin(object sender, System.EventArgs e)
    {
        CoinAmount.text = GameManager.GetCoinAmount().ToString();
    }
}

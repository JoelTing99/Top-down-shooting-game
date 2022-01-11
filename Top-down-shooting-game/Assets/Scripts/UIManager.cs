using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Player Player;
    private SpawnManager SpawnManager;
    private Text WaveTimer;
    private Text WaveNum;
    private Text Timer;
    private GameObject GrenadeCoolDownImage;
    private GameObject RollCoolDownImage;
    private Image ReloadImage;
    private string TimerString;
    private float time;
    private int time_Minutes = 0;

    void Start()
    {
        Player = FindObjectOfType<Player>();
        SpawnManager = FindObjectOfType<SpawnManager>();
        WaveTimer = transform.Find("Wave Timer").Find("Time").GetComponent<Text>();
        WaveNum = transform.Find("Wave Counter").Find("Wave").GetComponent<Text>();
        Timer = transform.Find("Timer").Find("Time").GetComponent<Text>();
        GrenadeCoolDownImage = transform.Find("Grenade").Find("GrenadeCooldown").gameObject;
        RollCoolDownImage = transform.Find("Roll").Find("RollCooldown").gameObject;
        ReloadImage = transform.Find("Right joystick").Find("Reload").GetComponent<Image>();
    }
    void Update()
    {
        GrenadeCoolDownImage.SetActive(Player.GetGrenadeCoolDownImageActive());
        GrenadeCoolDownImage.GetComponent<Image>().fillAmount = Player.GetGrenadeCoolDownImagefillAmount();
        RollCoolDownImage.SetActive(Player.GetGrenadeCoolDownImageActive());
        RollCoolDownImage.GetComponent<Image>().fillAmount = Player.GetRollCoolDownImagefillAmount();
        ReloadImage.fillAmount = Player.GetReloadImagefillAmount();
        WaveTimer.text = SpawnManager.GetWaveTime();
        WaveNum.text = SpawnManager.GetWaveCount();
        TimerCount();
    }
    private void TimerCount()
    {
        time += Time.deltaTime;
        if(time < 60)
        {
            if(time < 10)
            {
                if (time_Minutes < 10)
                {
                    TimerString = "0" + time_Minutes.ToString() + ":0" + ((int)time).ToString();
                }
                else
                {
                    TimerString = time_Minutes.ToString() + ":0" + ((int)time).ToString();
                }
            }
            else
            {
                if (time_Minutes < 10)
                {
                    TimerString = "0" + time_Minutes.ToString() + ":" + ((int)time).ToString();
                }
                else
                {
                    TimerString = time_Minutes.ToString() + ":" + ((int)time).ToString();
                }
            }
        }
        else
        {
            time_Minutes++;
            time -= 60;
        }
        Timer.text = TimerString;
    }
}

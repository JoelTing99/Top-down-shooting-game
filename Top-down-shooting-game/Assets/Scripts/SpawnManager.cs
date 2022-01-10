using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Transform[] Spawners;
    [SerializeField] private GameObject[] TypeOfEnemy;
    [SerializeField] private float SpawnPeriod;
    [SerializeField] private int SpawnNum;
    [SerializeField] private Text Timer;
    [SerializeField] private Text WaveCounter;
    private int SpawnedNum;
    private int WaveNum = 1;
    private float WavePeriod;
    private float WavePeriodTime = 5;
    private float TimeBtwSpawn;
    void Start()
    {
        
    }
    void Update()
    {
        SpawnSystem();
        Timer.text = ((int)WavePeriod).ToString();
        WaveCounter.text = WaveNum.ToString();
    }
    private void SpawnSystem()
    {
        if (WaveNum <= 5)
        {
            if (SpawnedNum < SpawnNum)
            {
                Spawn(Random.Range(0, TypeOfEnemy.Length));
                WavePeriod = WavePeriodTime;
            }
            else
            {
                WaveTimeCountDown();
            }
        } else if (WaveNum <= 10 && WaveNum > 5)
        {
            if (SpawnedNum < SpawnNum)
            {
                WavePeriod = WavePeriodTime;
            }
            else
            {
                WaveTimeCountDown();
            }
        }
    }
    private void Spawn(int Type)
    {
        if (TimeBtwSpawn <= 0)
        {
            Instantiate(TypeOfEnemy[Type], Spawners[Random.Range(0, Spawners.Length)].position, Quaternion.identity);
            SpawnedNum++;
            TimeBtwSpawn = SpawnPeriod;
        }
        else
        {
            TimeBtwSpawn -= Time.deltaTime;
        }
    }
    private void WaveTimeCountDown()
    {
        
        if(WavePeriod <= 0)
        {
            WaveNum++;
            SpawnNum += Random.Range(1, 3);
            SpawnedNum = 0;
        }
        else
        {
            WavePeriod -= Time.deltaTime;
        }

    }
}

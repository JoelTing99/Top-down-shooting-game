using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Transform[] Spawners;
    [SerializeField] private GameObject[] TypeOfEnemy;
    [SerializeField] private float SpawnPeriod;
    [SerializeField] private int SpawnNum;
    private int SpawnedNum;
    private bool StartSpawning = true;
    private int WaveNum = 1;
    private float WavePeriod = 5;
    private float TimeBtwSpawn;
    void Start()
    {
        
    }
    void Update()
    {
        SpawnSystem();
        Debug.Log(WaveNum);
    }
    private void SpawnSystem()
    {
        for (int i = 1; i < 101; i++)
        {
            if (WaveNum == i)
            {
                if (WaveNum <= 5 && StartSpawning)
                {
                    if (SpawnedNum < SpawnNum)
                    {
                        Spawn(0);
                    }
                    else
                    {
                        WaveNum++;
                        StartSpawning = false;
                        SpawnNum += Random.Range(1, 3);
                        WaveTimeCountDown(WavePeriod);
                    }
                } else if (WaveNum <= 10 && WaveNum > 5)
                {

                }
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
    private void WaveTimeCountDown(float time)
    {
        if(time <= 0)
        {
            StartSpawning = true;
            SpawnedNum = 0;
        }
        else
        {
            time -= Time.deltaTime;
        }

    }
}

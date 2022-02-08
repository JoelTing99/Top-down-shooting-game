using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<Transform> Spawners;
    [SerializeField] private GameObject[] TypeOfEnemy;
    [SerializeField] private int SpawnNum;
    [SerializeField] private GameObject Spawner;
    private Templates Templates;
    private GameObject Player;
    private bool CanSpawn;
    private string Timer;
    private string WaveCount;
    private int SpawnedNum;
    private int WaveNum = 1;
    private float SpawnPeriod;
    private float WavePeriod;
    private float WavePeriodTime = 30;
    private float TimeBtwSpawn;
    private int CubeNum, FurstumNum, DodecahedronNum, OctahedronNum, SmallStellatedNum;
    private void Start()
    {
        Templates = FindObjectOfType<Templates>();
        Player = GameObject.FindWithTag("Player");
        CubeNum = SpawnRateCalculate(0.25f);
        FurstumNum = SpawnRateCalculate(0.35f);
        DodecahedronNum = SpawnRateCalculate(0.1f);
        OctahedronNum = SpawnRateCalculate(0.2f);
        SmallStellatedNum = SpawnRateCalculate(0.1f);
        SpawnNum = CubeNum + FurstumNum + DodecahedronNum + OctahedronNum + SmallStellatedNum;
        Invoke("AddSpawner", 10);
    }
    void Update()
    {
        if(Player != null && CanSpawn)
        {
            SpawnSystem();
        }
    }
    private void SpawnSystem()
    {
        if (WaveNum <= 2)
        {
            int Index = 0;
            int Order = Random.Range(0, TypeOfEnemy.Length);
            switch (Order)
            {
                case 0:
                    if (FurstumNum != 0)
                    {
                        Index = 0;
                    }
                    break;
                case 1:
                    if (CubeNum != 0)
                    {
                        Index = 1;
                    }
                    break;
                case 2:
                    if (OctahedronNum != 0)
                    {
                        Index = 2;
                    }
                    break;
                case 3:
                    if (DodecahedronNum != 0)
                    {
                        Index = 3;
                    }
                    break;
                case 4:
                    if (SmallStellatedNum != 0)
                    {
                        Index = 4;
                    }
                    break;
            }
            SpawnPeriod = Random.Range(0.5f, 3f);
            Spawn(Index);
        }
    }
    private void Spawn(int Type)
    {
        if (SpawnedNum < SpawnNum)
        {
            if (TimeBtwSpawn <= 0)
            {
                Instantiate(TypeOfEnemy[Type], Spawners[Random.Range(0, Spawners.Count)].position, Quaternion.identity);
                SpawnedNum++;
                TimeBtwSpawn = SpawnPeriod;
                switch (Type)
                {
                    case 0:
                        FurstumNum--;
                        break;
                    case 1:
                        CubeNum--;
                        break;
                    case 2:
                        OctahedronNum--;
                        break;
                    case 3:
                        DodecahedronNum--;
                        break;
                    case 4:
                        SmallStellatedNum--;
                        break;
                }
            }
            else
            {
                TimeBtwSpawn -= Time.deltaTime;
            }
            WavePeriod = WavePeriodTime;
        }
        else
        {
            WaveTimeCountDown();
        }
    }
    private void WaveTimeCountDown()
    {
        if(WavePeriod <= 0)
        {
            WaveNum++;
            SpawnNum += Random.Range(1, 4);
            SpawnedNum = 0;
            CubeNum = SpawnRateCalculate(0.25f);
            FurstumNum = SpawnRateCalculate(0.35f);
            DodecahedronNum = SpawnRateCalculate(0.1f);
            OctahedronNum = SpawnRateCalculate(0.2f);
            SmallStellatedNum = SpawnRateCalculate(0.1f);
            SpawnNum = CubeNum + FurstumNum + DodecahedronNum + OctahedronNum + SmallStellatedNum;
        }
        else
        {
            WavePeriod -= Time.deltaTime;
        }
    }
    private int SpawnRateCalculate(float rate)
    {
        return Mathf.RoundToInt(SpawnNum * rate);
    }
    private void AddSpawner()
    {
        int RandNumber = Random.Range(Templates.Bridges.Count / 10, Templates.Bridges.Count);
        for (int i = 0; i < RandNumber; i++)
        {
            int RandLocation = Random.Range(10, Templates.Bridges.Count);
            GameObject spawner = Instantiate(Spawner, Templates.Bridges[RandLocation].transform.position, Quaternion.identity, transform);
            Spawners.Add(spawner.transform);
        }
        CanSpawn = true;
    }
    public string GetWaveTime()
    {
        Timer = ((int)WavePeriod).ToString();
        return Timer;
    }
    public string GetWaveCount()
    {
        WaveCount = WaveNum.ToString();
        return WaveCount;
    }
}

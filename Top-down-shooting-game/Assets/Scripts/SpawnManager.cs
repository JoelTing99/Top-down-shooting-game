using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public event EventHandler OnNextWave; 

    [SerializeField] private List<Transform> Spawners;
    [SerializeField] private GameObject Spaceship;
    [SerializeField] private float WaitTime;
    private float waitTime;
    private List<Vector3> SpawnedPos = new List<Vector3>();
    private Templates Templates;
    private GameObject Player;
    private string WaveCount;
    private int WaveNum = 0;
    private void Start()
    {
        waitTime = WaitTime;
        Templates = FindObjectOfType<Templates>();
        Player = GameObject.FindWithTag("Player");
    }
    void Update()
    {
        if(Player != null)
        {
            SpawnSystem();
        }
    }

    private void SpawnSystem()
    {
         if(Spawners.Count <= 0 )
         {
            if(waitTime <= 0)
            {
                WaveNum++;
                AddSpawner();
                waitTime = WaitTime;
                if(OnNextWave != null)
                {
                    OnNextWave(this, EventArgs.Empty);
                }
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
         }
    }
    private void AddSpawner()
    {
        for (int i = 0; i < WaveNum; i++)
        {
            int XPos = UnityEngine.Random.Range(-17, 17);
            int ZPos = UnityEngine.Random.Range(-12, 12);
            Vector3 SpawnPos = new Vector3(XPos, 20, ZPos);
            GameObject spaceship = Instantiate(Spaceship, SpawnPos, Quaternion.identity);
            Spawners.Add(spaceship.transform);
            spaceship.GetComponent<Spaceship>().target = new Vector3(XPos, 7, ZPos);
        }
        /*int RandNumber = Random.Range(1, (int)((float)Templates.Bridges.Count / 5));
        for (int i = 0; i < RandNumber; i++)
        {
            int RandLocation = Random.Range(10, Templates.Bridges.Count);
            Vector3 SpawnerPos = Templates.Bridges[RandLocation].transform.position;
            if (SpawnedPos.Contains(SpawnerPos)) return;
            Vector3 ShipPos = SpawnerPos + new Vector3(Random.Range(5, 10), Random.Range(12, 20), Random.Range(5, 10));
            GameObject spaceship = Instantiate(Spaceship, ShipPos, Quaternion.identity, transform);
            spaceship.GetComponent<Spaceship>().target = SpawnerPos + new Vector3(0, 7, 0);
            Spawners.Add(spaceship.transform);
            SpawnedPos.Add(SpawnerPos);
        } */
    }
    public string GetWaveCount()
    {
        WaveCount = WaveNum.ToString();
        return WaveCount;
    }
    public List<Transform> GetSpawners()
    {
        return Spawners;
    }
}

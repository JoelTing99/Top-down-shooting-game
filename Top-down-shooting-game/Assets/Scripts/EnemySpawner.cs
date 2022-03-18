using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] TypeOfEnemy;
    private SpawnManager spawnManager;
    private float TimeBtwSpawn;
    private float SpawnPeriod;
    private bool CanSpawn = false;

    public bool canSpawn 
    {
        get { return CanSpawn; }
        set { CanSpawn = value; } 
    }
    private void Start()
    {
        spawnManager = GetComponent<SpawnManager>();
    }
    private void Update()
    {
        if (CanSpawn)
        {
            Spawn();
        }
    }
    private void Spawn()
    {
        SpawnPeriod = Random.Range(3f, 5f);
        if (TimeBtwSpawn <= 0)
        {
            Vector3 SpawnPos = transform.position + new Vector3(Random.Range(1f, 2f), 0, Random.Range(1f, 2f));
            Instantiate(TypeOfEnemy[Random.Range(0, TypeOfEnemy.Length)], SpawnPos, Quaternion.identity);
            TimeBtwSpawn = SpawnPeriod;     
        }
        else
        {
            TimeBtwSpawn -= Time.deltaTime;
        }

    }
}

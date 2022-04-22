using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] TypeOfEnemy;
    [SerializeField] private GameObject[] SpawnEnemy;
    [SerializeField] private Transform EnemyHolder;
    private SpawnManager spawnManager;
    private GameObject Enemy;
    private Animator animator;
    private float TimeBtwSpawn;
    private float SpawnPeriod;
    private bool CanSpawn = false;
    private bool IsSpawning;
    private int RandNum;

    public bool canSpawn 
    {
        get { return CanSpawn; }
        set { CanSpawn = value; } 
    }
    private void Start()
    {
        spawnManager = GetComponent<SpawnManager>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (CanSpawn)
        {
            SpawnAnimation();
        }
        if (IsSpawning)
        {
            if(EnemyHolder.localScale.x <= 1)
            {
                EnemyHolder.localScale += new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);
            }
            Enemy.transform.position = Enemy.transform.position;
        }
    }
    private void SpawnAnimation()
    {
        SpawnPeriod = Random.Range(5f, 10f);
        if (TimeBtwSpawn <= 0)
        {
            animator.SetTrigger("Spawn");
            TimeBtwSpawn = SpawnPeriod;     
        }
        else
        {
            TimeBtwSpawn -= Time.deltaTime;
        }
    }   
    private void SpawnBegin()
    {
        RandNum = Random.Range(0, TypeOfEnemy.Length);
        Enemy = Instantiate(SpawnEnemy[RandNum], EnemyHolder.position, Quaternion.identity, EnemyHolder.transform);
        EnemyHolder.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        IsSpawning = true;
    }
    private void SpawnEnd()
    {
        IsSpawning = false;
        Enemy.transform.parent = null;
        Instantiate(TypeOfEnemy[RandNum], Enemy.transform.position, Enemy.transform.rotation);
        Destroy(Enemy);
        Enemy = null;
        EnemyHolder.localScale = Vector3.one;
    }
}

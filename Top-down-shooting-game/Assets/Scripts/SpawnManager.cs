using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<Transform> Spawners;
    [SerializeField] private GameObject Spaceship;
    private Templates Templates;
    private GameObject Player;
    private string WaveCount;
    private int WaveNum = 1;
    private void Start()
    {
        Templates = FindObjectOfType<Templates>();
        Player = GameObject.FindWithTag("Player");
        Invoke("AddSpawner", 5);
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

    }
    private void AddSpawner()
    {
        int RandNumber = Random.Range(1, (int)((float)Templates.Bridges.Count / 10));
        for (int i = 0; i < RandNumber; i++)
        {
            int RandLocation = Random.Range(10, Templates.Bridges.Count);
            Vector3 SpawnerPos = Templates.Bridges[RandLocation].transform.position;
            Vector3 ShipPos = SpawnerPos + new Vector3(Random.Range(5, 10), Random.Range(7, 12), Random.Range(5, 10));
            GameObject spaceship = Instantiate(Spaceship, ShipPos, Quaternion.identity, transform);
            spaceship.GetComponent<Spaceship>().target = SpawnerPos + new Vector3(0, 7, 0);
            Spawners.Add(spaceship.transform);
        }
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

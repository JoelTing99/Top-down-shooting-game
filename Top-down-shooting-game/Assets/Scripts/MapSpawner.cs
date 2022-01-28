using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawner : MonoBehaviour
{
    [SerializeField] private int OpenDirection;
    private bool spawned = false;
    private Templates Templates;
    private int rand;
    private void Start()
    {
        Templates = FindObjectOfType<Templates>();
        if (gameObject.CompareTag("SpawnPoint"))
        {
            if(Random.value > 0.3f)
            {
                Invoke("SpawnRoom", 0.5f);
            }else
            {
                //transform.position -= 2 * transform.forward;
                Invoke("SpawnConnection", 0.5f);
            }
        }
        else
        {
            Invoke("SpawnConnection", 0.5f);
        }
        //Destroy(gameObject, 10);
    }
    private void SpawnRoom()
    {
        if(spawned == false)
        {
            switch (OpenDirection)
            {
                case 1:
                    rand = Random.Range(0, Templates.TopRoom.Length);
                    Instantiate(Templates.TopRoom[rand], transform.position, Templates.TopRoom[rand].transform.rotation);
                    break;
                case 2:
                    rand = Random.Range(0, Templates.RightRoom.Length);
                    Instantiate(Templates.RightRoom[rand], transform.position, Templates.RightRoom[rand].transform.rotation);
                    break;
                case 3:
                    rand = Random.Range(0, Templates.BottomRoom.Length);
                    Instantiate(Templates.BottomRoom[rand], transform.position, Templates.BottomRoom[rand].transform.rotation);
                    break;
                case 4:
                    rand = Random.Range(0, Templates.LeftRoom.Length);
                    Instantiate(Templates.LeftRoom[rand], transform.position, Templates.LeftRoom[rand].transform.rotation);
                    break;
            }
            spawned = true;
        }
    }
    private void SpawnConnection()
    {
        if (spawned == false)
        {
            switch (OpenDirection)
            {
                case 1:
                    rand = Random.Range(0, Templates.TopConnect.Length);
                    Instantiate(Templates.TopConnect[rand], transform.position, Templates.TopConnect[rand].transform.rotation);
                    break;
                case 2:
                    rand = Random.Range(0, Templates.RightConnect.Length);
                    Instantiate(Templates.RightConnect[rand], transform.position, Templates.RightConnect[rand].transform.rotation);
                    break;
                case 3:
                    rand = Random.Range(0, Templates.BottomConnect.Length);
                    Instantiate(Templates.BottomConnect[rand], transform.position, Templates.BottomConnect[rand].transform.rotation);
                    break;
                case 4:
                    rand = Random.Range(0, Templates.LeftConnect.Length);
                    Instantiate(Templates.LeftConnect[rand], transform.position, Templates.LeftConnect[rand].transform.rotation);
                    break;
            }
            spawned = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpawnPoint") || other.CompareTag("ConnectPoint"))
        {
            MapSpawner Spawner = other.GetComponent<MapSpawner>();
            if (Spawner.spawned == false && spawned == false)
            {
                if(OpenDirection == 1 && Spawner.OpenDirection == 4)
                {
                    Quaternion rotation = Quaternion.Euler(0, 180 ,0);
                    Instantiate(Templates.L, transform.position, rotation);
                }
                if (OpenDirection == 4 && Spawner.OpenDirection == 1)
                {
                    Quaternion rotation = Quaternion.Euler(0, 180, 0);
                    Instantiate(Templates.L, transform.position, rotation);
                }
                if (OpenDirection == 1 && Spawner.OpenDirection == 2)
                {
                    Quaternion rotation = Quaternion.Euler(0, -90, 0);
                    Instantiate(Templates.L, transform.position, rotation);
                }
                if (OpenDirection == 2 && Spawner.OpenDirection == 1)
                {
                    Quaternion rotation = Quaternion.Euler(0, -90, 0);
                    Instantiate(Templates.L, transform.position, rotation);
                }
                if (OpenDirection == 3 && Spawner.OpenDirection == 4)
                {
                    Quaternion rotation = Quaternion.Euler(0, 90, 0);
                    Instantiate(Templates.L, transform.position, rotation);
                }
                if (OpenDirection == 4 && Spawner.OpenDirection == 3)
                {
                    Quaternion rotation = Quaternion.Euler(0, 90, 0);
                    Instantiate(Templates.L, transform.position, rotation);
                }
                if (OpenDirection == 3 && Spawner.OpenDirection == 2)
                {
                    Instantiate(Templates.L, transform.position, Quaternion.identity);
                }
                if (OpenDirection == 2 && Spawner.OpenDirection == 3)
                {
                    Instantiate(Templates.L, transform.position, Quaternion.identity);
                }

                //Destroy(gameObject);
            }
            spawned = true;
        }
    }
}

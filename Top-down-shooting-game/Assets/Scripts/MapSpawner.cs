using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawner : MonoBehaviour
{
    [SerializeField] private int OpenDirection;
    [SerializeField] private bool spawned = false;
    private Templates Templates;
    private Vector3 OriginalPosition;
    private int rand;
    private Vector3 TVector = new Vector3(0, 0, 12);
    private Vector3 BVector = new Vector3(0, 0, -12);
    private Vector3 LVector = new Vector3(-12, 0, 0);
    private Vector3 RVector = new Vector3(12, 0, 0);
    private bool TDetected, LDetected, BDetected, RDetected;
    private void Awake()
    {
        OriginalPosition = transform.position;
        StartCoroutine(Detect());
    }
    private void Start()
    {
        Templates = FindObjectOfType<Templates>();
        if (gameObject.CompareTag("SpawnPoint"))
        {
            if(Random.value > 0.3f)
            {
                Invoke("SpawnRoom", 1f);
            }else
            {
                //transform.position -= 2 * transform.forward;
                Invoke("SpawnConnection", 1f);
            }
        }
        else
        {
            Invoke("SpawnConnection", 1f);
        }
        //Destroy(gameObject, 10);
    }
    private IEnumerator Detect()
    {
        transform.position += TVector;
        yield return new WaitForSeconds(0.1f);
        transform.position = OriginalPosition;
        transform.position += BVector;
        yield return new WaitForSeconds(0.1f);
        transform.position = OriginalPosition;
        transform.position += RVector;
        yield return new WaitForSeconds(0.1f);
        transform.position = OriginalPosition;
        transform.position += LVector;
        yield return new WaitForSeconds(0.1f);
        transform.position = OriginalPosition;
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
                    if (LDetected && RDetected && TDetected)
                    {
                        rand = Random.Range(0, Templates.TopRoom.Length);
                        Instantiate(Templates.TopRoom[rand], transform.position, Templates.TopRoom[rand].transform.rotation);
                    }
                    else if (LDetected && RDetected && !TDetected)
                    {
                        rand = Random.Range(0, Templates.TopConnect.Length);
                        Instantiate(Templates.TopConnect[rand], transform.position, Templates.TopConnect[rand].transform.rotation);
                    }
                    else if(LDetected && TDetected && !RDetected)
                    {
                        rand = Random.Range(0, Templates.L_TRConnect.Length);
                        Instantiate(Templates.L_TRConnect[rand], transform.position, Templates.L_TRConnect[rand].transform.rotation);
                    }
                    else if (LDetected && !TDetected && !RDetected)
                    {
                        if(Random.value > 0.5)
                        {
                            rand = Random.Range(0, Templates.L_TRConnect.Length);
                            Instantiate(Templates.L_TRConnect[rand], transform.position, Templates.L_TRConnect[rand].transform.rotation);
                        }
                        else
                        {
                            rand = Random.Range(0, Templates.T_TRConnect.Length);
                            Instantiate(Templates.T_TRConnect[rand], transform.position, Templates.T_TRConnect[rand].transform.rotation);
                        }
                    }
                    else if(RDetected && TDetected && !LDetected)
                    {
                        rand = Random.Range(0, Templates.L_TLConnect.Length);
                        Instantiate(Templates.L_TLConnect[rand], transform.position, Templates.L_TLConnect[rand].transform.rotation);
                    }
                    else if (RDetected && !TDetected && !LDetected)
                    {
                        if(Random.value > 0.5)
                        {
                            rand = Random.Range(0, Templates.L_TLConnect.Length);
                            Instantiate(Templates.L_TLConnect[rand], transform.position, Templates.L_TLConnect[rand].transform.rotation);
                        }
                        else
                        {
                            rand = Random.Range(0, Templates.T_TLConnect.Length);
                            Instantiate(Templates.T_TLConnect[rand], transform.position, Templates.T_TLConnect[rand].transform.rotation);
                        }
                    }
                    else if(!TDetected && !RDetected && !LDetected)
                    {
                        rand = Random.Range(0, 4);
                        switch (rand)
                        {
                            case 0:
                                rand = Random.Range(0, Templates.TopConnect.Length);
                                Instantiate(Templates.TopConnect[rand], transform.position, Templates.TopConnect[rand].transform.rotation);
                                break;
                            case 1:
                                rand = Random.Range(0, Templates.L_TRConnect.Length);
                                Instantiate(Templates.L_TRConnect[rand], transform.position, Templates.L_TRConnect[rand].transform.rotation);
                                break;
                            case 2:
                                rand = Random.Range(0, Templates.L_TLConnect.Length);
                                Instantiate(Templates.L_TLConnect[rand], transform.position, Templates.L_TLConnect[rand].transform.rotation);
                                break;
                            case 3:
                                rand = Random.Range(0, Templates.T_TRConnect.Length);
                                Instantiate(Templates.T_TRConnect[rand], transform.position, Templates.T_TRConnect[rand].transform.rotation);
                                break;
                            case 4:
                                rand = Random.Range(0, Templates.T_TLConnect.Length);
                                Instantiate(Templates.T_TLConnect[rand], transform.position, Templates.T_TLConnect[rand].transform.rotation);
                                break;
                        }
                    }
                    break;
                case 2:
                    if (BDetected && RDetected && TDetected)
                    {
                        rand = Random.Range(0, Templates.RightRoom.Length);
                        Instantiate(Templates.RightRoom[rand], transform.position, Templates.RightRoom[rand].transform.rotation);
                    }
                    else if (BDetected && !RDetected && TDetected)
                    {
                        rand = Random.Range(0, Templates.RightConnect.Length);
                        Instantiate(Templates.RightConnect[rand], transform.position, Templates.RightConnect[rand].transform.rotation);
                    }
                    else if (RDetected && TDetected && !BDetected)
                    {
                        rand = Random.Range(0, Templates.L_RBConnect.Length);
                        Instantiate(Templates.L_RBConnect[rand], transform.position, Templates.L_RBConnect[rand].transform.rotation);
                    }
                    else if (!RDetected && TDetected && !BDetected)
                    {
                        if (Random.value > 0.5)
                        {
                            rand = Random.Range(0, Templates.L_RBConnect.Length);
                            Instantiate(Templates.L_RBConnect[rand], transform.position, Templates.L_RBConnect[rand].transform.rotation);
                        }
                        else
                        {
                            rand = Random.Range(0, Templates.T_RBConnect.Length);
                            Instantiate(Templates.T_RBConnect[rand], transform.position, Templates.T_RBConnect[rand].transform.rotation);
                        }
                    }
                    else if (RDetected && BDetected && !TDetected)
                    {
                        rand = Random.Range(0, Templates.L_RTConnect.Length);
                        Instantiate(Templates.L_RTConnect[rand], transform.position, Templates.L_RTConnect[rand].transform.rotation);
                    }
                    else if (!RDetected && BDetected && !TDetected)
                    {
                        if (Random.value > 0.5)
                        {
                            rand = Random.Range(0, Templates.L_RTConnect.Length);
                            Instantiate(Templates.L_RTConnect[rand], transform.position, Templates.L_RTConnect[rand].transform.rotation);
                        }
                        else
                        {
                            rand = Random.Range(0, Templates.T_RTConnect.Length);
                            Instantiate(Templates.T_RTConnect[rand], transform.position, Templates.T_RTConnect[rand].transform.rotation);
                        }
                    }
                    else if (!TDetected && !RDetected && !BDetected)
                    {
                        rand = Random.Range(0, 4);
                        switch (rand)
                        {
                            case 0:
                                rand = Random.Range(0, Templates.RightConnect.Length);
                                Instantiate(Templates.RightConnect[rand], transform.position, Templates.RightConnect[rand].transform.rotation);
                                break;
                            case 1:
                                rand = Random.Range(0, Templates.L_RBConnect.Length);
                                Instantiate(Templates.L_RBConnect[rand], transform.position, Templates.L_RBConnect[rand].transform.rotation);
                                break;
                            case 2:
                                rand = Random.Range(0, Templates.L_RTConnect.Length);
                                Instantiate(Templates.L_RTConnect[rand], transform.position, Templates.L_RTConnect[rand].transform.rotation);
                                break;
                            case 3:
                                rand = Random.Range(0, Templates.T_RBConnect.Length);
                                Instantiate(Templates.T_RBConnect[rand], transform.position, Templates.T_RBConnect[rand].transform.rotation);
                                break;
                            case 4:
                                rand = Random.Range(0, Templates.T_RTConnect.Length);
                                Instantiate(Templates.T_RTConnect[rand], transform.position, Templates.T_RTConnect[rand].transform.rotation);
                                break;
                        }
                    }
                    break;
                case 3:
                    if (LDetected && RDetected && BDetected)
                    {
                        rand = Random.Range(0, Templates.BottomRoom.Length);
                        Instantiate(Templates.BottomRoom[rand], transform.position, Templates.BottomRoom[rand].transform.rotation);
                    }
                    else if (LDetected && RDetected && !BDetected)
                    {
                        rand = Random.Range(0, Templates.BottomConnect.Length);
                        Instantiate(Templates.BottomConnect[rand], transform.position, Templates.BottomConnect[rand].transform.rotation);
                    }
                    else if (LDetected && BDetected && !RDetected)
                    {
                        rand = Random.Range(0, Templates.L_BRConnect.Length);
                        Instantiate(Templates.L_BRConnect[rand], transform.position, Templates.L_BRConnect[rand].transform.rotation);
                    }
                    else if (LDetected && !BDetected && !RDetected)
                    {
                        if (Random.value > 0.5)
                        {
                            rand = Random.Range(0, Templates.L_BRConnect.Length);
                            Instantiate(Templates.L_BRConnect[rand], transform.position, Templates.L_BRConnect[rand].transform.rotation);
                        }
                        else
                        {
                            rand = Random.Range(0, Templates.T_BRConnect.Length);
                            Instantiate(Templates.T_BRConnect[rand], transform.position, Templates.T_BRConnect[rand].transform.rotation);
                        }
                    }
                    else if (RDetected && BDetected && !LDetected)
                    {
                        rand = Random.Range(0, Templates.L_BLConnect.Length);
                        Instantiate(Templates.L_BLConnect[rand], transform.position, Templates.L_BLConnect[rand].transform.rotation);
                    }
                    else if (RDetected && !BDetected && !LDetected)
                    {
                        if (Random.value > 0.5)
                        {
                            rand = Random.Range(0, Templates.L_BLConnect.Length);
                            Instantiate(Templates.L_BLConnect[rand], transform.position, Templates.L_BLConnect[rand].transform.rotation);
                        }
                        else
                        {
                            rand = Random.Range(0, Templates.T_BLConnect.Length);
                            Instantiate(Templates.T_BLConnect[rand], transform.position, Templates.T_BLConnect[rand].transform.rotation);
                        }
                    }
                    else if (!BDetected && !RDetected && !LDetected)
                    {
                        rand = Random.Range(0, 4);
                        switch (rand)
                        {
                            case 0:
                                rand = Random.Range(0, Templates.BottomRoom.Length);
                                Instantiate(Templates.BottomRoom[rand], transform.position, Templates.BottomRoom[rand].transform.rotation);
                                break;
                            case 1:
                                rand = Random.Range(0, Templates.L_BRConnect.Length);
                                Instantiate(Templates.L_BRConnect[rand], transform.position, Templates.L_BRConnect[rand].transform.rotation);
                                break;
                            case 2:
                                rand = Random.Range(0, Templates.L_BLConnect.Length);
                                Instantiate(Templates.L_BLConnect[rand], transform.position, Templates.L_BLConnect[rand].transform.rotation);
                                break;
                            case 3:
                                rand = Random.Range(0, Templates.T_BRConnect.Length);
                                Instantiate(Templates.T_BRConnect[rand], transform.position, Templates.T_BRConnect[rand].transform.rotation);
                                break;
                            case 4:
                                rand = Random.Range(0, Templates.T_BLConnect.Length);
                                Instantiate(Templates.T_BLConnect[rand], transform.position, Templates.T_BLConnect[rand].transform.rotation);
                                break;
                        }
                    }
                    break;
                case 4:
                    if (BDetected && LDetected && TDetected)
                    {
                        rand = Random.Range(0, Templates.LeftRoom.Length);
                        Instantiate(Templates.LeftRoom[rand], transform.position, Templates.LeftRoom[rand].transform.rotation);
                    }
                    else if (BDetected && !LDetected && TDetected)
                    {
                        rand = Random.Range(0, Templates.LeftConnect.Length);
                        Instantiate(Templates.LeftConnect[rand], transform.position, Templates.LeftConnect[rand].transform.rotation);
                    }
                    else if (LDetected && TDetected && !BDetected)
                    {
                        rand = Random.Range(0, Templates.L_LBConnect.Length);
                        Instantiate(Templates.L_LBConnect[rand], transform.position, Templates.L_LBConnect[rand].transform.rotation);
                    }
                    else if (!LDetected && TDetected && !BDetected)
                    {
                        if (Random.value > 0.5)
                        {
                            rand = Random.Range(0, Templates.L_LBConnect.Length);
                            Instantiate(Templates.L_LBConnect[rand], transform.position, Templates.L_LBConnect[rand].transform.rotation);
                        }
                        else
                        {
                            rand = Random.Range(0, Templates.T_LBConnect.Length);
                            Instantiate(Templates.T_LBConnect[rand], transform.position, Templates.T_LBConnect[rand].transform.rotation);
                        }
                    }
                    else if (LDetected && BDetected && !TDetected)
                    {
                        rand = Random.Range(0, Templates.L_LTConnect.Length);
                        Instantiate(Templates.L_LTConnect[rand], transform.position, Templates.L_LTConnect[rand].transform.rotation);
                    }
                    else if (!LDetected && BDetected && !TDetected)
                    {
                        if (Random.value > 0.5)
                        {
                            rand = Random.Range(0, Templates.L_LTConnect.Length);
                            Instantiate(Templates.L_LTConnect[rand], transform.position, Templates.L_LTConnect[rand].transform.rotation);
                        }
                        else
                        {
                            rand = Random.Range(0, Templates.T_LTConnect.Length);
                            Instantiate(Templates.T_LTConnect[rand], transform.position, Templates.T_LTConnect[rand].transform.rotation);
                        }
                    }
                    else if (!TDetected && !LDetected && !BDetected)
                    {
                        rand = Random.Range(0, 4);
                        switch (rand)
                        {
                            case 0:
                                rand = Random.Range(0, Templates.LeftConnect.Length);
                                Instantiate(Templates.LeftConnect[rand], transform.position, Templates.LeftConnect[rand].transform.rotation);
                                break;
                            case 1:
                                rand = Random.Range(0, Templates.L_LBConnect.Length);
                                Instantiate(Templates.L_LBConnect[rand], transform.position, Templates.L_LBConnect[rand].transform.rotation);
                                break;
                            case 2:
                                rand = Random.Range(0, Templates.L_LTConnect.Length);
                                Instantiate(Templates.L_LTConnect[rand], transform.position, Templates.L_LTConnect[rand].transform.rotation);
                                break;
                            case 3:
                                rand = Random.Range(0, Templates.T_LBConnect.Length);
                                Instantiate(Templates.T_LBConnect[rand], transform.position, Templates.T_LBConnect[rand].transform.rotation);
                                break;
                            case 4:
                                rand = Random.Range(0, Templates.T_LTConnect.Length);
                                Instantiate(Templates.T_LTConnect[rand], transform.position, Templates.T_LTConnect[rand].transform.rotation);
                                break;
                        }
                    }
                    break;
            }
            spawned = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(transform.position != OriginalPosition)
        {
            if (transform.position == OriginalPosition + TVector)
            {
                TDetected = true;
            }
            if (transform.position == OriginalPosition + BVector)
            {
                BDetected = true;
            }
            if (transform.position == OriginalPosition + LVector)
            {
                LDetected = true;
            }
            if (transform.position == OriginalPosition + RVector)
            {
                RDetected = true;
            }
        }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusItemSpawner : MonoBehaviour
{
    private GameManager GameManager;
    void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
        if (Random.value <= 0.5f)
        {
            Vector3 Position = transform.position;
            Position.y = 0.5f;
            Instantiate(GameManager.GetRandomBonusItem(), Position, Quaternion.identity);
        }
    }

}

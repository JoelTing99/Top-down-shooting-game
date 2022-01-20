using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpPack : MonoBehaviour
{
    private GameManager GameManager;
    void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.StartSpeedUp();
            Destroy(gameObject);
        }
    }
}

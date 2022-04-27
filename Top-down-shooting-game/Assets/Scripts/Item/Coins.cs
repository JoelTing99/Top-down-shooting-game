using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class Coins : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private VisualEffect CollectEffect;
    private VisualEffect collectEffect;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.SetParent(GameObject.FindWithTag("CameraHolder").transform);
            GetComponent<Rigidbody>().useGravity = false;
            collectEffect = Instantiate(CollectEffect, GameObject.FindWithTag("CameraHolder").transform);
            collectEffect.resetSeedOnPlay = true;
            collectEffect.SetVector3("SpawnPos", transform.localPosition);
            gameManager.AddCoin(1);
            Destroy(collectEffect.gameObject, 3);
            Destroy(gameObject);
        }
    }
}

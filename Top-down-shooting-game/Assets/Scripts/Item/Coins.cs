using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    private GameManager GameManager;
    private bool Collected = false;
    void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        if(Collected)
        {
            MovetoPoint(new Vector3(3.1f, 2f, 6f));
        }
    }
    private void MovetoPoint(Vector3 point)
    {
         if(transform.localPosition != point)
         {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, point, 0.4f);
         }
         else
         {
            GameManager.AddCoin(1);
            Destroy(gameObject);
         }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.SetParent(GameObject.FindWithTag("CameraHolder").transform);
            GetComponent<Rigidbody>().useGravity = false;
            Collected = true;
        }
    }
}

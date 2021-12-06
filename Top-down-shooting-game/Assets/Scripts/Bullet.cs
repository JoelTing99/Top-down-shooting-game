using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rb; 
    [SerializeField]
    private float Speed;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}

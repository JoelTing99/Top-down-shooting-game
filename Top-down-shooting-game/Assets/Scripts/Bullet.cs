using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Bullet : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]
    private float Speed;
    [SerializeField]
    private VisualEffect HitEffect;
    [SerializeField]
    private VisualEffect FlyEffect;
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
        if (!other.CompareTag("Player") || !other.CompareTag("Bullet"))
        {
            Speed = 0f;
            FlyEffect.Stop();
            HitEffect.SendEvent("Hit");
            
            transform.SetParent(other.transform);
            Destroy(gameObject, 3f);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}

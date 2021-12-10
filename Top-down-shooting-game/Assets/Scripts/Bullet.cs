using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Bullet : MonoBehaviour
{
    private Rigidbody rb;
    private Transform currentTransform;
    [SerializeField]
    private float Speed;
    [SerializeField]
    private VisualEffect HitEffect;
    [SerializeField]
    private VisualEffect FlyEffect;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        currentTransform = transform;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            HitEffect.SendEvent("Hit");
            if (!other.CompareTag("Wall"))
            {
                transform.position = currentTransform.position;
                transform.SetParent(other.transform);
            }
            FlyEffect.Stop();
            Speed = 0f;
            Destroy(gameObject, 3f);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float Speed;
    [SerializeField]
    private VisualEffect HitEffect;
    [SerializeField]
    private VisualEffect FlyEffect;
    private void Awake()
    {
    }

    void Update()
    {

        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("PlayerBullet"))
        {
            Destroy(gameObject);
        }
        else if (other.CompareTag("Wall"))
        {
            Speed = 0f;
            FlyEffect.Stop();
            HitEffect.SendEvent("Hit");
            Destroy(gameObject, 3f);
        }
        else
        {
            Speed = 0f;
            FlyEffect.Stop();
            HitEffect.SendEvent("Hit");
            transform.SetParent(other.transform);
            Destroy(gameObject, 3f);
        }
    }
}

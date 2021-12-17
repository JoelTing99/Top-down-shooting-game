using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private float Speed;
    private void Awake()
    {
        transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
    }
    private void Update()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }

    public void Charge()
    {
        Speed = 0f;
        StartCoroutine(Charging());
    }
    public IEnumerator Charging()
    {
        while(transform.localScale.x < 0.3f || transform.localScale.y < 0.3f || transform.localScale.z < 0.3f)
        {
            transform.localScale *= 1.1f;
            yield return new WaitForSeconds(0.1f);
        }
    }
    public void Fire()
    {
        StartCoroutine(FireChountDown());
    }
    private IEnumerator FireChountDown()
    {
        yield return new WaitForSeconds(2f);
        Speed = 10f;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}

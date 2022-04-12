using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private GameManager GameManager;
    private float Speed = 0;
    public bool Charging;
    private void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
        transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
    }
    private void Update()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        if (Charging)
        {
            Charge();
        }
    }

    public void Charge()
    {
        Speed = 0f;
        if(transform.localScale.x < 0.25f || transform.localScale.y < 0.25f || transform.localScale.z < 0.25f)
        {
            transform.localScale *= 1.05f;
        }
    }
    public void Fire()
    {
        Charging = false;
        Speed = 10f;
        transform.SetParent(null);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            GameManager.AttackPlayer(GameManager.GetOctahedronDamage());
        }else if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}

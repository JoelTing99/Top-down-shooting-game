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
        transform.position += transform.right * Speed * Time.deltaTime;
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
            transform.localScale *= 1.04f;
        }
    }
    public void Fire()
    {
        transform.SetParent(null);
        Charging = false;
        Speed = 10f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            GameManager.AttackPlayer(GameManager.GetOctahedronDamage());
        }
        else if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}

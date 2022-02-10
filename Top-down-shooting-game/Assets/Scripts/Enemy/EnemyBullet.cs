using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private GameManager GameManager;
    private float Speed = 0;
    private void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
        transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
    }
    private void Update()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }

    public IEnumerator Charge()
    {
        Speed = 0f;
        while(transform.localScale.x < 0.3f || transform.localScale.y < 0.3f || transform.localScale.z < 0.3f)
        {
            transform.localScale *= 1.02f;
            yield return null;
        }
    }
    public void Fire()
    {
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

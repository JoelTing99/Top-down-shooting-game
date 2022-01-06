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
    private GameManager GameManager;
    private void Awake()
    {
        GameManager = FindObjectOfType<GameManager>();
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
            switch (other.name)
            {
                case "Cube Enemy":
                    other.GetComponent<CubeEnemy>().TakeDamage(GameManager.GetPlayerDamage());
                    break;
                case "Dodecahedron Enemy":
                    other.GetComponent<DodecahedronEnemy>().TakeDamage(GameManager.GetPlayerDamage());
                    break;
                case "Frustum Enemy":
                    other.GetComponent<FurstumEnemy>().TakeDamage(GameManager.GetPlayerDamage());
                    break;
                case "Octahedron Enemy":
                    other.GetComponent<OctahedronEnemy>().TakeDamage(GameManager.GetPlayerDamage());
                    break;
                case "Small Stellated Enemy":
                    other.GetComponent<SmallStellatedEnemy>().TakeDamage(GameManager.GetPlayerDamage());
                    break;
            }
            Speed = 0f;
            FlyEffect.Stop();
            HitEffect.SendEvent("Hit");
            transform.SetParent(other.transform);
            Destroy(gameObject, 3f);
        }
    }
}

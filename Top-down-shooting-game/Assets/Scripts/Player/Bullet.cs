using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private VisualEffect HitEffect;
    [SerializeField] private VisualEffect FlyEffect;
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
        if (other.CompareTag("Wall"))
        {
            Speed = 0f;
            FlyEffect.Stop();
            HitEffect.SendEvent("Hit");
            Destroy(gameObject, 3f);
        }
        else if(!other.isTrigger)
        {
            float PlayerDamage;
            if(Random.value >= GameManager.GetPlayerCritRate())
            {
                PlayerDamage = GameManager.GetPlayerDamage();
            }else
            {
                PlayerDamage = GameManager.GetPlayerDamage() * GameManager.GetPlayerCritDamageRate();
                Debug.Log("Crit!");
            }
            if(Random.value <= GameManager.GetPlayerHeadShotRate())
            {
                PlayerDamage = 9999;
            }
            switch (other.tag)
            {
                case "CubeEnemy":
                    other.GetComponent<CubeEnemy>().TakeDamage(PlayerDamage);
                    break;
                case "DodecahedronEnemy":
                    other.GetComponent<DodecahedronEnemy>().TakeDamage(PlayerDamage);
                    break;
                case "FrustumEnemy":
                    other.GetComponent<FurstumEnemy>().TakeDamage(PlayerDamage);
                    break;
                case "OctahedronEnemy":
                    other.GetComponent<OctahedronEnemy>().TakeDamage(PlayerDamage);
                    break;
                case "SmallStellatedEnemy":
                    other.GetComponent<SmallStellatedEnemy>().TakeDamage(PlayerDamage);
                    break;
            }
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

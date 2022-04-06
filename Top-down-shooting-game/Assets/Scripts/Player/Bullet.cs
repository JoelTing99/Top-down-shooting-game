using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float Speed;
    //[SerializeField] private VisualEffect HitEffect;
    //[SerializeField] private VisualEffect FlyEffect;
    private GameManager GameManager;
    private HealthSystem HealthSystem;
    private void Awake()
    {
        GameManager = FindObjectOfType<GameManager>();
        HealthSystem = GameManager.GetPlayerHealthSystem();
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            //FlyEffect.Stop();
            //HitEffect.SendEvent("Hit");
        }
        else if(!other.isTrigger)
        {
            float PlayerDamage = GameManager.GetPlayerDamage();
            if(GameManager.GetHaveLifeSteal()) {
                HealthSystem.Heal(GameManager.GetPlayerDamage() * GameManager.GetPlayerLifeStealRate());
            }
            if(Random.value <= GameManager.GetPlayerCritRate())
            {
                PlayerDamage = GameManager.GetPlayerDamage() * GameManager.GetPlayerCritDamageRate();
                Debug.Log("Crit!");
            }
            if(Random.value <= GameManager.GetPlayerHeadShootRate())
            {
                PlayerDamage = 10000;
                Debug.Log("HeadShot");
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
                case "EnemyShip":
                    other.GetComponent<Spaceship>().TakeDamage(PlayerDamage);
                    break;
            }
            if(other.GetComponent<Rigidbody>() != null)
            {
                other.GetComponent<Rigidbody>().AddForce(-other.transform.forward * 2, ForceMode.Impulse);
            }
            //FlyEffect.Stop();
            //HitEffect.SendEvent("Hit");

        }
        else
        {
            Destroy(gameObject);
        }
    }
}

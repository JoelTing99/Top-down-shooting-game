using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private VisualEffect HitEffect;
    private GameManager GameManager;
    private HealthSystem HealthSystem;
    private void Awake()
    {
        GameManager = FindObjectOfType<GameManager>();
        HealthSystem = GameManager.GetPlayerHealthSystem();
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * Speed * Time.deltaTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        bool Popuped = false;
        float PlayerDamage = GameManager.GetPlayerDamage();
        ContactPoint Contact = collision.contacts[0];
        Quaternion Rot = Quaternion.FromToRotation(Vector3.up, -Contact.normal);
        VisualEffect Hiteffect = Instantiate(HitEffect, Contact.point, Rot);
        if (GameManager.GetHaveLifeSteal())
        {
            float StealAmount = GameManager.GetPlayerDamage() * GameManager.GetPlayerLifeStealRate();
            HealthSystem.Heal(StealAmount);
            Textpopup.Create(FindObjectOfType<Player>().transform.position + new Vector3(0, 2, 0), (int)StealAmount, Color.red);
        }
        if (Random.value <= GameManager.GetPlayerCritRate())
        {
            PlayerDamage = GameManager.GetPlayerDamage() * GameManager.GetPlayerCritDamageRate();
            if (!Popuped)
            {
                Textpopup.Create(Contact.point, (int)PlayerDamage, Color.red);
                Popuped = true;
            }
            Debug.Log("Crit!");
        }
        if (Random.value <= GameManager.GetPlayerHeadShootRate())
        {
            PlayerDamage = 10000;
            if (!Popuped)
            {
                Textpopup.Create(Contact.point, "HeadShot", Color.red);
                Popuped = true;
            }
            Debug.Log("HeadShot");
        }
        if (!Popuped)
        {
            Textpopup.Create(Contact.point, (int)PlayerDamage, Color.white);
        }
        switch (collision.collider.tag)
        {
            case "CubeEnemy":
                collision.collider.GetComponent<CubeEnemy>().TakeDamage(PlayerDamage);
                break;
            case "DodecahedronEnemy":
                collision.collider.GetComponent<DodecahedronEnemy>().TakeDamage(PlayerDamage);
                break;
            case "FrustumEnemy":
                collision.collider.GetComponent<FurstumEnemy>().TakeDamage(PlayerDamage);
                break;
            case "OctahedronEnemy":
                collision.collider.GetComponent<OctahedronEnemy>().TakeDamage(PlayerDamage);
                break;
            case "SmallStellatedEnemy":
                collision.collider.GetComponent<SmallStellatedEnemy>().TakeDamage(PlayerDamage);
                break;
            case "EnemyShip":
                collision.collider.GetComponent<Spaceship>().TakeDamage(PlayerDamage);
                break;
        }
        if (collision.collider.GetComponent<Rigidbody>() != null)
        {
            Transform colliderTransform = collision.collider.transform;
            colliderTransform.position -= colliderTransform.forward * 10 * Time.deltaTime;
            colliderTransform.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        if (collision.collider.GetComponent<Material>() != null)
        {
            HitEffect.SetVector4("HitedColor", collision.collider.GetComponent<Material>().color);
        }
          
        Destroy(Hiteffect.gameObject, 1);
        Destroy(gameObject);

    }
}

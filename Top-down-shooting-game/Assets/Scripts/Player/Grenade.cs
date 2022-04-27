using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Grenade : MonoBehaviour
{
    [SerializeField] private float ExplodeTime;
    [SerializeField] private float ExplodeForce;
    [SerializeField] VisualEffect ExplosionEffect;
    private float GrenadeDamage;
    private bool HasExploded;
    private GameManager GameManager;

    private void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
        GrenadeDamage = GameManager.GetGrenadeDamage();
    }
    void Update()
    {
        ExplodeTime -= Time.deltaTime;
        if(ExplodeTime <= 0 && !HasExploded)
        {
            Explode();
            HasExploded = true;
        }
    }
    private void Explode()
    {
        Collider[] Hit = Physics.OverlapSphere(transform.position, GameManager.GetGrenadeExplodeRadius());
        foreach (var hit in Hit)
        {
            switch (hit.tag)
            {
                case "CubeEnemy":
                    hit.GetComponent<CubeEnemy>().TakeDamage(GrenadeDamage);
                    break;
                case "DodecahedronEnemy":
                    hit.GetComponent<DodecahedronEnemy>().TakeDamage(GrenadeDamage);
                    break;
                case "FrustumEnemy":
                    hit.GetComponent<FurstumEnemy>().TakeDamage(GrenadeDamage);
                    break;
                case "OctahedronEnemy":
                    hit.GetComponent<OctahedronEnemy>().TakeDamage(GrenadeDamage);
                    break;
                case "SmallStellatedEnemy":
                    hit.GetComponent<SmallStellatedEnemy>().TakeDamage(GrenadeDamage);
                    break;
            }
            if (hit.GetComponent<Rigidbody>() != null)
            {
                hit.GetComponent<Rigidbody>().AddExplosionForce(ExplodeForce, transform.position, GameManager.GetGrenadeExplodeRadius());
            }
        }
        VisualEffect explosionEffect = Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
        Destroy(explosionEffect.gameObject, 5);
        Destroy(gameObject);
    }
}

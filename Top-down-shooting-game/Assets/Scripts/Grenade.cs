using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField]
    private float ExplodeTime;
    [SerializeField]
    private float ExplodeRadius;
    [SerializeField]
    private float ExplodeForce;
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
        Collider[] Hit = Physics.OverlapSphere(transform.position, ExplodeRadius);
        foreach (var hit in Hit)
        {
            switch (hit.name)
            {
                case "Cube Enemy":
                    hit.GetComponent<CubeEnemy>().TakeDamage(GrenadeDamage);
                    break;
                case "Dodecahedron Enemy":
                    hit.GetComponent<DodecahedronEnemy>().TakeDamage(GrenadeDamage);
                    break;
                case "Frustum Enemy":
                    hit.GetComponent<FurstumEnemy>().TakeDamage(GrenadeDamage);
                    break;
                case "Octahedron Enemy":
                    hit.GetComponent<OctahedronEnemy>().TakeDamage(GrenadeDamage);
                    break;
                case "Small Stellated Enemy":
                    hit.GetComponent<SmallStellatedEnemy>().TakeDamage(GrenadeDamage);
                    break;
            }
            if (hit.GetComponent<Rigidbody>() != null)
            {
                hit.GetComponent<Rigidbody>().AddExplosionForce(ExplodeForce, transform.position, 1);
            }
        }
        Destroy(gameObject);
    }
}

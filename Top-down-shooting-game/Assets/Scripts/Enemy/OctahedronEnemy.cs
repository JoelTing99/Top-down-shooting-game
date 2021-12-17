using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctahedronEnemy : MonoBehaviour
{
    private Animator Animator;
    [SerializeField]
    private GameObject Bullet;
    [SerializeField]
    private Transform FirePoint;
    [SerializeField]
    private bool Fire;
    [SerializeField]
    private bool Charging;
    private int BulletCount;
    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }


    private void Update()
    {
        Attact();
    }
    private void Attact()
    {
        if(!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), 5f, 6)){
            bool HitPlayer = false;
            RaycastHit[] Hit = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), 5f);
            foreach (var hit in Hit)
            {
                HitPlayer = true;
            }
            if (HitPlayer)
            {
                Animator.SetBool("IsAttack", true);
            }
            else
            {
                Animator.SetBool("IsAttack", false);
            }
        }
        else
        {
            Animator.SetBool("IsAttack", false);
        }

        if (Charging && BulletCount < 1)
        {
            GameObject bullet = Instantiate(Bullet, FirePoint.position, transform.rotation, transform);
            bullet.GetComponent<EnemyBullet>().Charge();
            bullet.GetComponent<EnemyBullet>().Fire();
            BulletCount++;
        }
        if (Fire)
        {
            BulletCount = 0;
        }
    }

}

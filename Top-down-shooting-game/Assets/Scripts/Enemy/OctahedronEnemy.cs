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
    private GameObject bullet;
    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }


    private void Update()
    {
        Attact();
        FireAction();
    }
    private void Attact()
    {
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit HIT, 5f) && !HIT.collider.CompareTag("Wall"))
        {
            bool HitPlayer = false;
            RaycastHit[] Hit = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), 5f);
            foreach (var hit in Hit)
            {
                HitPlayer = true;
            }
            if (HitPlayer)
            {
                Animator.SetBool("IsAttack", true);
                if (BulletCount < 1 && Charging)
                {
                    bullet = Instantiate(Bullet, FirePoint.position, transform.rotation, transform);
                    BulletCount++;
                }
            }
            else
            {
                Animator.SetBool("IsAttack", false);
                Destroy(bullet, 5f);
                BulletCount = 0;
            }
        }
        else
        {
            Animator.SetBool("IsAttack", false);
            Destroy(bullet, 5f);
            BulletCount = 0;
        }
    }
    private void FireAction()
    {
        if (bullet != null)
        {
            if (Charging)
            {
                bullet.GetComponent<EnemyBullet>().IsCharging = true;
            }
            else
            {
                bullet.GetComponent<EnemyBullet>().IsCharging = false;
            }
            if (Fire)
            {
                bullet.GetComponent<EnemyBullet>().Fire();
                BulletCount = 0;
            }
        }
    }

}

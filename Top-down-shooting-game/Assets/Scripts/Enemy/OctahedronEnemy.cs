using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctahedronEnemy : MonoBehaviour
{
    private Animator Animator;
    [SerializeField]
    private bool IsAttacking;
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

            RaycastHit[] Hit = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), 5f);
            foreach (var hit in Hit)
            {
                if (hit.collider.CompareTag("Player"))
                {
                    Animator.SetBool("IsAttack", true);
                }
                else
                {
                    Animator.SetBool("IsAttack", false);
                }
            }
        }
        if (IsAttacking)
        {

        }
        else
        {

        }
    }

}

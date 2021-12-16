using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class DodecahedronEnemy : MonoBehaviour
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
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, 0.7f) && hit.collider.CompareTag("Player"))
        {
            Animator.SetBool("IsAttack", true);
        }
        else
        {
            Animator.SetBool("IsAttack", false);
        }
        if (IsAttacking)
        {

        }
        else
        {

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

public class CubeEnemy : MonoBehaviour
{
    private Transform Player;
    private NavMeshAgent NavMeshAgent;
    private Animator Animator;
    [SerializeField]
    private VisualEffect AttactEffect;
    [SerializeField]
    private bool IsAttacking;
    private void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        Animator = GetComponent<Animator>();
        Player = GameObject.FindWithTag("Player").transform;
        StartCoroutine(PathRenew());
    }

    private void Update()
    {
        Player = GameObject.FindWithTag("Player").transform;
        transform.LookAt(Player);
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
            AttactEffect.SendEvent("Attacking");
        }
        else
        {
            AttactEffect.SendEvent("StopAttacking");
        }
    }
    private IEnumerator PathRenew()
    {
        while (true)
        {
            NavMeshAgent.destination = Player.position;
            yield return new WaitForSeconds(0.25f);
        }
    }
}

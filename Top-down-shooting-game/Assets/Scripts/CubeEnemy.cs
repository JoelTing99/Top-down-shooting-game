using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CubeEnemy : MonoBehaviour
{
    private Transform Player;
    private NavMeshAgent NavMeshAgent;
    private Animator Animator;
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
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, 0.7f) && hit.collider.CompareTag("Player"))
        {
            Animator.SetBool("IsAttack", true);
            Debug.Log("Attacking");
        }
        else
        {
            Animator.SetBool("IsAttack", false);
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

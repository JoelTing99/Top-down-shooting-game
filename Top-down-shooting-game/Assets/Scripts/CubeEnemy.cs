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
    }

    private void Update()
    {
        Player = GameObject.FindWithTag("Player").transform;
        NavMeshAgent.destination = Player.position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private Transform Player;
    private NavMeshAgent NavMeshAgent;

    private void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        Player = FindObjectOfType<Player>().transform;
    }
    private void Update()
    {
        if (Player != null && NavMesh.Raycast(transform.position, Vector3.down, out NavMeshHit hit, 1))
        {
            Player = FindObjectOfType<Player>().transform;
            NavMeshAgent.destination = Player.position;
            transform.LookAt(Player.position + new Vector3(0, 1.5f, 0));
        }
    }
}

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
        Player = GameObject.FindWithTag("Player").transform;
        StartCoroutine(SetDestination());
    }
    private IEnumerator SetDestination()
    {
        while (true)
        {
            if (Player != null)
            {
                Player = GameObject.FindWithTag("Player").transform;
                NavMeshAgent.destination = Player.position;
                transform.LookAt(Player.position + new Vector3(0, 1.5f, 0));
            }
            yield return null;
        }
    }
}

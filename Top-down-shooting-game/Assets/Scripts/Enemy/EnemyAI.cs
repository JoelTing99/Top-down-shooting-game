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
        StartCoroutine(PathRenew());
    }


    private void Update()
    {
        Player = GameObject.FindWithTag("Player").transform;
        transform.LookAt(Player);
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

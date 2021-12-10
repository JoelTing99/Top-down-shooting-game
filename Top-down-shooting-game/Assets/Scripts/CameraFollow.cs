using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform Player;
    private void Awake()
    {
        Player = GameObject.FindWithTag("Player").transform;
    }
    void FixedUpdate()
    {
        if(Vector3.Distance(Player.position, transform.position - new Vector3(0f, 8.5f, -2.5f)) >= 0.5)
        {
            transform.position = Vector3.Lerp(transform.position, Player.position + new Vector3(0f, 8.5f, -2.5f), 0.03f);
        }
    }
}

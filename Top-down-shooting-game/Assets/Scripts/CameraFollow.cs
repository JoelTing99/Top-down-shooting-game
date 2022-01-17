using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform Player;
    [SerializeField] Vector3 Offset;
    private void Awake()
    {
        Player = GameObject.FindWithTag("Player").transform;
    }
    void FixedUpdate()
    {
        if(Player != null)
        {
            transform.position = Vector3.Lerp(transform.position, Player.position + Offset, 0.125f);
        }
    }
}

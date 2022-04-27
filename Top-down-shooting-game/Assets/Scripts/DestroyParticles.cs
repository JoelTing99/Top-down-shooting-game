using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticles : MonoBehaviour
{
    private float count;
    void Start()
    {
         count = GetComponent<ParticleSystem>().main.duration;
    }
    private void Update()
    {
        if(count >= 0)
        {
             count -= Time.deltaTime;
        }
        else
        {
             DestroyImmediate(gameObject, true);
        }
    }
}

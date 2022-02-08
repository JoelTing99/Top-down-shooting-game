using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddList : MonoBehaviour
{
    private Templates Templates;
    private void Start()
    {
        Templates = FindObjectOfType<Templates>();
        Templates.Bridges.Add(this.gameObject);
        Templates.RoomNumbers--;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AI;

public class Templates : MonoBehaviour
{
    public GameObject[] TopRoom;
    public GameObject[] LeftRoom;
    public GameObject[] RightRoom;
    public GameObject[] BottomRoom;

    public GameObject[] TopConnect;
    public GameObject[] L_TRConnect;
    public GameObject[] L_TLConnect;
    public GameObject[] T_TRConnect;
    public GameObject[] T_TLConnect;

    public GameObject[] LeftConnect;
    public GameObject[] L_LTConnect;
    public GameObject[] L_LBConnect;
    public GameObject[] T_LTConnect;
    public GameObject[] T_LBConnect;

    public GameObject[] RightConnect;
    public GameObject[] L_RTConnect;
    public GameObject[] L_RBConnect;
    public GameObject[] T_RTConnect;
    public GameObject[] T_RBConnect;

    public GameObject[] BottomConnect;
    public GameObject[] L_BRConnect;
    public GameObject[] L_BLConnect;
    public GameObject[] T_BRConnect;
    public GameObject[] T_BLConnect;

    public GameObject[] X_TConnect;
    public GameObject[] X_RConnect;
    public GameObject[] X_BConnect;
    public GameObject[] X_LConnect; 

    public GameObject L;
    public GameObject Straight;

    public List<GameObject> Bridges;
    public int RoomNumbers;
    private void Start()
    {
        NavMeshBuilder.ClearAllNavMeshes();
        Invoke("BuildNavMesh", 10);
    }
    private void BuildNavMesh()
    {
        NavMeshBuilder.BuildNavMesh();
    }
}

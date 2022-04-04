using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuideArrow : MonoBehaviour
{
    private Transform Spawner;
    private Player Player;
    Vector2 SpawnerPos;
    Vector2 Target;
    void Start()
    {
         Player = FindObjectOfType<Player>();
    }
    void Update()
    {
        if (Spawner != null)
        {
            LookTarget();
            MoveToTarget();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void LookTarget()
    {
        SpawnerPos = new Vector2(Spawner.position.x, Spawner.position.z);
        Target = SpawnerPos - new Vector2(Player.transform.position.x, Player.transform.position.z);
        float TurnAngle = Mathf.Atan2(Target.y, Target.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, TurnAngle - 90);
    }
    private void MoveToTarget()
    {
        GetComponent<Image>().rectTransform.localPosition = transform.up * 100;
    }
    public void SetSpawner(Transform Spawner)
    {
        this.Spawner = Spawner;
    }
}

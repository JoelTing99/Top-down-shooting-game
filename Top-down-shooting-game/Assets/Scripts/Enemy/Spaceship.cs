using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    private HealthSystem healthSystem;
    private Vector3 Target;
    private EnemySpawner Spawner;
    private EnemyHealthBar HealthBar;

    public Vector3 target
    {
        get { return Target; }
        set { Target = value; }
    }

    private void Start()
    {
        Spawner = GetComponentInChildren<EnemySpawner>();
        healthSystem = new HealthSystem(300);
        HealthBar = GetComponentInChildren<EnemyHealthBar>();
        HealthBar.SetHealthSystem(healthSystem);
        HealthBar.gameObject.SetActive(false);
    }

    void Update()
    {
        Dead();
        if (transform.position == Target)
        {
            Spawner.canSpawn = true;
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, Target, Time.deltaTime);
    }
    private void Dead()
    {
        if(healthSystem.GetHealth() <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void TakeDamage(float Amount)
    {
        HealthBar.gameObject.SetActive(true);
        healthSystem.Damage(Amount);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            //Destroy(other.gameObject);
        }
    }
}

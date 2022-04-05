using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    [SerializeField] private GameObject Destroyed;
    private HealthSystem healthSystem;
    private LevelSystem levelSystem;
    private GameManager GameManager;
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
        GameManager = FindObjectOfType<GameManager>();
        levelSystem = GameManager.GetLevelSystem();
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
            GameObject destroy = Instantiate(Destroyed, transform.position, transform.rotation);
            Collider[] Collider = Physics.OverlapSphere(transform.position, 2f);
            for (int i = 0; i < Random.Range(5, 10); i++)
            {
                Instantiate(GameManager.GetCoinsGameObject(), transform.position, Quaternion.identity);
            }
            foreach (var collider in Collider)
            {
                if (collider.GetComponent<Rigidbody>() != null)
                {
                    collider.GetComponent<Rigidbody>().AddExplosionForce(400, transform.position, 2);
                }
            }
            levelSystem.ObtainExp(GameManager.GetCubeExpAmount());
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

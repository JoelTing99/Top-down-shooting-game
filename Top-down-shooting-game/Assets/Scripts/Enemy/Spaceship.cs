using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Spaceship : MonoBehaviour
{
    [SerializeField] private GameObject Destroyed;
    [SerializeField] private VisualEffect DeadEffect;
    private HealthSystem healthSystem;
    private LevelSystem levelSystem;
    private GameManager GameManager;
    private SpawnManager SpawnManager;
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
        healthSystem = new HealthSystem(GameManager.GetSpaceshipHP());
        HealthBar = GetComponentInChildren<EnemyHealthBar>();
        SpawnManager = FindObjectOfType<SpawnManager>();
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
        transform.position = Vector3.MoveTowards(transform.position, Target, 3 * Time.deltaTime);
    }
    private void Dead()
    {
        if(healthSystem.GetHealth() <= 0)
        {
            GameObject destroy = Instantiate(Destroyed, transform.position, transform.rotation);
            VisualEffect deadeffect = Instantiate(DeadEffect, transform.position, transform.rotation);
            Collider[] Collider = Physics.OverlapSphere(transform.position, 2f);
            for (int i = 0; i < Random.Range(5, 10); i++)
            {
                Vector3 RandPos = new Vector3(Random.Range(-1f, 1f), Random.Range(0f, 1f), Random.Range(-1f, 1f));
                Instantiate(GameManager.GetCoinsGameObject(), transform.position + RandPos, Quaternion.identity);
            }
            foreach (var collider in Collider)
            {
                if (collider.GetComponent<Rigidbody>() != null)
                {
                    collider.GetComponent<Rigidbody>().AddExplosionForce(400, transform.position, 2);
                }
            }
            levelSystem.ObtainExp(GameManager.GetSpaceshipExpAmount());
            Textpopup.Create(FindObjectOfType<Player>().transform.position + new Vector3(0, 2, 0), GameManager.GetSpaceshipExpAmount(), Color.green);
            Destroy(gameObject);
            Destroy(destroy, 5);
            Destroy(deadeffect, 3);
            SpawnManager.GetSpawners().Remove(transform);
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

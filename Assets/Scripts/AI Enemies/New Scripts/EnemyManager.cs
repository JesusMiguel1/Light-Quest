using UnityEngine;
using System.Collections;
using object_pool;

public class EnemyManager : MonoBehaviour
{
    public Transform player;
    public GameObject enemyPrefab;
    public GameObject enemyPrefab2;
    public GameObject[] enemyPrefabs;
    public int currentWave = 1;
    private int enemiesPerWave = 1;
    private int enemiesRemaining;

    EnemySpawner enemies;
    void Start()
    {
        StartWave();
        enemies = new EnemySpawner();
        enemyPrefabs = new GameObject[] {enemyPrefab, enemyPrefab2};
    }

    void Update()
    {
        if (enemiesRemaining == 0)
        {
            StartNextWave();
            
        }
        Debug.Log($"<b> CHECKING ENEMIES CURRENT WAVE....{enemiesRemaining}</b>");
    }

    void StartWave()
    {
        enemiesRemaining = enemiesPerWave * currentWave;

        for (int i = 0; i < enemiesRemaining; i++)
        {
            //SpawnEnemy();
           if(enemies != null)
            {
                enemies.SpawnEnemy(enemiesPerWave, 2f);
            }
        }
    }

    void StartNextWave()
    {
        currentWave++;
        StartWave();
    }

    void SpawnEnemy()
    {
        int index = Random.Range(0, enemyPrefabs.Length);
        //GameObject enemy = Instantiate(enemyPrefabs[index], GetRandomSpawnPosition(), Quaternion.identity);
        //enemy.GetComponent<AI_Drones>().player = player;
        //enemy.GetComponent<AI_Drones>().enemyManager = this;
    }

    Vector3 GetRandomSpawnPosition()
    {
        // Implement your logic to get random spawn positions here
        float x = Random.Range(30f, 60f);
        float z = Random.Range(30f, 60f);
        return new Vector3(x, 0.5f, z);
    }
    public void EnemyDestroyed()
    {
        enemiesRemaining--;

        if (enemiesRemaining == 0)
        {
            StartNextWave();
        }
    }
}


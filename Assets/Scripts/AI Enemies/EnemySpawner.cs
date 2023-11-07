using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //Trying to spawn enemies on random amount
    private float m_interval = 5f;
    private float m_time = 0f;
    private int minAmount = 1;
    private int maxAmount = 3;


    private GameObject dronePrefab;
    private GameObject robotPrefab;
    public GameObject drone;


    void Start()
    {
        dronePrefab = Resources.Load("D1", typeof(GameObject)) as GameObject;
        robotPrefab = Resources.Load("Robot", typeof(GameObject)) as GameObject;

        //StartCoroutine(SpawnDrone(timeInSeconds));
    }

    //private IEnumerator SpawnDrone(float delayInSeconds)
    //{
    //    yield return new WaitForSeconds(delayInSeconds);
    //    if (!isSpawned)
    //    {
    //        Vector3 newPosition = Random.onUnitSphere * spawnRadius;
    //        newPosition.y = Random.Range(9.5f, 10f);
    //        Instantiate(dronePrefab, newPosition, Quaternion.identity);
        
    //        //isSpawned = true;
    //    }
  
    //}

    void Update()
    {

        if (!drone.activeInHierarchy)
        {
            //StartCoroutine(SpawnEnemies());

            //Increasing timer 
            m_time += Time.deltaTime;

            if (m_time >= m_interval)
            {

                //reset timer
                m_time = 0f;

                int amountOfEnemies = Random.Range(minAmount, maxAmount + 1);

                for (int i = 0; i < amountOfEnemies; i++)
                {
                    // Instantiate the enemy prefab at a random position
                    Vector3 spawnPosition = new Vector3(Random.Range(-40f, 50f), 5f, Random.Range(-20f, 40f));
                    Instantiate(dronePrefab, spawnPosition, Quaternion.identity);
                }
            }
        }

    }

    //public IEnumerator SpawnEnemies()
    //{
    //    while ( enemyCount < amountEnemies )
    //    {
           
    //            Vector3 newPosition = Random.onUnitSphere * spawnRadius;
    //            newPosition.y = Random.Range(5f, 10f);
    //            Instantiate(dronePrefab, newPosition, Quaternion.identity);
    //            yield return new WaitForSeconds(1);
    //            enemyCount += 1;
    //        if (enemyCount == amountEnemies)
    //        {
    //            yield break;
    //        }

    //    }
    //}
    //Instantiate the prefab
   
    //void SpawnMoreEnemies()
    //{
    //    for (int i = 0; i < amountOfDrones; i++)
    //    {
    //        Vector3 newPosition = Random.onUnitSphere * spawnRadius;
    //        Instantiate(dronePrefab, newPosition, Quaternion.identity);
    //    }
    //}
    //void SpawnRobot()
    //{
    //    if(!isSpawned && Time.time > 30f && !dronePrefab.activeSelf)
    //    {
    //        Vector3 newPosition = Random.onUnitSphere * spawnRadius;
    //        //newPosition.y = 15f;
    //        Instantiate(robotPrefab, newPosition, Quaternion.identity);
    //    }
    //}

}

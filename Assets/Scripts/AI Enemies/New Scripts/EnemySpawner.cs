using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace object_pool
{
    public class EnemySpawner : MonoBehaviour
    {
        private int enemiesAmount = 3;
        private int numberOfWaves = 5;
        private float delayWave = 2f;
        private GameObject enemy;

        void Start()
        {
            StartCoroutine(SpawnEnemiesTrigger());
        }

        IEnumerator SpawnEnemiesTrigger()
        {
            float timeToSpawn = 1f;
            float waveTimeToSpan = 3f;

            for (int wave = 0; wave < numberOfWaves; wave++)
            {
                yield return StartCoroutine(SpawnWave(enemiesAmount, timeToSpawn));
                enemiesAmount += 3;
                yield return new WaitForSeconds(waveTimeToSpan * delayWave);
            }
        }

        IEnumerator SpawnWave(int numEnemies, float spawnDelay)
        {
            for (int i = 0; i < numEnemies; i++)
            {
                enemy = DronesPoolManager.Instance.GetDrones();
                enemy.transform.position = new Vector3(Random.Range(-50, 50), 1f, Random.Range(-50, 50));
                enemy.transform.rotation = transform.rotation;
                yield return new WaitForSeconds(spawnDelay);
            }
        }







        ////Trying to spawn enemies on random amount
        //private float m_interval = 3f;
        //private float m_time = 0f;
        //private int minAmount = 1;
        //private int maxAmount = 5;
        //private int maxAmountOfEnemies = 10;
        //public GameObject drone;




        //void Update()
        //{
        //    if (!drone.activeInHierarchy)
        //    {
        //        //Increasing timer 
        //        m_time += Time.deltaTime;

        //        if (m_time >= m_interval)
        //        {
        //            //reset timer
        //            m_time = 0f;

        //            int amountOfEnemies = Random.Range(minAmount, maxAmount + 1);

        //            for (int i = 0; i < amountOfEnemies; i++)
        //            {
        //                // Instantiate the enemy prefab at a random position
        //                Vector3 spawnPosition = new Vector3(Random.Range(-50f, 50f), 5f, Random.Range(-60f, 60f));
        //                //Instantiate(dronePrefab, spawnPosition, Quaternion.identity);
        //                GameObject drone = DronesPoolManager.Instance.GetDrones();
        //                drone.transform.position = spawnPosition;
        //                //drone.transform.rotation = transform.rotation;
        //            }
        //        }
        //    }
        //}
    }
}

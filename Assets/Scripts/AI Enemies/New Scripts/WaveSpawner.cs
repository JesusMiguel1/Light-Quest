using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace object_pool
{
    public class WaveSpawner : MonoBehaviour
    {
        private int enemiesAmount = 1;
        private int numberOfWaves = 25;
        private float delayWave = 2f;
        private GameObject enemy;
        private float speed = 10f;
        private EnemySpawner spawner;
        public GameObject enemyTrigger;
        private GlobalSpeedManager speedManager;

        void OnEnable()
        {
            StartCoroutine(SpawnEnemiesTrigger());
            speedManager = new GlobalSpeedManager();
            spawner = new EnemySpawner();
        }

        IEnumerator SpawnEnemiesTrigger()
        {
            float timeToSpawn =2f;
            float waveTimeToSpan = 1f;
           
            for (int wave = 0; wave < numberOfWaves; wave++)
            {
                if(spawner != null)
                {
                    yield return StartCoroutine(spawner.SpawnEnemy(enemiesAmount, timeToSpawn));
                    speedManager.CurrentSpeed = speed;
                    if (wave > 1)
                    {
                        enemiesAmount += 1;
                        timeToSpawn -= 0.08F;
                        //if (wave > 15) {
                        //    timeToSpawn -= 0.1f;
                        //}
                        speed += 2f;
                        Debug.Log($"<b> CHECKING WAVE TIME....{timeToSpawn}</b> ");
                    }
                }
                yield return new WaitForSeconds(waveTimeToSpan * delayWave);

                Debug.Log($"<b> CHECKING ENEMIES WAVE....{wave}</b> <b> and ENEMIES AMOUNT...{enemiesAmount} </b>");
            }
        }

        //IEnumerator SpawnEnemy(int numEnemies, float spawnDelay)
        //{
        //    for (int i = 0; i < numEnemies; i++)
        //    {
        //        enemy = DronesPoolManager.Instance.GetDrones();
        //        enemy.transform.position = new Vector3(UnityEngine.Random.Range(-50, 50), 1f, UnityEngine.Random.Range(-50, 50));
        //        enemy.transform.rotation = transform.rotation;

        //        // speedManager.OnSpeedChanged(speed+=5);
        //        yield return new WaitForSeconds(spawnDelay);
        //    }
        //}








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

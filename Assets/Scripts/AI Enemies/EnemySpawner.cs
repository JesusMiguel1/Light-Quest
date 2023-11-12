using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace punk_vs_robots
{
    public class EnemySpawner : MonoBehaviour
    {
        //Trying to spawn enemies on random amount
        private float m_interval = 3f;
        private float m_time = 0f;
        private int minAmount = 1;
        private int maxAmount = 5;
        public GameObject drone;




        void Update()
        {
            if (!drone.activeInHierarchy)
            {
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
                        Vector3 spawnPosition = new Vector3(Random.Range(-50f, 50f), 5f, Random.Range(-60f, 60f));
                        //Instantiate(dronePrefab, spawnPosition, Quaternion.identity);
                        GameObject drone = DronesPoolManager.Instance.GetDrones();
                        drone.transform.position = spawnPosition;
                        //drone.transform.rotation = transform.rotation;
                    }
                }
            }
        }
    }
}

using System;
using System.Collections;
using UnityEngine;

namespace object_pool
{
    public class EnemyTriggerManager : MonoBehaviour
    {
        [Header("Enemy trigger")]
        [Tooltip("Enemy trigger must go here. This object will trigger the enemies when deactivated")]
        public GameObject enemyTrigger;

        [Header("Enemy spawner")]
        [Tooltip("Enemy spawner must go here. This object will spawn the enemies when the enemy trigger deactivated")]
        public GameObject enemySpawner;

        void OnEnable()
        {
            enemySpawner.SetActive(false);
        }
        void Update()
        {
            if (!enemyTrigger.activeInHierarchy) 
            {
                StartCoroutine(StartEnemyWave());
            }
        }
        void OnDisable()
        {
            enemySpawner.SetActive(false);
        }
        IEnumerator StartEnemyWave()
        {
            yield return new WaitForSeconds(5);
            enemySpawner.SetActive(true);
        }
    }
}


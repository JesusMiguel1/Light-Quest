using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace object_pool
{
    public class WaveSpawner : MonoBehaviour
    {
        //private int enemiesAmount = 1;
        //private int numberOfWaves = 25;
        //private float delayWave = 2f;
        //private GameObject enemy;
        //private float speed = 10f;
        //private EnemySpawner spawner;
        //public GameObject enemyTrigger;
        //private GlobalSpeedManager speedManager;
        //public GameObject pipe; 
        ////public GameObject pipe1; 
        ////public GameObject pipe2;
        


        //void OnEnable()
        //{
        //    StartCoroutine(SpawnEnemiesTrigger());
        //    speedManager = new GlobalSpeedManager();
        //    spawner = new EnemySpawner();
        //}

        //IEnumerator SpawnEnemiesTrigger()
        //{
        //    float timeToSpawn =2f;
        //    float waveTimeToSpan = 1f;
        //   // GameObject[] pipes = new GameObject[] { pipe, pipe1, pipe2 };
        //    for (int wave = 0; wave < numberOfWaves; wave++)
        //    {
        //       // GameObject randomPipe = pipes[Random.Range(0, pipes.Length)];
        //        if (spawner != null)
        //        {
        //            yield return StartCoroutine(spawner.SpawnEnemy(enemiesAmount, timeToSpawn, pipe));
        //            speedManager.CurrentSpeed = speed;
        //            if (wave > 1)
        //            {
        //                enemiesAmount += 1;
        //                timeToSpawn -= 0.08F;
        //                //if (wave > 15) {
        //                //    timeToSpawn -= 0.1f;
        //                //}
        //                speed += 2f;
        //                Debug.Log($"<b> CHECKING WAVE TIME....{timeToSpawn}</b> ");
        //            }
        //        }
        //        yield return new WaitForSeconds(waveTimeToSpan * delayWave);

        //        //Debug.Log($"<b> CHECKING ENEMIES WAVE....{wave}</b> <b> and ENEMIES AMOUNT...{enemiesAmount} </b>");
        //    }
        //}
    }
}

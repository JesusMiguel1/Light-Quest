using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace object_pool
{
    public class EnemySpawner
    {
       // private Transform transform;
        

        public IEnumerator SpawnEnemy(int numEnemies, float spawnDelay)
        {
            for (int i = 0; i < numEnemies; i++)
            {
                GameObject enemy = DronesPoolManager.Instance.GetDrones();
                enemy.transform.position = new Vector3(UnityEngine.Random.Range(-90, 90), 1f, UnityEngine.Random.Range(-90, 90));
               // enemy.transform.rotation = transform.rotation;
                
                // speedManager.OnSpeedChanged(speed+=5);
                yield return new WaitForSeconds(spawnDelay);
            }
        }
    }
}
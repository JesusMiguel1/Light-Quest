using object_pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveOfEnemiesSpawner : MonoBehaviour
{
    private GlobalStrings _strings;
    private GameObject slapper;
    private GameObject secondDrone;
    private GameObject[] dronesPrefabs;

    private int maxAmountOfEnemies = 5;
    private int amoutnOfEnemiesToSpawn = 1;
    private int numOfWaves = 10;
    private int currentWave = 1;

    private float spawnInterval = 2;
    // Start is called before the first frame update
    void Start()
    {
        

        for (int i = 0; i < currentWave; i++)
        {
            for(int j = 0; j < maxAmountOfEnemies; j++) 
            {
                Vector3 position = new Vector3(Random.Range(-50, 50), 5f, Random.Range(-30, 30));
                GameObject drone = DronesPoolManager.Instance.GetDrones();
                drone.transform.position = position;

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

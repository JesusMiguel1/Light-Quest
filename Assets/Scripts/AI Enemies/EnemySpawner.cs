using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private GameObject dronePrefab;
    private GameObject robotPrefab;
    private bool isSpawned = false;
    private float spawnRadius = 10f;

    // Start is called before the first frame update
    void Start()
    {
        //Getting the drone prefab
       
    }

    // Update is called once per frame
    void Update()
    {
        //Instantiate the prefab after one minutes of the game started
        if(!isSpawned && Time.time >= 30f)
        {
            dronePrefab = Resources.Load("Drone", typeof(GameObject)) as GameObject;
            robotPrefab = Resources.Load("Robot", typeof(GameObject)) as GameObject;
            SpawnDrone();
            isSpawned = true;
        }
    }

    //Instantiate the prefab
    void SpawnDrone()
    {
        Vector3 newPosition = Random.onUnitSphere * spawnRadius;
        newPosition.y = 0f;
        Instantiate(dronePrefab, newPosition, Quaternion.identity);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private GameObject dronePrefab;
    private GameObject robotPrefab;
    private bool isSpawned = false;
    private float spawnRadius = 10f;
    private Transform _player;
    // Start is called before the first frame update
    void Start()
    {
        //Getting the drone prefab
        //_player = GameObject.FindWithTag("Player").transform;
       
    }

    // Update is called once per frame
    void Update()
    {
        //Instantiate the prefab after one minutes of the game started
        if(!isSpawned && Time.time >= 2f)
        {
            dronePrefab = Resources.Load("Drone", typeof(GameObject)) as GameObject;
            robotPrefab = Resources.Load("Robot", typeof(GameObject)) as GameObject;
            SpawnDrone();
            isSpawned = true;

            if(dronePrefab == null) {
                Instantiate(robotPrefab, transform.position, Quaternion.identity);
            }
        }
    }

    //Instantiate the prefab
    void SpawnDrone()
    {

        Vector3 newPosition = Random.onUnitSphere * spawnRadius;
        //newPosition.y = 15f;
        Instantiate(dronePrefab, newPosition, Quaternion.identity);

        //Rigidbody addingRigidBody = dronePrefab.AddComponent<Rigidbody>();

       //adding gravity to it 
        //addingRigidBody.useGravity = true;  
    }

    void SpawnRobot()
    {
        if(!isSpawned && Time.time > 30f && !dronePrefab.activeSelf)
        {
            Vector3 newPosition = Random.onUnitSphere * spawnRadius;
            //newPosition.y = 15f;
            Instantiate(robotPrefab, newPosition, Quaternion.identity);
        }
    }
}

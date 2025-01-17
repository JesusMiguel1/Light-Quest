﻿using object_pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    [HideInInspector] public GameObject enemies;
    public string waveName;
    public int enemyCount;
    public float waveRate;
}
public class EnemyWaveSpawner : MonoBehaviour
{
    #region VARIABLES
    public enum SpawnWaveState { SPAWNING, WAIT, COUNTER}
    [Header("WAVE OF ENEMIES")]
    [Tooltip("Here we can set up the number of waves we wants to spawn and the amount " +
        "of enemies on each wave and also a delay to start the next wave ")]
    public Wave[] enemyWaves;
    public float waveCountDown;
    [Header("WAY POINTS SETTINGS")]
    [Tooltip("Setup the waypoints amount abd add the way point objects the the correponding fields.")]
    public Transform[] spawnPoints;
    private int nextWave = 0;
    private float timeToNextWave = 1f;
    private float checkForEnemiesTimer = 1f;
    private bool stopSpawning = false;
    private bool hasSpawned;

    [HideInInspector]public GameObject pipe;

    [Header("ENEMIES TRIGGER")]
    [Tooltip("A game object that will trigger the enemies if destroyed. In this case we are using the enemy trigger gameobjec.")]
    public GameObject trigger;
    public GameObject backMusic;
    private GameObject policeInspector;
    GameObject polInspector;
    GameObject wanderRobots;
    
    GameObject bossPolice;
    [SerializeField] GameObject bossSpawner; 

    private int wanderAmount;

    private GlobalStrings strings;


    private SpawnWaveState waveState = SpawnWaveState.COUNTER;
    #endregion

    void Start()
    {
        wanderAmount = 10;
        strings = new GlobalStrings();
        //trigger = Instantiate(Resources.Load(strings.Trigger, typeof(GameObject))) as GameObject;
        policeInspector = Resources.Load(strings.policeInspector, typeof(GameObject)) as GameObject;
        wanderRobots = Resources.Load(strings.wanderDrone, typeof(GameObject)) as GameObject;
        waveCountDown = timeToNextWave;
        backMusic.SetActive(false);

        for (int i = 0; i < wanderAmount;  i++)
        {
            SpawnWanderDrones();

        }

        bossPolice = Resources.Load("BOSS", typeof(GameObject)) as GameObject; 
        
    }
    void Update()
    {
        
        if (!trigger.activeInHierarchy && !hasSpawned)
        {
            SpawnInspector();
            hasSpawned = true;
            polInspector = GameObject.Find(strings.policeInspectorClone);

        }
        SpawningPolice();

        //I need to self destroy enemies if they stay alive for to long when to far from player to keep the waves going
    }


    #region SPAWN POLICE
    void SpawningPolice()
    {
        
        if (!trigger.activeInHierarchy && !stopSpawning && !polInspector.activeInHierarchy )
        {
            backMusic.SetActive(true);

            FindObjectOfType<MainAudioManager>().PlaySound("Theme");

            CheckForEnemies();
        }
    }
    #endregion



    void SpawnWanderDrones()
    {
        Vector3 positions = new Vector3(Random.Range(-10, 20), Random.Range(6.8F, 10f), Random.Range(-10, 30));
        GameObject civilians = Instantiate(wanderRobots);
        civilians.transform.position = positions;
        civilians.transform.rotation = Quaternion.identity;
    }

    void SpawnBoss()
    {
        GameObject boss = Instantiate(bossPolice);
        boss.transform.position = bossSpawner.transform.position;
        
    }
    void CheckForEnemies()
    {
        if (waveState == SpawnWaveState.WAIT)
        {
            //CHECK IF THERE STILL ENEMIES IN THE LEVEL
            if (!IfEnemyAlive())
            {
               NextWaveIfCompleted();
            }
            else
            {
                return;
            }
        }
        if (waveCountDown <= 0)
        {
            if (waveState != SpawnWaveState.SPAWNING)
            {
                //SPAWN WAVE
                StartCoroutine(SpawnWaves(enemyWaves[nextWave]));
            }
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }
    }

    void NextWaveIfCompleted()
    {
        Debug.Log($"<b> WAVE COMPLETED! ... </b>");
        waveState = SpawnWaveState.COUNTER;
        waveCountDown = timeToNextWave;
        if(nextWave + 1 > enemyWaves.Length - 1)
        {
            stopSpawning = true;
            nextWave = 0;

            Debug.Log($"<b> ALL WAVE COMPLETED! ... </b>");

            SpawnBoss(); 
            //HERE WE CAN ADD THE BOSS TO BE SPAWNED AFTER THE LOOP IS COMPLETED.
            //AND ALL A WINNING CHEERS IF THE PLAYR WIN THE GAME 
        }
        else
        {
            nextWave++;
        }
    }

    bool IfEnemyAlive()
    {
        checkForEnemiesTimer -= Time.deltaTime;
        if(checkForEnemiesTimer <= 0) 
        {
            checkForEnemiesTimer = 0.2f;
            //if (GameObject.Find(strings.slapper) == null)
            //{
            //    return false;
            //}

            //THIS IS THE RIGHT CODE
            if (GameObject.Find(strings.slapperClone) == null)
            {
                return false;
            }
        }
        
        return true;    
    }

    IEnumerator SpawnWaves(Wave _wave)
    {
        waveState = SpawnWaveState.SPAWNING;
        for(int i=0; i < _wave.enemyCount; i++)
        {
            SpawnEnemies(_wave.enemies);
            yield return new WaitForSeconds(0.5f / _wave.waveRate);
        }
        waveState = SpawnWaveState.WAIT;
        yield break;
    }
    void SpawnInspector()
    {
        Vector3 position = new Vector3(15f, 15, 90f);
        GameObject police = Instantiate(policeInspector);
        police.transform.position = position;
        police.transform.rotation = Quaternion.identity;
    }
    void SpawnEnemies(GameObject _enemies)
    {
        Transform enemiesSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)]; 
        _enemies = DronesPoolManager.Instance.GetDrones();
        _enemies.transform.position = enemiesSpawnPoint.position;//new Vector3(UnityEngine.Random.Range(-90, 90), 1f, UnityEngine.Random.Range(-90, 90));
        _enemies.transform.rotation =   Quaternion.identity;
    }
}

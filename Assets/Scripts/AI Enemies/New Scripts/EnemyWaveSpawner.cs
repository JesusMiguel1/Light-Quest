﻿using object_pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyWaveSpawner;

public class EnemyWaveSpawner : MonoBehaviour
{
    public enum SpawnWaveState { SPAWNING, WAIT, COUNTER}
    [System.Serializable]
    public class Wave
    {
        [HideInInspector]public GameObject enemies;
        public string waveName;
        public int enemyCount;
        public float waveRate;
    }
    public Wave[] enemyWaves;
    public Transform[] spawnPoints;
    private int nextWave = 0;
    private float timeToNextWave = 2f;
    public float waveCountDown;
    private float checkForEnemiesTimer = 1f;
    private bool stopSpawning = false;

    [HideInInspector]public GameObject pipe;
    public GameObject trigger;
    public GameObject backMusic;


    private SpawnWaveState waveState = SpawnWaveState.COUNTER;

    void Start()
    {
        waveCountDown = timeToNextWave;
        backMusic.SetActive(false);
    }
    void Update()
    {
        if(!trigger.activeInHierarchy && !stopSpawning)
        {
            backMusic.SetActive(true);
            CheckForEnemies();
        }
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
            checkForEnemiesTimer = 1f;
            if (GameObject.Find("slapper") == null)
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
            yield return new WaitForSeconds(1f / _wave.waveRate);
        }
        waveState = SpawnWaveState.WAIT;
        yield break;
    }

    void SpawnEnemies(GameObject _enemies)
    {
        Transform enemiesSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)]; 
        _enemies = DronesPoolManager.Instance.GetDrones();
        _enemies.transform.position = enemiesSpawnPoint.position;//new Vector3(UnityEngine.Random.Range(-90, 90), 1f, UnityEngine.Random.Range(-90, 90));
        _enemies.transform.rotation = enemiesSpawnPoint.rotation;
    }
}
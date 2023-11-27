using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour
{
    public enum SpawnWaveState { SPAWNING, WAIT, COUNTER}
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public Transform enemies;
        public int enemyCount;
        public float waveRate;
    }
    public Wave[] enemyWaves;
    private int nextWave = 0;
    private float timeToNextWave = 3f;
    public float waveCountDown;

    private SpawnWaveState waveState = SpawnWaveState.COUNTER;

    void Start()
    {
        waveCountDown = timeToNextWave;
    }
    void Update()
    {
        if(waveState == SpawnWaveState.WAIT)
        {
            //CHECK IF THERE STILL ENEMIES IN THE LEVEL
        }
        if(waveCountDown <= 0)
        {
            if(waveState != SpawnWaveState.SPAWNING)
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
    bool IfEnemyAlive()
    {
        if (GameObject.Find("slapper") == null){
            return false;
        }
        return true;    
    }
    IEnumerator SpawnWaves(Wave _wave)
    {
        waveState = SpawnWaveState.SPAWNING;
        for(int i=0; i< _wave.enemyCount; i++)
        {
            SpawnEnemies(_wave.enemies);
            yield return new WaitForSeconds(1f / _wave.waveRate);
        }
        waveState = SpawnWaveState.WAIT;
        yield break;
    }

    void SpawnEnemies(Transform _enemies)
    {

    }
}

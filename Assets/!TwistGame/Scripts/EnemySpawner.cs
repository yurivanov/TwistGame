using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
     public enum SpawnState { SPAWNING, WAITING, COUNTING};

    [SerializeField] private Wave[] waves;
    
    [SerializeField] private float timeBetweenWaves = 3f;

    [SerializeField] private float waveCountdown = 0;

    private SpawnState state = SpawnState.COUNTING;

    [SerializeField] private Transform[] spawners;

   
   private void Start()
    {
        waveCountdown = timeBetweenWaves;
    }

    private void Update() 
    {

        if (waveCountdown <= 0 )
                {
                    if(state != SpawnState.SPAWNING);
                    {
                        
                    }
                }

        else 
             waveCountdown -= Time.deltaTime;
    }       


    private IEnumerator SpawnWave(Wave wave)
    {

        state = SpawnState.SPAWNING;

        for (int i = 0; i < wave.enemiesAmount; i++)
                  {
                        SpawnZombie(wave.enemy);
                        yield return new WaitForSeconds(wave.delay);

                  }

        state = SpawnState.WAITING;

        yield break;

    }

    private void SpawnZombie(GameObject enemy)
    {
        int randomInt = Random.RandomRange(1, spawners.Length);
        Transform randomSpawner = spawners[randomInt];

        Instantiate(enemy, randomSpawner.position, randomSpawner.rotation);

    }

}

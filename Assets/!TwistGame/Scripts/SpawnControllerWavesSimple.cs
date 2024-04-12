using System.Collections;
using UnityEngine;

public class SpawnControllerWavesSimple : MonoBehaviour
 {
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs;  // A prefab for each wave
    public int[] waveCounts;  // Number of enemies in each wave
    public float[] waveDelays;  // Delay time before each wave
    public float[] spawnDelays;  // Delay before spawning each enemy in the wave

    private void Start() {
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves() {
        for (int wave = 0; wave < waveCounts.Length; wave++) {
            if(waveCounts[wave] == 0) {
                continue;
            }
            for (int count = 0; count < waveCounts[wave]; count++) {
                SpawnEnemy(enemyPrefabs[wave]);  // Spawn the enemy specific to the current wave
                yield return new WaitForSeconds(spawnDelays[wave]);
            }
            if(wave < waveDelays.Length) {
                yield return new WaitForSeconds(waveDelays[wave]);
            }
        }
    }

    private void SpawnEnemy(GameObject enemyPrefab) {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}

using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveController : MonoBehaviour
{
    [SerializeField] Vector4[] waves;
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] private float spawnRange;


    [SerializeField]
    Vector3 SpawnPoint => new(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0);

    int _waveIndex;


    private void Start()
    {
        SpawnWave();
    }


    void SpawnWave()
    {
        for (var i = 0; i < 4; i++)
        for (var j = 0; j < waves[_waveIndex][i]; j++)
        {
            var enemy = Instantiate(enemyPrefabs[i], SpawnPoint, Quaternion.identity);
            enemy.name = enemyPrefabs[i].name + " " + j;
        }
        
        _waveIndex++;
    }
    
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveController : MonoBehaviour
{
    public static WaveController Instance;
    
    [SerializeField] Vector4[] waves;
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] private float spawnRange;
    public List<GameObject> aliveEnemies = new List<GameObject>();


    [SerializeField]
    Vector3 SpawnPoint => new(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0);

    int _waveIndex;

    private void Awake()
    {
        Instance ??= this;
    }

    private void Start()
    {
        StartCoroutine(SpawnWave());
    }
    
    

    IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(1f);
        for (var i = 0; i < 4; i++)
        for (var j = 0; j < waves[_waveIndex][i]; j++)
        {
            var enemy = Instantiate(enemyPrefabs[i], SpawnPoint, Quaternion.identity);
            enemy.name = enemyPrefabs[i].name + " " + j;
            aliveEnemies.Add(enemy);
        }

        if(_waveIndex < waves.Length) _waveIndex++;
    }

    public void Control()
    {
        if (aliveEnemies.Count == 0) StartCoroutine(SpawnWave());
    }
    
    
}

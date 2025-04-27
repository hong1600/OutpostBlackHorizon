using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBossSpawner : MonoBehaviour
{
    Terrain terrain;

    EnemyManager enemyManager;
    EnemySpawner enemySpawner;
    ObjectPoolManager poolManager;

    private void Awake()
    {
        terrain = Terrain.activeTerrain;
    }

    private void Start()
    {
        enemyManager = EnemyManager.instance;
        enemySpawner = enemyManager.EnemySpawner;
        poolManager = ObjectPoolManager.instance;

        GameManager.instance.Round.onRoundEvent += SpawnDelay;
    }

    private void SpawnDelay()
    {
        Invoke(nameof(SpawnWaveBoss), 30);
    }

    //상속구조 팩토리패턴
    public void SpawnWaveBoss()
    {
        int spawnPoint = Random.Range(0, enemySpawner.EnemySpawnPointList().Count);

        Vector3 spawnPos =
            enemySpawner.EnemySpawnPointList()[spawnPoint].transform.position;

        spawnPos.y = terrain.SampleHeight(spawnPos);

        GameObject waveBoss =
            poolManager.EnemyPool.FindEnemy(EEnemy.ROBOT5, spawnPos, Quaternion.identity);
    }
}

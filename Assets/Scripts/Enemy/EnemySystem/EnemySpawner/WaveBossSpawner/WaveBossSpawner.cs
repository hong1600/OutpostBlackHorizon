using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBossSpawner : MonoBehaviour
{
    Terrain terrain;

    EnemyManager enemyManager;
    EnemySpawner enemySpawner;
    EnemyFactory enemyFactory;

    private void Awake()
    {
        terrain = Terrain.activeTerrain;
    }

    private void Start()
    {
        enemyManager = EnemyManager.instance;
        enemySpawner = enemyManager.EnemySpawner;
        enemyFactory = FactoryManager.instance.EnemyFactory;

        GameManager.instance.Round.onRoundEvent += SpawnDelay;
    }

    private void SpawnDelay()
    {
        Invoke(nameof(SpawnWaveBoss), 10);
    }

    public void SpawnWaveBoss()
    {
        if (GameManager.instance.Timer.isSpawnTime)
        {
            int spawnPoint = Random.Range(0, enemySpawner.EnemySpawnPointList().Count);

            Vector3 spawnPos =
                enemySpawner.EnemySpawnPointList()[spawnPoint].transform.position;

            spawnPos.y = terrain.SampleHeight(spawnPos);

            GameObject waveBoss = enemyFactory.Create(EEnemy.ROBOT5, spawnPos, Quaternion.identity, null, null);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Mathematics;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    Terrain terrain;
    EnemyPool enemyPool;

    event Action onEnemySpawn;

    [SerializeField] List<Transform> enemySpawnPointList;
    [SerializeField] Vector3[] enemySpawnPos = new Vector3[4];
    [SerializeField] Transform[] targetPoints;
    [SerializeField] float enemySpawnDelay;

    private void Awake()
    {
        terrain = Terrain.activeTerrain;
    }

    private void Start()
    {
        enemyPool = Shared.objectPoolManager.EnemyPool;
        enemySpawnDelay = 0;

        enemySpawnPos[0] = new Vector3(-3f, 0, 0);
        enemySpawnPos[1] = new Vector3(-1f, 0, 0);
        enemySpawnPos[2] = new Vector3(1f, 0, 0);
        enemySpawnPos[3] = new Vector3(3f, 0, 0);
    }

    public void SpawnEnemy()
    {
        if (Shared.gameManager.Round.GetCurRound() == 0 || Shared.gameManager.Round.GetIsBossRound()) { return; }

        if (enemySpawnDelay <= 0)
        {
            int firstSpawnPoint = Random.Range(0, enemySpawnPointList.Count);
            int secondSpawnPoint;
            do
            {
                secondSpawnPoint = Random.Range(0, enemySpawnPointList.Count);
            } while (firstSpawnPoint == secondSpawnPoint);

            EEnemy eEnemy1 = (EEnemy)Random.Range(0, 4);
            EEnemy eEnemy2 = (EEnemy)Random.Range(0, 4);

            for (int i = 0; i < 1; i++)
            {
                //적 초기위치
                Vector3 spawnPos1 = enemySpawnPointList[firstSpawnPoint].transform.position + (enemySpawnPos[i]);
                Vector3 spawnPos2 = enemySpawnPointList[secondSpawnPoint].transform.position + (enemySpawnPos[i]);

                //터레인 지면 높이
                spawnPos1.y = terrain.SampleHeight(spawnPos1);
                spawnPos2.y = terrain.SampleHeight(spawnPos2);

                //적 가져오기
                GameObject obj1 = enemyPool.FindEnemy(eEnemy1, spawnPos1, Quaternion.identity);
                GameObject obj2 = enemyPool.FindEnemy(eEnemy2, spawnPos2, Quaternion.identity);

                onEnemySpawn?.Invoke();
            }

            enemySpawnDelay = 2f;
        }

        enemySpawnDelay -= Time.deltaTime;
    }

    public void SubEnemySpawn(Action _listener) { onEnemySpawn += _listener; }
    public void UnEnemySpawn(Action _listener) { onEnemySpawn -= _listener; }
    public float GetEnemySpawnDelay() { return enemySpawnDelay; }
    public void SetEnemySpawnDelay(float _value) { enemySpawnDelay = _value; }
    public Transform[] GetTargetPoint() { return targetPoints; }
    public List<Transform> GetEnemySpawnPointList() { return enemySpawnPointList; }
}

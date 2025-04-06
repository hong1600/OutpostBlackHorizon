using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Mathematics;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public event Action onEnemySpawn;

    Terrain terrain;
    EnemyPool enemyPool;
    Round round;

    public Vector3[] enemySpawnPos { get; private set; } = new Vector3[4];
    [SerializeField] List<Transform> enemySpawnPointList;
    [SerializeField] Transform[] targetPoints;
    [SerializeField] float enemySpawnDelay;

    bool isSpawn = false;
    bool firstSpawn = true;

    private void Awake()
    {
        terrain = Terrain.activeTerrain;
    }

    private void Start()
    {
        enemyPool = ObjectPoolManager.instance.EnemyPool;
        round = GameManager.instance.Round;
        enemySpawnDelay = 0;

        enemySpawnPos[0] = new Vector3(-6f, 0, 0);
        enemySpawnPos[1] = new Vector3(-2f, 0, 0);
        enemySpawnPos[2] = new Vector3(2f, 0, 0);
        enemySpawnPos[3] = new Vector3(6f, 0, 0);
    }

    public void SpawnEnemy()
    {
        if (!isSpawn || round.curRound == 0 || round.isBossRound) { return; }
        
        if (enemySpawnDelay <= 0)
        {
            int spawnPoint;

            if (firstSpawn)
            {
                spawnPoint = 1;
                firstSpawn = false;
            }
            else
            {
                spawnPoint = Random.Range(0, enemySpawnPointList.Count);
            }

            StartCoroutine(StartSpawn(spawnPoint));

            enemySpawnDelay = 8f;
        }

        enemySpawnDelay -= Time.deltaTime;
    }

    IEnumerator StartSpawn(int _spawnPoint)
    {
        for (int i = 0; i < 4; i++)
        {
            EEnemy eEnemy = (EEnemy)Random.Range(0, 4);

            Vector3 spawnPos1 = enemySpawnPointList[_spawnPoint].transform.position + (enemySpawnPos[0]);
            Vector3 spawnPos2 = enemySpawnPointList[_spawnPoint].transform.position + (enemySpawnPos[1]);
            Vector3 spawnPos3 = enemySpawnPointList[_spawnPoint].transform.position + (enemySpawnPos[2]);
            Vector3 spawnPos4 = enemySpawnPointList[_spawnPoint].transform.position + (enemySpawnPos[3]);

            spawnPos1.y = terrain.SampleHeight(spawnPos1);
            spawnPos2.y = terrain.SampleHeight(spawnPos2);
            spawnPos3.y = terrain.SampleHeight(spawnPos3);
            spawnPos4.y = terrain.SampleHeight(spawnPos4);

            GameObject obj1 = enemyPool.FindEnemy(eEnemy, spawnPos1, Quaternion.identity);
            GameObject obj2 = enemyPool.FindEnemy(eEnemy, spawnPos2, Quaternion.identity);
            GameObject obj3 = enemyPool.FindEnemy(eEnemy, spawnPos3, Quaternion.identity);
            GameObject obj4 = enemyPool.FindEnemy(eEnemy, spawnPos4, Quaternion.identity);

            onEnemySpawn?.Invoke();

            yield return new WaitForSeconds(2f);
        }
    }

    public List<Transform> EnemySpawnPointList() { return enemySpawnPointList; }
    public Transform[] TargetPoint() { return targetPoints; }
    public float EnemySpawnDelay
    {
        get { return enemySpawnDelay; }
        set { enemySpawnDelay = value; }
    }
    public bool IsSpawn
    {
        get { return isSpawn; }
        set { isSpawn = value; }
    }
}

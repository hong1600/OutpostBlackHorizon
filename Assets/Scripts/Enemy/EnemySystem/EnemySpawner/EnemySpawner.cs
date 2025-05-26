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
    Round round;
    EnemyFactory enemyFactory;

    public Vector3[] enemySpawnPos { get; private set; } = new Vector3[4];
    [SerializeField] List<Transform> enemySpawnPointList;
    [SerializeField] Transform centerPoint;
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
        enemyFactory = FactoryManager.instance.EnemyFactory;
        round = GameManager.instance.Round;
        enemySpawnDelay = 0;

        enemySpawnPos[0] = new Vector3(-9f, 0, 0);
        enemySpawnPos[1] = new Vector3(-3f, 0, 0);
        enemySpawnPos[2] = new Vector3(3f, 0, 0);
        enemySpawnPos[3] = new Vector3(9f, 0, 0);
    }

    public void SpawnEnemy()
    {
        if (!isSpawn || round.CurRound == 0) { return; }
        
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

            enemySpawnDelay = 6f;
        }

        enemySpawnDelay -= Time.deltaTime;
    }

    IEnumerator StartSpawn(int _spawnPoint)
    {
        for (int i = 0; i < 3; i++)
        {
            EEnemy eEnemy = (EEnemy)Random.Range(0, 4);

            Vector3[] spawnPos = new Vector3[4];
            Vector3 basePos = enemySpawnPointList[_spawnPoint].transform.position;

            for (int j = 0; j < 4; j++)
            {
                spawnPos[j] = basePos + (enemySpawnPos[j]);
                spawnPos[j].y = terrain.SampleHeight(spawnPos[j]);
            }

            for (int j = 0; j < 3; j++)
            {
                enemyFactory.Create(eEnemy, spawnPos[j], Quaternion.identity, null, null);
            }

            yield return new WaitForSeconds(2f);
        }
    }

    public List<Transform> EnemySpawnPointList() { return enemySpawnPointList; }
    public Transform GetCenterPoint() { return centerPoint; }
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

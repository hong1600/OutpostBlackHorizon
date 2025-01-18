using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Mathematics;
using Random = UnityEngine.Random;

public interface IEnemySpawner
{
    void SubEnemySpawn(Action _listener);
    void UnEnemySpawn(Action _listener);
    void SpawnEnemy();

    float GetEnemySpawnDelay();
    void SetEnemySpawnDelay(float _value);
    Transform[] GetEnemySpawnPoints();
    Transform GetTargetPoint();
}

public class EnemySpawner : MonoBehaviour, IEnemySpawner
{
    private event Action onEnemySpawn;

    [SerializeField] List<GameObject> enemyList;
    [SerializeField] Transform[] enemySpawnPoints;
    [SerializeField] Vector3[] enemySpawnPos = new Vector3[4];
    [SerializeField] Transform targetPoint;
    [SerializeField] float enemySpawnDelay;

    private void Awake()
    {
        enemySpawnDelay = 0;

        enemySpawnPos[0] = new Vector3(-3, 0, 0);
        enemySpawnPos[1] = new Vector3(-1, 0, 0);
        enemySpawnPos[2] = new Vector3(1, 0, 0);
        enemySpawnPos[3] = new Vector3(3, 0, 0);
    }

    public void SpawnEnemy()
    {
        if (Shared.gameMng.iRound.GetCurRound() == 0 || Shared.gameMng.iRound.GetIsBossRound()) { return; }

        int firstSpawnPoint = Random.Range(0, enemySpawnPoints.Length);
        int secondSpawnPoint;
        do 
        {
            secondSpawnPoint = Random.Range(0, enemySpawnPoints.Length);
        }while (firstSpawnPoint == secondSpawnPoint);

        EEnemy eEnemy = (EEnemy)Random.Range(0, 4);


        if (enemySpawnDelay <= 0)
        {
            for (int i = 0; i < 1; i++)
            {
                GameObject obj1 = Shared.objectPoolMng.iEnemyPool.FindEnemy(EEnemy.SLIME);

                obj1.transform.position = enemySpawnPoints[firstSpawnPoint].transform.position + (enemySpawnPos[i]);

                //GameObject obj2 = Shared.objectPoolMng.iEnemyPool.FindEnemy(eEnemy);
                //
                //obj2.transform.position = enemySpawnPoints[secondSpawnPoint].transform.position + (enemySpawnPos[i]);

                onEnemySpawn?.Invoke();
            }

            enemySpawnDelay = 100f;
        }

        enemySpawnDelay -= Time.deltaTime;
    }

    public void SubEnemySpawn(Action _listener) { onEnemySpawn += _listener; }
    public void UnEnemySpawn(Action _listener) { onEnemySpawn -= _listener; }
    public float GetEnemySpawnDelay() { return enemySpawnDelay; }
    public void SetEnemySpawnDelay(float _value) { enemySpawnDelay = _value; }
    public Transform[] GetEnemySpawnPoints() { return enemySpawnPoints; }
    public Transform GetTargetPoint() { return targetPoint; }
}

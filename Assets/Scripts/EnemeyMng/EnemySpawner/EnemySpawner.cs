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
    Transform GetTargetPoint();
}

public class EnemySpawner : MonoBehaviour, IEnemySpawner
{
    event Action onEnemySpawn;

    [SerializeField] List<GameObject> enemyList;
    [SerializeField] List<Transform> enemySpawnPointList;
    [SerializeField] Vector3[] enemySpawnPos = new Vector3[4];
    [SerializeField] Transform targetPoint;
    [SerializeField] float enemySpawnDelay;

    private void Start()
    {
        enemySpawnDelay = 0;

        enemySpawnPointList = Shared.gameMng.iFieldBuilder.GetEnemySpawnPointList();

        enemySpawnPos[0] = new Vector3(-3f, 0, 0);
        enemySpawnPos[1] = new Vector3(-1f, 0, 0);
        enemySpawnPos[2] = new Vector3(1f, 0, 0);
        enemySpawnPos[3] = new Vector3(3f, 0, 0);
    }

    public void SpawnEnemy()
    {
        if (Shared.gameMng.iRound.GetCurRound() == 0 || Shared.gameMng.iRound.GetIsBossRound()) { return; }

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

            for (int i = 0; i < 4; i++)
            {
                GameObject obj1 = Shared.objectPoolMng.iEnemyPool.FindEnemy(eEnemy1);
                GameObject obj2 = Shared.objectPoolMng.iEnemyPool.FindEnemy(eEnemy2);

                obj1.transform.position = enemySpawnPointList[firstSpawnPoint].transform.position + (enemySpawnPos[i]);
                obj2.transform.position = enemySpawnPointList[secondSpawnPoint].transform.position + (enemySpawnPos[i]);

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
    public Transform GetTargetPoint() { return targetPoint; }
}

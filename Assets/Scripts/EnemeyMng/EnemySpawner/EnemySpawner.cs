using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IEnemySpawner
{
    void SubEnemySpawn(Action _listener);
    void UnEnemySpawn(Action _listener);
    void SpawnEnemy();

    float GetEnemySpawnDelay();
    void SetEnemySpawnDelay(float _value);
    Transform GetEnemySpawnPoint();
    Transform[] GetWayPoint();
}

public class EnemySpawner : MonoBehaviour, IEnemySpawner
{
    private event Action onEnemySpawn;

    public GameObject EnemyHpBar;
    public List<GameObject> enemyList;
    public Transform enemySpawnPoint;
    public Transform[] wayPoints;
    public float enemySpawnDelay;
    public int curEnemy;

    private void Awake()
    {
        curEnemy = 0;
        enemySpawnDelay = 0;
    }

    public void SpawnEnemy()
    {
        if (Shared.gameMng.iRound.GetCurRound() == 0 || Shared.gameMng.iRound.GetIsBossRound()) { return; }

        if (enemySpawnDelay <= 0)
        {
            GameObject obj = Shared.objectPoolMng.iEnemyPool.FindEnemy(
                enemyList[Shared.gameMng.iRound.GetCurRound()].name);

            obj.transform.position = enemySpawnPoint.transform.position;

            Enemy enemy = obj.GetComponent<Enemy>();

            enemySpawnDelay = 0.85f;

            onEnemySpawn?.Invoke();
        }

        enemySpawnDelay -= Time.deltaTime;
    }

    public void SubEnemySpawn(Action _listener) { onEnemySpawn += _listener; }
    public void UnEnemySpawn(Action _listener) { onEnemySpawn -= _listener; }
    public float GetEnemySpawnDelay() { return enemySpawnDelay; }
    public void SetEnemySpawnDelay(float _value) { enemySpawnDelay = _value; }
    public Transform GetEnemySpawnPoint() { return enemySpawnPoint; }
    public Transform[] GetWayPoint() { return wayPoints; }
}

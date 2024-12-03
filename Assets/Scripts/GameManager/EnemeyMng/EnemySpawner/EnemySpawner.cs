using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemySpawner
{
    void spawnEnemy();
    void setEnemySpawnDelay(float value);
    Transform getEnemySpawnPoint();
    Transform[] getWayPoint();
}

public class EnemySpawner : MonoBehaviour, IEnemySpawner
{
    public Timer timer;
    public ISpawnTime iSpawnTime;
    public Round round;
    public IRound iRound;
    public EnemyMng enemyMng;
    public IEnemyMng iEnemyMng;

    public Transform[] wayPoint;
    public Transform enemySpawnPoint;
    public List<GameObject> enemy;
    public float enemySpawnDelay;

    private void Awake()
    {
        iSpawnTime = timer;
        iRound = round;
        iEnemyMng = enemyMng;
        enemySpawnDelay = 0;
    }

    public void spawnEnemy()
    {
        if (enemySpawnDelay <= 0)
        {
            Instantiate(enemy[iRound.getCurRound()], enemySpawnPoint.transform.position,
            Quaternion.identity, iEnemyMng.getEnemyParent().transform);
            enemySpawnDelay = 0.85f;
        }
        enemySpawnDelay -= Time.deltaTime;
    }

    public void setEnemySpawnDelay(float value) { enemySpawnDelay = value; }
    public Transform getEnemySpawnPoint() { return enemySpawnPoint; }
    public Transform[] getWayPoint() { return wayPoint; }
}

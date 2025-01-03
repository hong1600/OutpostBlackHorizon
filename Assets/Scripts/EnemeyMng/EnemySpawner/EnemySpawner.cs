using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemySpawner
{
    void SpawnEnemy();
    float GetEnemySpawnDelay();
    void SetEnemySpawnDelay(float _value);
    Transform GetEnemySpawnPoint();
    Transform[] GetWayPoint();
}

public class EnemySpawner : MonoBehaviour, IEnemySpawner
{
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
            GameObject obj = 
                Instantiate(enemyList[Shared.gameMng.iRound.GetCurRound()-1], enemySpawnPoint.transform.position,
                Quaternion.identity, Shared.enemyMng.enemyParent.transform);

            Enemy enemy = obj.GetComponent<Enemy>();

            enemySpawnDelay = 0.85f;
        }
        enemySpawnDelay -= Time.deltaTime;
    }

    public float GetEnemySpawnDelay() {  return enemySpawnDelay; }
    public void SetEnemySpawnDelay(float _value) { enemySpawnDelay = _value; }
    public Transform GetEnemySpawnPoint() { return enemySpawnPoint; }
    public Transform[] GetWayPoint() { return wayPoints; }
}

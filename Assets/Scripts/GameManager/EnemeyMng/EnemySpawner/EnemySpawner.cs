using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemySpawner
{
    void setEnemySpawnDelay(float value);
    Transform getEnemySpawnPoint();
}

public class EnemySpawner : MonoBehaviour, IEnemySpawner
{
    public Timer timer;
    public ISpawnTime iSpawnTime;
    public Round round;
    public IRound iRound;
    public EnemyMng enemyMng;
    public IEnemyMng iEnemyMng;

    public Transform enemySpawnPoint;
    public List<GameObject> enemy;
    public float enemySpawnDelay;

    public void spawnEnemy()
    {
        if (enemySpawnDelay < 0 && iSpawnTime.isSpawnTime()
            && !iRound.isBossRound())
        {
            Instantiate(enemy[iRound.getCurRound()], enemySpawnPoint.transform.position,
            Quaternion.identity, iEnemyMng.getEnemyParent().transform);
            enemySpawnDelay = 0.85f;
        }
        if (iRound.isBossRound() && enemySpawnDelay < 0)
        {
            Instantiate(enemy[iRound.getCurRound()], enemySpawnPoint.transform.position,
            Quaternion.identity, iEnemyMng.getEnemyParent().transform);
            enemySpawnDelay = 100f;
            iSpawnTime.setSpawnTime(false);
        }
        enemySpawnDelay -= Time.deltaTime;
    }

    public void setEnemySpawnDelay(float value) { enemySpawnDelay = value; }
    public Transform getEnemySpawnPoint() { return enemySpawnPoint; }

}

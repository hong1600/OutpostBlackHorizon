using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INormalEnemy
{
    void spawnNormalEnemy();
    void setEnemySpawnDelay(float value);
    Transform getEnemySpawnPoint();
}

public class NormalEnemy : MonoBehaviour, INormalEnemy
{
    public ISpawnTime iSpawnTime;
    public IRound iRound;
    public IEnemyMng iEnemyMng;

    public Transform enemySpawnPoint;
    public List<GameObject> enemy;
    public float enemySpawnDelay;
    public bool canSpawn;

    private void Update()
    {
        if (canSpawn)
        {
            spawnNormalEnemy();
        }
    }

    public void spawnNormalEnemy()
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
    public Transform getEnemySpawnPoint() {  return enemySpawnPoint; }
}

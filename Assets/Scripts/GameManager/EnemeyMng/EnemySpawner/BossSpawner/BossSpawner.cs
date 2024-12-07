using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IBossSpawner
{
    void spawnBoss();
}

public class BossSpawner : MonoBehaviour, IBossSpawner
{
    public EnemySpawner enemySpawner;
    public IEnemySpawner iEnemySpawner;
    public EnemyMng enemyMng;
    public IEnemyMng iEnemyMng;
    public Timer timer;
    public ISpawnTime iSpawnTime;

    public GameObject boss;

    public void spawnBoss()
    {
        if (iEnemySpawner.getEnemySpawnDelay() < 0)
        {
            GameObject obj = Instantiate(boss, iEnemySpawner.getEnemySpawnPoint().transform.position,
            Quaternion.identity, iEnemyMng.getEnemyParent().transform);

            Enemy enemy = obj.GetComponent<Enemy>();

            iEnemySpawner.initEnemy(enemy);

            iEnemySpawner.setEnemySpawnDelay(100f);

            iSpawnTime.setSpawnTime(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IBoss
{
    void spawnBoss();
}

public class Boss : EnemySpawner, IBoss
{
    public void spawnBoss()
    {
        if (enemySpawnDelay < 0)
        {
            Instantiate(enemy[iRound.getCurRound()], enemySpawnPoint.transform.position,
            Quaternion.identity, iEnemyMng.getEnemyParent().transform);
            enemySpawnDelay = 100f;
            iSpawnTime.setSpawnTime(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IBossSpawner
{
    void SpawnBoss();
}

public class BossSpawner : MonoBehaviour, IBossSpawner
{
    public GameObject boss;

    public void SpawnBoss()
    {
        if (Shared.enemyMng.iEnemySpawner.GetEnemySpawnDelay() < 0)
        {
            GameObject obj = Instantiate(boss, Shared.enemyMng.iEnemySpawner.GetEnemySpawnPoint().transform.position,
            Quaternion.identity, Shared.enemyMng.enemyParent.transform);

            Enemy enemy = obj.GetComponent<Enemy>();

            Shared.enemyMng.iEnemySpawner.SetEnemySpawnDelay(100f);

            Shared.gameMng.iSpawnTimer.SetIsSpawnTime(false);
        }
    }
}

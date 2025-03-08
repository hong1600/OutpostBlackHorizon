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
    [SerializeField] GameObject boss;

    public void SpawnBoss()
    {
        if (Shared.enemyManager.iEnemySpawner.GetEnemySpawnDelay() < 0)
        {
            int rand = Random.Range(0, Shared.enemyManager.iEnemySpawner.GetEnemySpawnPointList().Count);
            GameObject obj = Instantiate(boss, Shared.enemyManager.iEnemySpawner.GetEnemySpawnPointList()[rand].transform.position,
            Quaternion.identity, Shared.enemyManager.iEnemyMng.GetEnemyParent()[5].transform);

            Enemy enemy = obj.GetComponent<Enemy>();

            Shared.enemyManager.iEnemySpawner.SetEnemySpawnDelay(100f);

            Shared.gameManager.SpawnTimer.SetIsSpawnTime(false);
        }
    }
}

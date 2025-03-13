using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    EnemyManager enemyManager;
    EnemySpawner enemySpawner;

    [SerializeField] GameObject boss;

    private void Start()
    {
        enemyManager = Shared.enemyManager;
        enemySpawner = Shared.enemyManager.EnemySpawner;
    }

    public void SpawnBoss()
    {
        if (enemySpawner.GetEnemySpawnDelay() < 0)
        {
            int rand = Random.Range(0, enemySpawner.GetEnemySpawnPointList().Count);
            GameObject obj = Instantiate(boss, enemySpawner.GetEnemySpawnPointList()[rand].transform.position,
            Quaternion.identity, enemyManager.GetEnemyParent()[5].transform);

            Enemy enemy = obj.GetComponent<Enemy>();

            enemySpawner.SetEnemySpawnDelay(100f);

            Shared.gameManager.SpawnTimer.SetIsSpawnTime(false);
        }
    }
}

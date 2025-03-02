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
        if (Shared.enemyMng.iEnemySpawner.GetEnemySpawnDelay() < 0)
        {
            int rand = Random.Range(0, Shared.enemyMng.iEnemySpawner.GetEnemySpawnPointList().Count);
            GameObject obj = Instantiate(boss, Shared.enemyMng.iEnemySpawner.GetEnemySpawnPointList()[rand].transform.position,
            Quaternion.identity, Shared.enemyMng.iEnemyMng.GetEnemyParent()[5].transform);

            Enemy enemy = obj.GetComponent<Enemy>();

            Shared.enemyMng.iEnemySpawner.SetEnemySpawnDelay(100f);

            Shared.gameMng.SpawnTimer.SetIsSpawnTime(false);
        }
    }
}

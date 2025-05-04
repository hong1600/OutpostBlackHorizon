using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    ObjectPoolManager poolManager;

    [SerializeField] Transform bossSpawnPos;
    [SerializeField] UIBossHpbar hpbar;

    private void Start()
    {
        GameManager.instance.Round.onBossRound += SpawnBoss;
        poolManager = ObjectPoolManager.instance;
    }

    public void SpawnBoss()
    {
        hpbar.ShowHpBar();

        GameObject boss = poolManager.EnemyPool.FindEnemy
            (EEnemy.ROBOT6, bossSpawnPos.position, Quaternion.Euler(0,180,0));
    }
}

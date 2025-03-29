using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] Transform bossSpawnPos;
    [SerializeField] UIBossHpbar hpbar;

    private void Start()
    {
        Shared.gameManager.Round.onBossRound += SpawnBoss;
    }

    public void SpawnBoss()
    {
        //hpbar.ShowHpBar();

        GameObject boss = Shared.objectPoolManager.EnemyPool.FindEnemy
            (EEnemy.ROBOT6, bossSpawnPos.position, Quaternion.Euler(0,180,0));
    }
}

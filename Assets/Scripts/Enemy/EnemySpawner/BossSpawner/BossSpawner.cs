using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    EnemyFactory enemyFactory;

    [SerializeField] Transform bossSpawnPos;
    [SerializeField] UIBossHpbar hpbar;

    public GameObject bossObj { get; private set; }

    private void Start()
    {
        GameManager.instance.Round.onBossRound += SpawnBoss;
        enemyFactory = FactoryManager.instance.EnemyFactory;
    }

    public void SpawnBoss()
    {
        hpbar.ShowHpBar();

        bossObj = enemyFactory.Create(EEnemy.ROBOT6, bossSpawnPos.position, Quaternion.Euler(0, 180, 0), null, null);
    }
}

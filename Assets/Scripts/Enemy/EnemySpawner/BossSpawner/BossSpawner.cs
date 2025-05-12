using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    Terrain terrain;

    EnemyFactory enemyFactory;

    [SerializeField] Transform bossSpawnPos;
    [SerializeField] UIBossHpbar hpbar;

    public GameObject bossObj { get; private set; }

    private void Awake()
    {
        terrain = Terrain.activeTerrain;
    }

    private void Start()
    {
        GameManager.instance.Round.onBossRound += SpawnBoss;
        enemyFactory = FactoryManager.instance.EnemyFactory;
    }

    public void SpawnBoss()
    {
        hpbar.ShowHpBar();

        Vector3 spawnPos = bossSpawnPos.position;
        spawnPos.y = terrain.SampleHeight(spawnPos);

        bossObj = enemyFactory.Create(EEnemy.ROBOT6, spawnPos, Quaternion.Euler(0, 180, 0), null, null);
    }
}

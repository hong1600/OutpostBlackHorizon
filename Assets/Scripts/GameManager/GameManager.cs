using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] SpawnTimer spawnTimer;
    [SerializeField] Round round;
    [SerializeField] GameState gameState;
    [SerializeField] GoldCoin goldCoin;
    [SerializeField] Rewarder rewarder;
    [SerializeField] ViewState viewState;

    EnemySpawner enemySpawner;
    WaveBossSpawner waveBossSpawner;
    BossSpawner bossSpawner;

    private void Awake()
    {
        if (Shared.gameManager == null)
        {
            Shared.gameManager = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        SpawnTimer = spawnTimer;
        Round = round;
        GameState = gameState;
        GoldCoin = goldCoin;
        Rewarder = rewarder;
        ViewState = viewState;
    }

    private void Start()
    {
        enemySpawner = Shared.enemyManager.EnemySpawner;
        waveBossSpawner = Shared.enemyManager.WaveBossSpawner;
        bossSpawner = Shared.enemyManager.BossSpawner;
    }

    private void Update()
    {
        if (!round.GetIsBossRound())
        {
            if (spawnTimer.GetIsSpawnTime())
            {
                enemySpawner.SpawnEnemy();
            }
        }
        else if (round.GetIsBossRound())
        {
            bossSpawner.SpawnBoss();
        }

        waveBossSpawner.SpawnWaveBossTime();
    }

    public SpawnTimer SpawnTimer { get; private set; }
    public Round Round { get; private set; }
    public GameState GameState { get; private set; }
    public GoldCoin GoldCoin { get; private set; }
    public Rewarder Rewarder { get; private set; }
    public ViewState ViewState { get; private set; }
}

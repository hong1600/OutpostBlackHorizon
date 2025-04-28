using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] Timer timer;
    [SerializeField] Round round;
    [SerializeField] GameState gameState;
    [SerializeField] GoldCoin goldCoin;
    [SerializeField] Rewarder rewarder;
    [SerializeField] ViewState viewState;

    EnemySpawner enemySpawner;
    WaveBossSpawner waveBossSpawner;
    BossSpawner bossSpawner;

    protected override void Awake()
    {
        base.Awake();

        Timer = timer;
        Round = round;
        GameState = gameState;
        GoldCoin = goldCoin;
        Rewarder = rewarder;
        ViewState = viewState;
    }

    private void Start()
    {
        enemySpawner = EnemyManager.instance.EnemySpawner;
        waveBossSpawner = EnemyManager.instance.WaveBossSpawner;
        bossSpawner = EnemyManager.instance.BossSpawner;
    }

    private void Update()
    {
        if (enemySpawner.IsSpawn)
        {
            enemySpawner.SpawnEnemy();
        }
    }

    public Timer Timer { get; private set; }
    public Round Round { get; private set; }
    public GameState GameState { get; private set; }
    public GoldCoin GoldCoin { get; private set; }
    public Rewarder Rewarder { get; private set; }
    public ViewState ViewState { get; private set; }
}

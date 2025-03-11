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
    [SerializeField] FieldBuild fieldBuild;

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
        FieldBuild = fieldBuild;
    }

    private void Update()
    {
        if (!round.GetIsBossRound())
        {
            if (spawnTimer.GetIsSpawnTime())
            {
                Shared.enemyManager.iEnemySpawner.SpawnEnemy();
            }
        }
        else if(round.GetIsBossRound())
        {
            Shared.enemyManager.iBossSpawner.SpawnBoss();
        }

        Shared.enemyManager.iWaveBossSpawner.SpawnWaveBossTime();
    }

    public SpawnTimer SpawnTimer { get; private set; }
    public Round Round { get; private set; }
    public GameState GameState { get; private set; }
    public GoldCoin GoldCoin { get; private set; }
    public Rewarder Rewarder { get; private set; }
    public ViewState ViewState { get; private set; }
    public FieldBuild FieldBuild { get; private set; }
}

using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMng : MonoBehaviour
{
    [SerializeField] SpawnTimer spawnTimer;
    [SerializeField] Round round;
    [SerializeField] GameState gameState;
    [SerializeField] GoldCoin goldCoin;
    [SerializeField] Rewarder rewarder;
    [SerializeField] SpeedUp speedUp;
    [SerializeField] FieldBuilder fieldBuilder;
    [SerializeField] ViewState viewState;
    public ISpawnTimer iSpawnTimer;
    public IRound iRound;
    public IGameState iGameState;
    public IGoldCoin iGoldCoin;
    public IRewarder iRewarder;
    public ISpeedUp iSpeedUp;
    public IFieldBuilder iFieldBuilder;
    public IViewState iViewState;

    private void Awake()
    {
        if (Shared.gameMng == null)
        {
            Shared.gameMng = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        iSpawnTimer = spawnTimer;
        iRound = round;
        iGameState = gameState;
        iGoldCoin = goldCoin;
        iRewarder = rewarder;
        iSpeedUp = speedUp;
        iFieldBuilder = fieldBuilder;
        iViewState = viewState;
    }

    private void Update()
    {
        if (!iRound.GetIsBossRound())
        {
            if (iSpawnTimer.GetIsSpawnTime())
            {
                Shared.enemyMng.iEnemySpawner.SpawnEnemy();
            }
        }
        else if(iRound.GetIsBossRound())
        {
            Shared.enemyMng.iBossSpawner.SpawnBoss();
        }

        Shared.enemyMng.iWaveBossSpawner.SpawnWaveBossTime();
    }
}

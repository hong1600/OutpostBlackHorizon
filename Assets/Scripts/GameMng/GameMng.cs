using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMng : MonoBehaviour
{
    public SpawnTimer spawnTimer;
    public ISpawnTimer iSpawnTimer;
    public Round round;
    public IRound iRound;
    public GameState gameState;
    public IGameState iGameState;
    public GoldCoin goldCoin;
    public IGoldCoin iGoldCoin;
    public Rewarder rewarder;
    public IRewarder iRewarder;
    public SpeedUp speedUp;
    public ISpeedUp iSpeedUp;

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
    }

    private void Update()
    {
        if (!iRound.GetIsBossRound())
        {
            iSpawnTimer.Timer();

            if (iSpawnTimer.GetIsSpawnTime())
            {
                Shared.enemyMng.enemySpawner.SpawnEnemy();
            }
        }
        else if(iRound.GetIsBossRound())
        {
            Shared.enemyMng.iBossSpawner.SpawnBoss();
        }

        Shared.enemyMng.iWaveBossSpawner.SpawnWaveBossTime();
    }
}

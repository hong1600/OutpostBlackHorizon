using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Timer timer;
    public ITimer iTimer;
    public ISpawnTime iSpawnTime;
    public Round round;
    public IRound iRound;
    public EnemySpawner enemySpawner;
    public IEnemySpawner iEnemySpawner;
    public WaveBossSpawner waveBossSpawner;
    public IWaveBossSpawner iWaveBossSpawner;
    public BossSpawner bossSpawner;
    public IBossSpawner iBossSpawner;

    private void Awake()
    {
        if(Instance == null) 
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        iTimer = timer;
        iSpawnTime = timer;
        iRound = round;
        iEnemySpawner = enemySpawner;
        iWaveBossSpawner = waveBossSpawner;
        iBossSpawner = bossSpawner;
    }

    private void Update()
    {
        iWaveBossSpawner.spawnWaveBossTime();
        if (!iRound.isBossRound())
        {
            iTimer.timer();
            if (iSpawnTime.isSpawnTime())
            {
                iEnemySpawner.spawnEnemy();
            }
        }
        else if(iRound.isBossRound())
        {
            iBossSpawner.spawnBoss();
        }
    }
}

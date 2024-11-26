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
    public Boss boss;
    public IBoss iBoss;

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
    }

    private void Update()
    {
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
            iBoss.spawnBoss();
        }
    }
}

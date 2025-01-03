using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMng : MonoBehaviour
{
    public EnemySpawner enemySpawner;
    public IEnemySpawner iEnemySpawner;
    public BossSpawner bossSpawner;
    public IBossSpawner iBossSpawner;
    public WaveBossSpawner waveBossSpawner;
    public IWaveBossSpawner iWaveBossSpawner;

    public GameObject enemyParent;
    public int maxEnemyCount;
    public int curEnemyCount;

    private void Awake()
    {
        if (Shared.enemyMng == null)
        {
            Shared.enemyMng = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        iEnemySpawner = enemySpawner;
        iBossSpawner = bossSpawner;
        iWaveBossSpawner = waveBossSpawner;

        maxEnemyCount = 100;
        curEnemyCount = 0;
    }

    public int EnemyCount()
    {
        curEnemyCount = enemyParent.transform.childCount;
        return curEnemyCount;
    }
}

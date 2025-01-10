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
    List<GameObject> enemyCountList = new List<GameObject>();


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

    private void Start()
    {
        for (int i = 0; i < enemyParent.transform.childCount; i++)
        {
            enemyCountList.Add(enemyParent.transform.GetChild(i).gameObject);
        }
    }

    public int EnemyCount()
    {
        curEnemyCount = 0;

        for (int i = 0; i < enemyCountList.Count; i++)
        {
            if (enemyCountList[i].activeInHierarchy)
            {
                curEnemyCount++;
            }
        }

        return curEnemyCount;
    }
}

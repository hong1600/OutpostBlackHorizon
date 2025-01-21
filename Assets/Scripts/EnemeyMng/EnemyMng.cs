using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyMng
{
    int GetMaxEnemy();
    int GetCurEnemy();
    GameObject[] GetEnemyParent();
}

public class EnemyMng : MonoBehaviour, IEnemyMng
{
    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] BossSpawner bossSpawner;
    [SerializeField] WaveBossSpawner waveBossSpawner;
    public IEnemyMng iEnemyMng;
    public IEnemySpawner iEnemySpawner;
    public IBossSpawner iBossSpawner;
    public IWaveBossSpawner iWaveBossSpawner;

    [SerializeField] GameObject[] enemyParents;
    [SerializeField] int maxEnemy;
    [SerializeField] int curEnemy;
    [SerializeField] List<GameObject> enemyCountList = new List<GameObject>();

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

        iEnemyMng = this;
        iEnemySpawner = enemySpawner;
        iBossSpawner = bossSpawner;
        iWaveBossSpawner = waveBossSpawner;

        maxEnemy = 100;
        curEnemy = 0;
    }

    private void Update()
    {
        EnemyCount();
    }

    private int EnemyCount()
    {
        int curEnemy = 0;

        for (int i = 0; i < enemyParents.Length; i++)
        {
            for (int j = 0; j < enemyParents[i].transform.childCount; j++)
            {
                Transform child = enemyParents[i].transform.GetChild(j);

                if (child.gameObject.activeInHierarchy)
                {
                    curEnemy++;
                }
            }
        }

        return curEnemy;
    }

    public int GetMaxEnemy() { return maxEnemy; }
    public int GetCurEnemy() { return curEnemy; }
    public GameObject[] GetEnemyParent() { return enemyParents; }
}

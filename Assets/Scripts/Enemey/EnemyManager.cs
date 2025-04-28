using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] WaveBossSpawner waveBossSpawner;
    [SerializeField] BossSpawner bossSpawner;

    [SerializeField] List<GameObject> enemyParentList;
    [SerializeField] int maxEnemy;
    [SerializeField] int curEnemy;
    [SerializeField] List<GameObject> enemyCountList = new List<GameObject>();

    public EnemySpawner EnemySpawner { get; private set; }
    public WaveBossSpawner WaveBossSpawner { get; private set; }
    public BossSpawner BossSpawner { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        maxEnemy = 100;
        curEnemy = 0;

        EnemySpawner = enemySpawner;
        WaveBossSpawner = waveBossSpawner;
        BossSpawner = bossSpawner;
    }

    public void OnEnemySpawn()
    {
        curEnemy++;
    }

    public void OnEnemyDie()
    {
        curEnemy--;
    }

    public int GetMaxEnemy() { return maxEnemy; }
    public int GetCurEnemy() { return curEnemy; }
    public List<GameObject> GetEnemyParent() { return enemyParentList; }
}

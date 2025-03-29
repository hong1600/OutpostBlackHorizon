using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public event Action onEnemyCountEvent;

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

    private void Awake()
    {
        if (Shared.enemyManager == null)
        {
            Shared.enemyManager = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        maxEnemy = 100;
        curEnemy = 0;

        EnemySpawner = enemySpawner;
        WaveBossSpawner = waveBossSpawner;
        BossSpawner = bossSpawner;
    }

    private void Start()
    {
        enemySpawner.onEnemySpawn += EnemyCount;
    }

    private void EnemyCount()
    {
        curEnemy = 0;

        for (int i = 0; i < enemyParentList.Count; i++)
        {
            for (int j = 0; j < enemyParentList[i].transform.childCount; j++)
            {
                Transform child = enemyParentList[i].transform.GetChild(j);

                if (child.gameObject.activeInHierarchy)
                {
                    curEnemy++;
                    onEnemyCountEvent?.Invoke();
                }
            }
        }
    }

    public int GetMaxEnemy() { return maxEnemy; }
    public int GetCurEnemy() { return curEnemy; }
    public List<GameObject> GetEnemyParent() { return enemyParentList; }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyMng
{
    event Action onEnemyCountEvent;
    int GetMaxEnemy();
    int GetCurEnemy();
    List<GameObject> GetEnemyParent();
}

public class EnemyMng : MonoBehaviour, IEnemyMng
{
    public event Action onEnemyCountEvent;

    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] BossSpawner bossSpawner;
    [SerializeField] WaveBossSpawner waveBossSpawner;
    public IEnemyMng iEnemyMng;
    public IEnemySpawner iEnemySpawner;
    public IBossSpawner iBossSpawner;
    public IWaveBossSpawner iWaveBossSpawner;

    [SerializeField] List<GameObject> enemyParentList;
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

    private void Start()
    {
        iEnemySpawner.UnEnemySpawn(EnemyCount);
        iEnemySpawner.SubEnemySpawn(EnemyCount);
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
                }
            }
        }

        onEnemyCountEvent?.Invoke();
    }

    public int GetMaxEnemy() { return maxEnemy; }
    public int GetCurEnemy() { return curEnemy; }
    public List<GameObject> GetEnemyParent() { return enemyParentList; }
}

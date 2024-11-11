using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyMng : MonoBehaviour
{
    private GameManager gameManager;

    public EnemySpawner enemySpawner;
    public WaveBossSpawner waveBossSpawner;

    public GameObject enemyParent;
    public int maxEnemyCount;
    public int curEnemyCount;

    public EnemyMng(GameManager manager)
    {
        gameManager = manager;

        enemySpawner.initialize(this);
        waveBossSpawner.initialized(this);
    }

    public int enemyCount()
    {
        curEnemyCount = enemyParent.transform.childCount;
        return curEnemyCount;
    }
}

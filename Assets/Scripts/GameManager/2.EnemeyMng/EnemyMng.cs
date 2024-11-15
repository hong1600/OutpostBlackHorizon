using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMng : GameManager
{
    public EnemyMngData enemyMngData;

    public GameObject enemyParent;
    public int maxEnemyCount;
    public int curEnemyCount;

    private void Awake()
    {
        maxEnemyCount = 0;
        curEnemyCount = 0;
        enemyMngData = Resources.Load<EnemyMngData>("GameManager/EnemyMngData/EnemyMngData");
        enemyParent = enemyMngData.enemyParent;
    }

    public int enemyCount()
    {
        curEnemyCount = enemyParent.transform.childCount;
        return curEnemyCount;
    }
}

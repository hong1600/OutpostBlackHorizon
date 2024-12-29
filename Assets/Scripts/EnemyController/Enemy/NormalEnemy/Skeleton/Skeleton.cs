using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : NormalEnemy
{
    public EnemyData enemyData;

    public void Start()
    {
        initEnemyData(enemyData);
    }
}

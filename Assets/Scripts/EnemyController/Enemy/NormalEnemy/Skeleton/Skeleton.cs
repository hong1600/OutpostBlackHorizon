using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : NormalEnemy
{
    [SerializeField] EnemyData enemyData;

    private void Start()
    {
        base.InitEnemyData(enemyData);
    }
}

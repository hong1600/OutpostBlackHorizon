using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : NormalEnemy
{
    public EnemyData enemyData;

    private void Awake()
    {
        initEnemy(enemyData);
    }
}

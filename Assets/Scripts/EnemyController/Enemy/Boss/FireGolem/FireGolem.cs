using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGolem : Boss
{
    public EnemyData enemyData;

    private void Start()
    {
        initEnemyData(enemyData);
    }
}

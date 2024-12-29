using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : Boss
{
    public EnemyData enemyData;

    private void Start()
    {
        initEnemyData(enemyData);
    }
}

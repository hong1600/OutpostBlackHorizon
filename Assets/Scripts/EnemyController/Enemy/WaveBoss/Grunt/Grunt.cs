using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt : WaveBoss
{
    public EnemyData enemyData;

    private void Start()
    {
        initWaveBoss();
        initEnemyData(enemyData);
    }
}

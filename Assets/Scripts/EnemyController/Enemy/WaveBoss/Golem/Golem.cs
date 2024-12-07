using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : WaveBoss
{
    public EnemyData enemyData;

    private void Start()
    {
        initWaveBoss();
        initEnemyData(enemyData);
    }
}

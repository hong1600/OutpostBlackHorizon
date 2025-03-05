using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : NormalEnemy
{

    private void Start()
    {
        base.InitEnemyData(DataManager.instance.TableEnemy.Get(201));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot1 : NormalEnemy
{
    TableEnemy.Info info;

    private void Start()
    {
        info = DataManager.instance.TableEnemy.Get(201);

        base.InitEnemyData(info.Name, info.MaxHp, info.Speed, info.AttackRange, info.AttackDmg, EEnemy.ROBOT1);
    }
}

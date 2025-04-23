using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot4 : NormalEnemy
{
    TableEnemy.Info info;

    private void Start()
    {
        info = DataManager.instance.TableEnemy.Get(204);

        base.InitEnemyData(info.Name, info.MaxHp, info.Speed, info.AttackRange, info.AttackDmg, EEnemy.ROBOT4);
    }
}

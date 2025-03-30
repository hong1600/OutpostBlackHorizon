using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot3 : NormalEnemy
{
    TableEnemy.Info info;

    private void Awake()
    {
        info = DataManager.instance.TableEnemy.Get(203);

        base.InitEnemyData(info.Name, info.MaxHp, info.Speed, info.AttackRange, info.AttackDmg, EEnemy.ROBOT3);
    }

    protected override IEnumerator StartAttack()
    {
        yield return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunslinger : RangedUnit
{
    public UnitData unitData;

    private void Awake()
    {
        Init(unitData);
    }

    protected override IEnumerator OnDamageEvent(Enemy enemy, int _damage)
    {


        return base.OnDamageEvent(enemy, _damage);
    }
}

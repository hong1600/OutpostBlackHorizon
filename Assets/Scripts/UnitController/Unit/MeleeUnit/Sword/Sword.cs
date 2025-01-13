using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MeleeUnit
{
    public UnitData unitData;

    private void Awake()
    {
        Init(unitData);
    }

    protected override IEnumerator StartAttack()
    {
        yield return base.StartAttack();
    }
}

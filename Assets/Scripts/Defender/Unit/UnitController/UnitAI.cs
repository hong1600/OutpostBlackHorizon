using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAI : DefenderAIState
{
    protected UnitBase unit;

    public void Init(UnitBase _unit)
    {
        base.Init(_unit);
        unit = _unit;
    }

    public override void Attack()
    {
        if (unit.isSkill)
        {
            aiState = EDefenderAI.SKILL;
            return;
        }

        if (unit.isAttack)
        {
            return;
        }
        else
        {
            base.Attack();
        }
    }

    public override void Skill()
    {
        if (unit.isSkill == false)
        {
            aiState = EDefenderAI.SEARCH;
            return;
        }

        base.Skill();
    }
}

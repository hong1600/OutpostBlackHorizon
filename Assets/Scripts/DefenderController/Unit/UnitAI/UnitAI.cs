using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class UnitAI
{
    protected Unit unit;

    public EUnitAI aiState = EUnitAI.CREATE;

    public void Init(Unit _unit)
    {
        unit = _unit;
    }

    public void State()
    {
        switch (aiState)
        {
            case EUnitAI.CREATE:
                Create();
                break;
            case EUnitAI.SEARCH:
                Search();
                break;
            case EUnitAI.ATTACK:
                Attack();
                break;
            case EUnitAI.SKILL:
                Skill();
                break;
            case EUnitAI.RESET:
                Reset();
                break;
        }
    }

    public virtual void Create()
    {
        aiState = EUnitAI.SEARCH;
    }

    public virtual void Search()
    {
        if (unit.target != null)
        {
            aiState = EUnitAI.ATTACK;
        }

        unit.TargetEnemy();
    }

    public virtual void Attack()
    {
        if (unit.isSkill)
        {
            aiState = EUnitAI.SKILL;
            return;
        }
        if (unit.target == null)
        {
            aiState = EUnitAI.SEARCH;
        }

        unit.Attack();
        unit.LookEnemy();
    }

    public virtual void Skill()
    {
        if (unit.isSkill == false)
        {
            aiState = EUnitAI.SEARCH;
            return;
        }
        unit.Attack();
        unit.LookEnemy();
    }

    public virtual void Reset()
    {
    }
}

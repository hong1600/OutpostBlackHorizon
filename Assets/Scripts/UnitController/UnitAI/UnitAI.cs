using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class UnitAI
{
    public Unit unit;

    public EUnitAI aiState = EUnitAI.CREATE;

    public void Init(Unit _unit)
    {
        unit = _unit;
    }

    public void State()
    {
        switch(aiState) 
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
        GameObject target = unit.TargetEnemy();

        if (target != null)
        {
            unit.target = target;
            aiState = EUnitAI.ATTACK;
        }
    }

    public virtual void Attack()
    {
        if(unit.target != null)
        {
            unit.Attack();
            unit.LookEnemy();
        }
        else
        {
            aiState = EUnitAI.SEARCH;
        }
    }

    public virtual void Reset()
    {
    }
}

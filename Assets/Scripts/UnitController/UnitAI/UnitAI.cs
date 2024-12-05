using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public enum eUnitAI
{
    CREATE,
    SEARCH,
    ATTACK,
    RESET,
}

public class UnitAI
{
    public Unit unit;

    public eUnitAI AIState = eUnitAI.CREATE;

    public void init(Unit _unit)
    {
        unit = _unit;
    }

    public void State()
    {
        switch(AIState) 
        {
            case eUnitAI.CREATE:
                Create();
                break;
            case eUnitAI.SEARCH:
                Search();
                break;
            case eUnitAI.ATTACK:
                Attack();
                break;
            case eUnitAI.RESET:
                Reset();
                break;
        }
    }

    public virtual void Create()
    {
        AIState = eUnitAI.SEARCH;
    }

    public virtual void Search()
    {
        unit.changeAnim(eUnitAI.SEARCH);
        GameObject target = unit.targetEnemy();

        if (target != null)
        {
            unit.target = target;
            AIState = eUnitAI.ATTACK;
        }
    }

    public virtual void Attack()
    {
        if(unit.target != null)
        {
            unit.attack();
            unit.lookEnemy();
            unit.changeAnim(eUnitAI.ATTACK);
        }
        else
        {
            AIState = eUnitAI.SEARCH;
        }
    }

    public virtual void Reset()
    {
    }
}

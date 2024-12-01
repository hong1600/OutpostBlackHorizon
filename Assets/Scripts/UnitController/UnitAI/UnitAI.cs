using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public enum eUnitAI
{
    eAI_CREATE,
    eAI_SEARCH,
    eAI_ATTACK,
    eAI_RESET,
}

public class UnitAI
{
    public Unit unit;

    public eUnitAI AIState = eUnitAI.eAI_CREATE;

    public void init(Unit _unit)
    {
        unit = _unit;
    }

    public void State()
    {
        switch(AIState) 
        {
            case eUnitAI.eAI_CREATE:
                Create();
                break;
            case eUnitAI.eAI_SEARCH:
                Search();
                break;
            case eUnitAI.eAI_ATTACK:
                Attack();
                break;
            case eUnitAI.eAI_RESET:
                Reset();
                break;
        }
    }

    public virtual void Create()
    {
        AIState = eUnitAI.eAI_SEARCH;
    }

    public virtual void Search()
    {
        unit.changeAnim(eUnitAI.eAI_SEARCH);
        GameObject target = unit.targetEnemy();

        if (target != null)
        {
            unit.target = target;
            AIState = eUnitAI.eAI_ATTACK;
        }
    }

    public virtual void Attack()
    {
        if(unit.target != null)
        {
            unit.attack();
            unit.changeAnim(eUnitAI.eAI_ATTACK);
        }
        else
        {
            AIState = eUnitAI.eAI_SEARCH;
        }
    }

    public virtual void Reset()
    {
        unit.target = null;
        AIState = eUnitAI.eAI_SEARCH;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderAI 
{
    protected DefenderBase defender;

    public EDefenderAI aiState = EDefenderAI.CREATE;

    public void Init(DefenderBase _defender)
    {
        defender = _defender;
    }

    public void State()
    {
        switch (aiState)
        {
            case EDefenderAI.CREATE:
                Create();
                break;
            case EDefenderAI.SEARCH:
                Search();
                break;
            case EDefenderAI.ATTACK:
                Attack();
                break;
            case EDefenderAI.SKILL:
                Skill();
                break;
            case EDefenderAI.RESET:
                Reset();
                break;
        }
    }

    public virtual void Create()
    {
        aiState = EDefenderAI.SEARCH;
    }

    public virtual void Search()
    {
        if (defender.target != null)
        {
            aiState = EDefenderAI.ATTACK;
        }

        defender.TargetEnemy();
    }

    public virtual void Attack()
    {
        defender.TargetEnemy();

        if (defender.target == null || Vector3.Distance
            (defender.target.transform.position, defender.transform.position) > defender.attackRange)
        {
            aiState = EDefenderAI.SEARCH;
        }

        defender.Attack();
        defender.LookTarget();
    }

    public virtual void Skill()
    {
        defender.Attack();
        defender.LookTarget();
    }

    public virtual void Reset()
    {
    }
}

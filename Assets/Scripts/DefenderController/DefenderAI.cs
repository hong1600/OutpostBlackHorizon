using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class DefenderAI
{
    protected Defender defender;

    public EDefenderAI aiState = EDefenderAI.CREATE;

    public void Init(Defender _defender)
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
        if (defender.target == null)
        {
            aiState = EDefenderAI.SEARCH;
        }

        defender.Attack();
        defender.LookEnemy();
    }

    public virtual void Skill()
    {
        defender.Attack();
        defender.LookEnemy();
    }

    public virtual void Reset()
    {
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAI : DefenderAI
{
    public UnitBase unit;

    public void Init(UnitBase _unit)
    {
        this.unit = _unit;
        base.Init(_unit);
        SetState(new UnitSearchState(this));
    }
}

public abstract class UnitAIState : DefenderAIState
{
    protected UnitBase unit;
    protected UnitAI unitAI;

    public UnitAIState(StateMachine _machine) : base(_machine)
    {
        unitAI = (UnitAI)_machine;
        this.unit = unitAI.unit;
    }
}

public class UnitSearchState : UnitAIState
{
    public UnitSearchState(StateMachine machine) : base(machine) { }

    public override void Enter()
    {
        unit.ChangeAnim(EDefenderAI.SEARCH);
    }

    public override void Execute()
    {
        unit.TargetEnemy();

        if (unit.target != null) 
        {
            machine.SetState(new UnitAttackState(machine));
        }
    }
}

public class UnitAttackState : DefenderAttackState
{
    UnitBase unit;

    public UnitAttackState(StateMachine _machine) : base(_machine)
    {
        unit = (UnitBase)defender;
    }

    public override void Enter()
    {
        unit.ChangeAnim(EDefenderAI.ATTACK);
    }

    public override void Execute()
    {
        if (unit.isAttack)
        {
            defender.LookTarget();
            return;
        }
        else
        {
            base.Execute();
        }
    }
}

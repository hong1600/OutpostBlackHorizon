using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DefenderAIState
{
    protected DefenderBase defender;

    public DefenderAIState(DefenderBase _defender)
    {
        this.defender = _defender;
    }

    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();
}

public class DefenderCreateState : DefenderAIState
{
    public DefenderCreateState(DefenderBase _defender) : base(_defender) { }

    public override void Enter() { }
    public override void Execute() 
    {
        defender.SetState(new DefenderSearchState(defender));
    }
    public override void Exit() { }
}

public class DefenderSearchState : DefenderAIState
{
    public DefenderSearchState(DefenderBase _defender) : base(_defender) { }

    public override void Enter() { }

    public override void Execute()
    {
        defender.TargetEnemy();

        if (defender.target != null)
        {
            defender.SetState(new DefenderAttackState(defender));
        }
    }

    public override void Exit() { }
}

public class DefenderAttackState : DefenderAIState
{
    public DefenderAttackState(DefenderBase _defender) : base(_defender) { }

    public override void Enter() { }

    public override void Execute()
    {
        defender.TargetEnemy();

        if (defender.target == null || Vector3.Distance
            (defender.target.transform.position, defender.transform.position) > defender.attackRange)
        {
            defender.SetState(new DefenderSearchState(defender));
        }

        defender.Attack();
        defender.LookTarget();
    }

    public override void Exit() { }
}

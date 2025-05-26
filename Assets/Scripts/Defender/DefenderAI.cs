using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderAI : StateMachine
{
    public DefenderBase defender;

    public void Init(DefenderBase _defender)
    {
        this.defender = _defender;
        SetState(new DefenderCreateState(this));
    }
}

public abstract class DefenderAIState : AIState
{
    protected DefenderBase defender;
    protected DefenderAI defenderAI;

    public DefenderAIState(StateMachine _machine) : base(_machine)
    {
        defenderAI = (DefenderAI)_machine;
        this.defender = defenderAI.defender;
    }
}

public class DefenderCreateState : DefenderAIState
{
    public DefenderCreateState(StateMachine _machine) : base(_machine) { }

    public override void Execute() 
    {
        machine.SetState(new DefenderSearchState(machine));
    }
}

public class DefenderSearchState : DefenderAIState
{
    public DefenderSearchState(StateMachine _machine) : base(_machine) { }

    public override void Execute()
    {
        defender.TargetEnemy();

        if (defender.target != null)
        {
            machine.SetState(new DefenderAttackState(machine));
        }
    }
}

public class DefenderAttackState : DefenderAIState
{
    public DefenderAttackState(StateMachine _machine) : base(_machine) { }

    public override void Execute()
    {
        defender.TargetEnemy();

        if (defender.target == null || Vector3.Distance
            (defender.target.transform.position, defender.transform.position) > defender.attackRange)
        {
            machine.SetState(new DefenderSearchState(machine));
        }

        defender.Attack();
        defender.LookTarget();
    }
}

public class DefenderSkillState : DefenderAIState
{
    public DefenderSkillState(StateMachine _machine) : base(_machine) { }

    public override void Execute()
    {
    }
}

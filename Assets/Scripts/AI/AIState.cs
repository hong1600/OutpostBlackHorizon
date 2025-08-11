using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIState
{
    protected StateMachine machine;

    public AIState(StateMachine _machine)
    {
        this.machine = _machine;
    }

    public virtual void Enter() { }
    public virtual void Execute() { }
    public virtual void Exit() { }
}

public abstract class StateMachine
{
    protected AIState curState = null;

    public void SetState(AIState _newState)
    {
        if (curState != null)
        {
            curState.Exit();
        }

        curState = _newState;

        if (curState != null)
        {
            curState.Enter();
        }
    }

    public virtual void Update()
    {
        if(curState != null) 
        {
            curState.Execute();
        }
    }
}

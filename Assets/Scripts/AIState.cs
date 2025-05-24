using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIState
{
    public abstract void Enter();
    public abstract void Exit();
    public virtual void Execute() { }
}

public abstract class StateMachine : MonoBehaviour
{
    protected AIState curState;

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

    protected virtual void Update()
    {
        if(curState != null) 
        {
            curState.Execute();
        }
    }
}

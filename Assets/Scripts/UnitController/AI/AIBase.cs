using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public enum eAI 
{
    eAI_CREATE,
    eAI_SEARCH,
    eAI_MOVE,
    eAI_RESET,
}

public class AIBase
{
    public Character character;

    public eAI AIState = eAI.eAI_CREATE;

    public void Init(Character _Character)
    {
        character = _Character;
    }

    public void State()
    {
        switch(AIState) 
        {
            case eAI.eAI_CREATE:
                Create();
                break;
            case eAI.eAI_SEARCH:
                Search();
                break;
            case eAI.eAI_MOVE:
                Move();
                break;
            case eAI.eAI_RESET:
                Reset();
                break;
        }
    }

    public virtual void Create()
    {
        AIState = eAI.eAI_SEARCH;
    }
    public virtual void Search()
    {
        AIState = eAI.eAI_MOVE;
    }
    public virtual void Move()
    {
        AIState = eAI.eAI_SEARCH;
    }
    public virtual void Reset()
    {
        AIState = eAI.eAI_SEARCH;
    }
}

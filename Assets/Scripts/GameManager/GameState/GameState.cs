using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameState
{
    EGameState GetGameState();
    void SetGameState(EGameState _state);
}

public class GameState : MonoBehaviour, IGameState
{
    public event Action onGameFinish;

    [SerializeField] EGameState curGameState;

    private void Awake()
    {
        curGameState = EGameState.PLAYING;
    }

    public void SetGameState(EGameState _state)
    { 
        curGameState = _state; 

        if(_state == EGameState.GAMECLEAR) 
        {
            onGameFinish?.Invoke();
        }
    }

    public EGameState GetGameState() 
    { 
        return curGameState;
    }
}

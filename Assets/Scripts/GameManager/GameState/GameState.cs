using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public event Action<EGameState> onGameFinish;

    [SerializeField] EGameState curGameState;

    private void Awake()
    {
        curGameState = EGameState.PLAYING;
    }

    public void SetGameState(EGameState _state)
    { 
        curGameState = _state; 

        if(_state == EGameState.GAMECLEAR || _state == EGameState.GAMEOVER) 
        {
            onGameFinish?.Invoke(_state);
        }
    }

    public EGameState GetGameState() 
    { 
        return curGameState;
    }
}

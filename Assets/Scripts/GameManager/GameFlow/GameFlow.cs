using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameFlow
{
    GameState getGameState();
    void setGameState(GameState state);
}

public class GameFlow : MonoBehaviour, IGameFlow
{
    public GameState curGameState;

    public bool gameOver;
    public bool gameClear;

    private void Awake()
    {
        curGameState = GameState.Playing;
    }

    public GameState getGameState() { return curGameState; }
    public void setGameState(GameState state) { curGameState = state; }
}

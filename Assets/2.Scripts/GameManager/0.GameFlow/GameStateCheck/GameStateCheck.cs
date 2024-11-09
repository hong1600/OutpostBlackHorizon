using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateCheck : MonoBehaviour
{
    public GameFlow gameFlow;

    public bool gameOver;
    public bool gameClear;

    public void initialized(GameFlow manager)
    {
        gameFlow = manager;
    }

    public void checkGameState()
    {

    }
}

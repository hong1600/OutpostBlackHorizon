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

    private void Awake()
    {
        gameOver = false;
        gameClear = false;

    }

    public void checkGameState()
    {

    }
}

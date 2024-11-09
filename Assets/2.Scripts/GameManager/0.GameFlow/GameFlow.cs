using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[System.Serializable]
public class GameFlow : MonoBehaviour
{
    private GameManager gameManager;

    public RoundTimer roundTimer;
    public GameClear gameClear;
    public Rewarder rewarder;
    public GameStateCheck gameStateCheck;
    public GameOver gameOver;

    private void Awake()
    {
        roundTimer.initialized(this);
        gameClear.initialized(this);
        gameOver.initialized(this);
        gameStateCheck.initialized(this);
        rewarder.initialized(this);
    }


    public GameFlow(GameManager manager)
    {
        gameManager = manager;
    }
}

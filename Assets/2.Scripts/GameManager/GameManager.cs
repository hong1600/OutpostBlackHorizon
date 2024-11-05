using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameFlow gameFlow;
    public UnitMng unitMng;
    public EnemyMng enemyMng;

    public int myGold;
    public int myCoin;

    private void Awake()
    {
        if(Instance == null) 
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        gameFlow = new GameFlow(this);
        unitMng = new UnitMng(this);
        enemyMng = new EnemyMng(this);
    }

    private void Update()
    {
        if (gameFlow.gameStateCheck.gameOver) return;

        gameFlow.roundTimer.timer();
        enemyMng.enemySpawner.spawnEnemy();
        gameFlow.gameStateCheck.checkGameState();
    }
}

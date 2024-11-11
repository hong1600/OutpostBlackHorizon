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
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        myGold = 2000;
        myCoin = 50;

        unitMng = FindObjectOfType<UnitMng>();
        gameFlow = FindObjectOfType<GameFlow>();
        enemyMng = FindObjectOfType<EnemyMng>();
    }

    private void Update()
    {
        if (gameFlow.gameStateCheck.gameOver) return;

        gameFlow.roundTimer.timer();
        enemyMng.enemySpawner.spawnEnemy();
        gameFlow.gameStateCheck.checkGameState();
        unitMng.checkGround();
    }

    public void adGold(int amount)
    {
        myGold += amount;
    }

    public void adCoin(int amount)
    {
        myCoin += amount;
    }
}

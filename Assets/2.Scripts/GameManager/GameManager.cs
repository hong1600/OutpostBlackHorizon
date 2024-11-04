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
    public RewardGameOverMng rewardGameOverMng;
    public UpgradeMng upgradeMng;

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
        rewardGameOverMng = new RewardGameOverMng(this);
        upgradeMng = new UpgradeMng(this);
    }

    private void Update()
    {
        if (rewardGameOverMng.gameOver) return;

        gameFlow.timer();
        enemyMng.spawnEnemy();
        rewardGameOverMng.checkGameOver();
    }
}

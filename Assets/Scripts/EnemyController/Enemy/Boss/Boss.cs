using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Boss : Enemy
{
    public GameState gamestate;

    public TextMeshProUGUI bossTimeText;
    public float bosstime;

    private void Awake()
    {
        bosstime = 60f;
    }

    public override void die()
    {
        base.die();
        iGoldCoin.setGold(300);
        iGoldCoin.setCoin(4);
        iRound.setBossRound(false);
        iTimer.setSec(15f);
        iEnemySpawner.setEnemySpawnDelay(0.85f);
    }

    public void bossTimer()
    {
        bosstime -= Time.deltaTime;
        bossTimeText.text = bosstime.ToString("F1") + "s";

        if (bosstime <= 0)
        {
            gamestate = GameState.GameOver;
        }
    }
}

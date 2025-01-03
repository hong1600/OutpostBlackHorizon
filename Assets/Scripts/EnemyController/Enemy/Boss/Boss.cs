using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Boss : Enemy
{
    public EGameState gameState;
    public TextMeshProUGUI bossTimeText;
    public float bossTime;

    private void Awake()
    {
        bossTime = 60f;
    }

    public override void Die()
    {
        base.Die();
        Shared.gameMng.iGoldCoin.SetGold(300);
        Shared.gameMng.iGoldCoin.SetCoin(4);
        Shared.gameMng.iRound.SetBossRound(false);
        Shared.gameMng.iSpawnTimer.SetSec(15f);
        Shared.enemyMng.iEnemySpawner.SetEnemySpawnDelay(0.85f);
    }

    public void bossTimer()
    {
        bossTime -= Time.deltaTime;
        bossTimeText.text = bossTime.ToString("F1") + "s";

        if (bossTime <= 0)
        {
            gameState = EGameState.GAMEOVER;
        }
    }
}

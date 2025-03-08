using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField] TextMeshProUGUI bossTimeText;
    [SerializeField] float bossTime;

    private void Awake()
    {
        bossTime = 60f;
    }

    protected internal override void Die()
    {
        base.Die();
        Shared.gameManager.GoldCoin.SetGold(300);
        Shared.gameManager.GoldCoin.SetCoin(4);
        Shared.gameManager.Round.SetBossRound(false);
        Shared.gameManager.SpawnTimer.SetSec(15f);
        Shared.enemyManager.iEnemySpawner.SetEnemySpawnDelay(0.85f);
    }

    public void bossTimer()
    {
        bossTime -= Time.deltaTime;
        bossTimeText.text = bossTime.ToString("F1") + "s";

        if (bossTime <= 0)
        {
            //gameState = EGameState.GAMEOVER;
        }
    }
}

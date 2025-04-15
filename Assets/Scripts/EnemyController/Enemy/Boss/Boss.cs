using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Boss : Enemy
{
    UIBossHpbar hpbar;

    private void Start()
    {
        hpbar = GameUI.instance.UIBossHpbar;

        hpbar.Init(this);
    }

    protected internal override void Die()
    {
        Die();
        goldCoin.SetGold(300);
        goldCoin.SetCoin(4);
        round.isBossRound = false;
        timer.SetSec(15f);
        enemySpawner.EnemySpawnDelay = 0.85f;

        GameManager.instance.GameState.SetGameState(EGameState.GAMECLEAR);
    }
}

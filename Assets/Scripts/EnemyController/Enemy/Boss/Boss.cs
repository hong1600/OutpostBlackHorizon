using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Boss : Enemy
{
    [SerializeField] UIBossHpbar hpbar;

    protected override void InitEnemyData(string _name, float _maxHp, float _spd, float _range, float _dmg, EEnemy _eEnemy)
    {
        //hpbar.Init(this);
    }

    protected internal override void Die()
    {
        Die();
        goldCoin.SetGold(300);
        goldCoin.SetCoin(4);
        round.isBossRound = false;
        timer.SetSec(15f);
        enemySpawner.EnemySpawnDelay = 0.85f;

        Shared.gameManager.GameState.SetGameState(EGameState.GAMECLEAR);
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField] TextMeshProUGUI bossTimeText;
    [SerializeField] float bossTime;

    protected override void Awake()
    {
        bossTime = 60f;
    }

    protected internal override void Die()
    {
        Die();
        goldCoin.SetGold(300);
        goldCoin.SetCoin(4);
        round.SetBossRound(false);
        spawnTimer.SetSec(15f);
        enemySpawner.SetEnemySpawnDelay(0.85f);
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

    protected override IEnumerator StartAttack()
    {
        throw new System.NotImplementedException();
    }
}

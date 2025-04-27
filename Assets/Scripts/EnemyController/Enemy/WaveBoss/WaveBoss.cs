using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class WaveBoss : Enemy
{
    WaveBossSpawner waveBossSpawner;

    private void Start()
    {
        waveBossSpawner = EnemyManager.instance.WaveBossSpawner;
    }

    protected override void Die()
    {
        base.Die();
        goldCoin.SetGold(10);
        goldCoin.SetCoin(10);
    }
}

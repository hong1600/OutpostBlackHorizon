using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class WaveBoss : Enemy
{
    WaveBossSpawner waveBossSpawner;

    private void Start()
    {
        waveBossSpawner = Shared.enemyManager.WaveBossSpawner;
    }

    protected internal override void Die()
    {
        Die();
        goldCoin.SetCoin(2);
    }
}

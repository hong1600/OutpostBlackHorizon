using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class WaveBoss : Enemy
{
    public float waveBossTime;
    public TextMeshProUGUI waveBossText;

    public void InitWaveBoss()
    {
        waveBossTime = 25f;
    }

    public override void Die()
    {
        base.Die();
        Shared.gameMng.iGoldCoin.SetCoin(2);
        Shared.enemyMng.iWaveBossSpawner.SetWaveBossDelay(25f);
    }

    //public void WaveBossTimer()
    //{
    //    waveBossTime -= Time.deltaTime;
    //    waveBossText.text = waveBossTime.ToString("F1") + "s";

    //    if (waveBossTime <= 0)
    //    {
    //        Destroy(this.gameObject);
    //        iWaveBossSpawner.setWaveBossDelay(25f);
    //    }
    //}
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class WaveBoss : Enemy
{
    [SerializeField] float waveBossDelay;
    [SerializeField] TextMeshProUGUI waveBossText;

    private void Start()
    {
        waveBossDelay = 25f;
    }

    protected internal override void Die()
    {
        base.Die();
        Shared.gameMng.GoldCoin.SetCoin(2);
        Shared.enemyMng.iWaveBossSpawner.SetWaveBossDelay(waveBossDelay);
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

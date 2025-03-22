using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class WaveBoss : Enemy
{
    WaveBossSpawner waveBossSpawner;

    [SerializeField] float waveBossDelay;
    [SerializeField] TextMeshProUGUI waveBossText;

    private void Start()
    {
        waveBossSpawner = Shared.enemyManager.WaveBossSpawner;
    }

    protected internal override void Die()
    {
        Die();
        goldCoin.SetCoin(2);
        waveBossSpawner.SetWaveBossDelay(waveBossDelay);
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

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveBoss : Enemy
{
    public float waveBossTime;
    public TextMeshProUGUI waveBossText;

    public void initWaveBoss()
    {
        waveBossTime = 25f;
    }

    private void Update()
    {
        waveBossTimer();
    }

    public override void die()
    {
        base.die();
        iGoldCoin.setCoin(2);
        iWaveBossSpawner.setWaveBossDelay(25f);
    }

    public void waveBossTimer()
    {
        waveBossTime -= Time.deltaTime;
        waveBossText.text = waveBossTime.ToString("F1") + "s";

        if (waveBossTime <= 0)
        {
            Destroy(this.gameObject);
            waveBossSpawner.wavebossDelay = 25f;
        }
    }
}

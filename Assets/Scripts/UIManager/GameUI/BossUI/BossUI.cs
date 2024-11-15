using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossUI : MonoBehaviour
{
    public WaveBossSpawner waveBossSpawner;

    public TextMeshProUGUI bossTimer;
    public TextMeshProUGUI waveBossLevelNameText;
    public GameObject spawnWaveBossBtn;

    public void waveBoss()
    {
        waveBossLevelNameText.text = $"LV.{waveBossSpawner.waveBossLevel} µ¹ °ñ·½";
    }
}

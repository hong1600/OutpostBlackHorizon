using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossUI : MonoBehaviour
{
    public IWaveBoss iWaveBoss;

    public TextMeshProUGUI bossTimer;
    public TextMeshProUGUI waveBossLevelNameText;
    public GameObject spawnWaveBossBtn;

    public void waveBoss()
    {
        waveBossLevelNameText.text = $"LV.{iWaveBoss.getWaveBossLevel()} µ¹ °ñ·½";
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUIWaveBossPanel : MonoBehaviour
{
    public WaveBossSpawner waveBossSpawner;
    public IWaveBossSpawner iWaveBossSpawner;

    public TextMeshProUGUI waveBossLevelNameText;

    private void Awake()
    {
        iWaveBossSpawner = waveBossSpawner;
    }

    public void waveBossPanel()
    {
        waveBossLevelNameText.text = $"LV.{iWaveBossSpawner.getWaveBossLevel()} µ¹ °ñ·½";
    }
}

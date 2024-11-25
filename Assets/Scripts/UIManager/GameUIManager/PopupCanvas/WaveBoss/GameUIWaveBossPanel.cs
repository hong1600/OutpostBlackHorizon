using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUIWaveBossPanel : MonoBehaviour
{
    public WaveBoss waveBoss;
    public IWaveBoss iWaveBoss;

    public TextMeshProUGUI waveBossLevelNameText;

    private void Awake()
    {
        iWaveBoss = waveBoss;
    }

    public void waveBossPanel()
    {
        waveBossLevelNameText.text = $"LV.{iWaveBoss.getWaveBossLevel()} µ¹ °ñ·½";
    }
}

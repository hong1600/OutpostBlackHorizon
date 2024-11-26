using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIWaveBossBtn : MonoBehaviour
{
    public WaveBoss waveBoss;
    public IWaveBoss iWaveBoss;

    public GameObject waveBossPanel;

    private void Awake()
    {
        iWaveBoss = waveBoss;
    }

    public void clickWaveBossPanelBtn()
    {
        waveBossPanel.SetActive(true);
    }

    public void clickWaveBossSpawnBtn()
    {
        iWaveBoss.spawnWaveBoss();
        waveBossPanel.SetActive(false);
    }
}

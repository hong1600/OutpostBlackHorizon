using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIWaveBossBtn : MonoBehaviour
{
    public WaveBossSpawner waveBossSpawner;
    public IWaveBossSpawner iWaveBossSpawner;

    public GameObject waveBossPanel;

    private void Awake()
    {
        iWaveBossSpawner = waveBossSpawner;
    }

    public void clickWaveBossPanelBtn()
    {
        waveBossPanel.SetActive(true);
    }

    public void clickWaveBossSpawnBtn()
    {
        iWaveBossSpawner.spawnWaveBoss();
        waveBossPanel.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWaveBossBtn : MonoBehaviour
{
    public GameObject waveBossPanel;

    public void ClickWaveBossPanelBtn()
    {
        waveBossPanel.SetActive(true);
    }

    public void ClickWaveBossSpawnBtn()
    {
        Shared.enemyMng.iWaveBossSpawner.SpawnWaveBoss();
        waveBossPanel.SetActive(false);
    }
}

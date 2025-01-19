using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIWaveBossPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI waveBossLevelNameText;

    private void waveBossPanel()
    {
        waveBossLevelNameText.text = $"LV.{Shared.enemyMng.iWaveBossSpawner.GetWaveBossLevel()} µ¹ °ñ·½";
    }
}

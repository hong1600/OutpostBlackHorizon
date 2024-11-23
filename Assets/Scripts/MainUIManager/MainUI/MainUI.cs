using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public ItemState curItems;
    public MainBtn btn;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            DataManager.instance.playerdata.gold += 10000;
        }
    }

    public void showPanelOpen(GameObject targetPanel)
    {
        if (targetPanel.activeSelf == false)
        {
            targetPanel.SetActive(true);
        }
        targetPanel.transform.localScale = Vector3.zero;
        targetPanel.transform.DOScale(Vector3.one, 0.2f);
    }
}

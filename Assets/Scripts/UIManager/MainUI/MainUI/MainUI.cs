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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            DataMng.instance.playerData.gold += 10000;
        }
    }
}

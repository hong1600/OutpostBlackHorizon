using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMixBtn : MonoBehaviour
{
    [SerializeField] GameObject mixPanel;
    [SerializeField] GameObject upgradePanel;
    [SerializeField] GameObject randomPanel;


    public void ClickMixPanelBtn()
    {
        if (upgradePanel.activeSelf || randomPanel.activeSelf)
        {
            upgradePanel.SetActive(false);
            randomPanel.SetActive(false);
        }

        UIMng.instance.OpenPanel(mixPanel);
    }

    public void ClickMixSpawnBtn()
    {
        StartCoroutine(Shared.unitMng.UnitMixer.StartUnitMixSpawn());
    }
}

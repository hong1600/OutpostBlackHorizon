using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMixBtn : MonoBehaviour
{
    UnitMixer unitMixer;

    [SerializeField] GameObject mixPanel;
    [SerializeField] GameObject upgradePanel;
    [SerializeField] GameObject randomPanel;

    private void Start()
    {
        unitMixer = UnitManager.instance.UnitMixer;
    }

    public void ClickMixPanelBtn()
    {
        if(upgradePanel.activeSelf) upgradePanel.SetActive(false);
        if (randomPanel.activeSelf) randomPanel.SetActive(false);

        UIManager.instance.OpenPanel(mixPanel, null, null, null, false);
    }

    public void ClickMixSpawnBtn()
    {
        StartCoroutine(unitMixer.StartUnitMixSpawn());
    }
}

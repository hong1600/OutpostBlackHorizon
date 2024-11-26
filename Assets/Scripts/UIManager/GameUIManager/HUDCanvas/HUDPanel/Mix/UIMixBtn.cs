using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMixBtn : MonoBehaviour
{
    public UnitMixer unitMixer;
    public IUnitMixer iUnitMixer;

    public GameObject mixPanel;

    private void Awake()
    {
        iUnitMixer = unitMixer;
    }

    public void clickMixPanelBtn()
    {
        BtnManager.instance.openPanel(mixPanel);
    }

    public void clickMixSpawnBtn()
    {
        iUnitMixer.unitMixSpawn();
    }
}

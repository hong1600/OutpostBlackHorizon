using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIMixBtn : MonoBehaviour
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
        mixPanel.SetActive(true);
    }
    public void clickMixSpawnBtn()
    {
        iUnitMixer.unitMixSpawn();
    }
}

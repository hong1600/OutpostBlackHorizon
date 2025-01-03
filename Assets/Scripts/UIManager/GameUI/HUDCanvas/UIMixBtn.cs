using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMixBtn : MonoBehaviour
{
    public GameObject mixPanel;

    public void ClickMixPanelBtn()
    {
        UIMng.instance.OpenPanel(mixPanel);
    }

    public void ClickMixSpawnBtn()
    {
        StartCoroutine(Shared.unitMng.iUnitMixer.StartUnitMixSpawn());
    }
}

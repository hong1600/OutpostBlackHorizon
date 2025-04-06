using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMixSpawnBtn : MonoBehaviour
{
    UnitMixer unitMixer;

    private void Start()
    {
        unitMixer = UnitManager.instance.UnitMixer;
    }

    public void ClickMixSpawn()
    {
        StartCoroutine(unitMixer.StartUnitMixSpawn());
    }
}

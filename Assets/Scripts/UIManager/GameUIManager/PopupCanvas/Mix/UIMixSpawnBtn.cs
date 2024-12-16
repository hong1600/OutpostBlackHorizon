using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMixSpawnBtn : MonoBehaviour
{
    public UnitMixer unitMixer;
    public IUnitMixer iUnitMixer;

    public void Awake()
    {
        iUnitMixer = unitMixer;
    }

    public void onMixSpawn()
    {
        StartCoroutine(iUnitMixer.unitMixSpawn());
    }
}

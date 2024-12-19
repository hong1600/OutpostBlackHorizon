using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFusionBtn : MonoBehaviour
{
    public UnitFusion unitFusion;
    public IUnitFusion iUnitFusion;

    private void Awake()
    {
        iUnitFusion = unitFusion;
    }

    public void onFusion()
    {
        StartCoroutine(iUnitFusion.unitFusion());
    }
}

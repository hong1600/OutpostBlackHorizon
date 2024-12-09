using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : RangedUnit
{
    public UnitData unitData;

    private void Awake()
    {
        Init(unitData);
    }
}

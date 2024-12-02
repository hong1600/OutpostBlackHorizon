using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Unit
{
    public UnitData unitData;

    private void Awake()
    {
        Init(unitData);
    }
}

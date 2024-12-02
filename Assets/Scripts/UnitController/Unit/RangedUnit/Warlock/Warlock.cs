using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warlock : Unit
{
    public UnitData unitData;

    private void Awake()
    {
        Init(unitData);
    }
}

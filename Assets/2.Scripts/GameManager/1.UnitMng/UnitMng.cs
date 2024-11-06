using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UnitMng : MonoBehaviour
{
    public UnitMngData mngData;

    public UnitSpawner unitSpawner;
    public UnitMixer unitMixer;
    public UnitUpgrader unitUpgrader;
    public UnitRandomSpawner unitRandomSpawner;

    public List<Unit> curUnitList = new List<Unit>();
    public List<GameObject> unitSpawnPointList = new List<GameObject>();
    public List<GameObject> unitListSS, unitListS, unitListA, unitListB, unitListC;
    public int groundNum;

    private void Start()
    {
        unitSpawnPointList = mngData.unitSpawnPointList;
    }

    public UnitMng()
    {
        unitSpawner.initialize(this);
        unitMixer.initialize(this);
        unitUpgrader.initialize(this);
        unitRandomSpawner.initialize(this);
    }

    public bool checkGround()
    {
        for (groundNum = 0; groundNum < unitSpawnPointList.Count; groundNum++)
        {
            if (unitSpawnPointList[groundNum].transform.childCount <= 0)
            {
                return true;
            }
        }
        return false;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IUnitMng 
{
    List<GameObject> getUnitList(UnitType unitType);
    List<GameObject> getUnitSpawnPointList();
    int getGroundNum();
    List<Unit> getCurUnitList(); 
}

public class UnitMng : MonoBehaviour, IUnitMng
{
    public UnitMngData unitMngData;

    public List<Unit> curUnitList = new List<Unit>();
    public List<GameObject> unitSpawnPointList = new List<GameObject>();
    public List<GameObject> unitListSS, unitListS, unitListA, unitListB, unitListC = new List<GameObject>();
    public int groundNum;

    private void Awake()
    {
        unitMngData = Resources.Load<UnitMngData>("GameManager/UnitMngData/UnitMngData");

        unitSpawnPointList = unitMngData.unitSpawnPointList;
        unitListSS = unitMngData.unitListSS;
        unitListS = unitMngData.unitListS;
        unitListA = unitMngData.unitListA;
        unitListB = unitMngData.unitListB;
        unitListC = unitMngData.unitListC;
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

    public List<GameObject> getUnitList(UnitType unitType)
    {
        switch (unitType)
        {
            case UnitType.SS: return unitListSS;
            case UnitType.S: return unitListS;
            case UnitType.A: return unitListA;
            case UnitType.B: return unitListB;
            case UnitType.C: return unitListC;
                default: return null;
        }
    }

    public List<GameObject> getUnitSpawnPointList() { return unitSpawnPointList; }
    public int getGroundNum() { return groundNum; }

    public List<Unit> getCurUnitList() { return curUnitList; }
}

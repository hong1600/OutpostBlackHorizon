using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IUnitMng 
{
    event Action onUnitCountChange;
    void addUnit(Unit unit);
    void removeUnit(Unit unit);
    bool checkGround();
    List<GameObject> getUnitList(EUnitGrade unitType);
    List<GameObject> getUnitSpawnPointList();
    int getGroundNum();
    List<Unit> getCurUnitList();
}

public class UnitMng : MonoBehaviour, IUnitMng
{
    public event Action onUnitCountChange;

    public List<Unit> curUnitList = new List<Unit>();
    public List<GameObject> unitSpawnPointList = new List<GameObject>();
    public List<GameObject> unitListSS, unitListS, unitListA, unitListB, unitListC = new List<GameObject>();
    public int groundNum;

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

    public List<GameObject> getUnitList(EUnitGrade unitType)
    {
        switch (unitType)
        {
            case EUnitGrade.SS:
                return unitListSS;
            case EUnitGrade.S:
                return unitListS;
            case EUnitGrade.A:
                return unitListA;
            case EUnitGrade.B:
                return unitListB;
            case EUnitGrade.C:
                return unitListC;
                default: 
                return null;
        }
    }
    public void addUnit(Unit unit)
    {
        curUnitList.Add(unit);
        onUnitCountChange?.Invoke();
    }

    public void removeUnit(Unit unit) 
    {
        curUnitList.Remove(unit);
        onUnitCountChange?.Invoke();
    }

    public List<GameObject> getUnitSpawnPointList() { return unitSpawnPointList; }

    public int getGroundNum() { return groundNum; }

    public List<Unit> getCurUnitList() { return curUnitList; }
}

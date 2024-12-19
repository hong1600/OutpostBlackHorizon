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
    int getGroundNum();
    bool checkGround(GameObject spawnUnit);
    GameObject unitInstantiate(GameObject unit);
    List<GameObject> getUnitList(EUnitGrade unitType);
    List<GameObject> getUnitSpawnPointList();
    List<Unit> getCurUnitList();
}

public class UnitMng : MonoBehaviour, IUnitMng
{
    public event Action onUnitCountChange;

    public UnitSpawner unitSpawner;
    public IUnitSpawner iUnitSpawner;

    public List<Unit> curUnitList = new List<Unit>();
    public List<GameObject> unitSpawnPointList = new List<GameObject>();
    public List<GameObject> unitListSS, unitListS, unitListA, unitListB, unitListC = new List<GameObject>();
    public int groundNum;

    private void Awake()
    {
        iUnitSpawner = unitSpawner;
    }

    public bool checkGround(GameObject spawnUnit)
    {
        groundNum = 0;

        for (groundNum = 0; groundNum < unitSpawnPointList.Count; groundNum++)
        {
            var ground = unitSpawnPointList[groundNum].transform;

            if (ground.childCount < 3 && ground.childCount > 0)
            {
                if (ground.GetChild(0).name == $"{spawnUnit.name}(Clone)")
                {
                    return true;
                }
            }
        }

        for(groundNum = 0 ;groundNum < unitSpawnPointList.Count; groundNum++) 
        {
            var ground = unitSpawnPointList[groundNum].transform;

            if (ground.transform.childCount == 0)
            {
                return true;
            }
        }

        return false;
    }

    public GameObject unitInstantiate(GameObject unit)
    {
        if (groundNum < 0 || groundNum > unitSpawnPointList.Count || unit == null) return null;

        Instantiate(unit, unitSpawnPointList[groundNum].transform.position,
            Quaternion.identity,
            unitSpawnPointList[groundNum].transform);

        curUnitList.Add(unit.GetComponent<Unit>());

        return unit;
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

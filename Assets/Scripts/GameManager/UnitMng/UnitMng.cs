using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IUnitMng 
{
    event Action onUnitCountChange;
    int getGroundNum();
    bool checkGround(GameObject spawnUnit);
    void unitInstantiate(GameObject unit);
    List<GameObject> getUnitByGradeList(EUnitGrade unitType);
    List<GameObject> getUnitByGroundList(int groundNum);
    List<GameObject> getUnitSpawnPointList();
    List<GameObject> getAllUnitList();
    void removeUnitData(GameObject unit);
}

public class UnitMng : MonoBehaviour, IUnitMng
{
    public event Action onUnitCountChange;

    public UnitSpawner unitSpawner;
    public IUnitSpawner iUnitSpawner;

    public List<GameObject> allUnitList = new List<GameObject>();
    public List<GameObject> unitC, unitB, unitA, unitS, unitSS = new List<GameObject>();
    public Dictionary<int, List<GameObject>> unitByGround = new Dictionary<int, List<GameObject>>();
    public List<GameObject> unitSpawnPointList = new List<GameObject>();
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

    public void unitInstantiate(GameObject unit)
    {
        if (groundNum < 0 || groundNum > unitSpawnPointList.Count || unit == null) return;

        Vector3 twoUnit1Pos = new Vector3(-0.5f, 0, 0);
        Vector3 twoUnit2Pos = new Vector3(0.5f, 0, 0);

        Vector3 threeUnit1Pos = new Vector3 (0, 0, 0.4f);
        Vector3 threeUnit2Pos = new Vector3 (-0.6f, 0, -0.4f);
        Vector3 threeUnit3Pos = new Vector3 (0.6f, 0, -0.4f);

        GameObject instantiateUnit;

        if (!unitByGround.ContainsKey(groundNum))
        {
            unitByGround[groundNum] = new List<GameObject>();
        }

        List<GameObject> curGroundUnit = unitByGround[groundNum];
        int unitCount = unitSpawnPointList[groundNum].transform.childCount;

        switch (unitCount) 
        {
            case 0:
                instantiateUnit = Instantiate(unit, unitSpawnPointList[groundNum].transform.position,
                    Quaternion.identity, unitSpawnPointList[groundNum].transform);
                break;

            case 1:
                curGroundUnit[0].transform.position = unitSpawnPointList[groundNum].transform.position + twoUnit1Pos;

                instantiateUnit = Instantiate(unit, unitSpawnPointList[groundNum].transform.position + twoUnit2Pos,
                    Quaternion.identity, unitSpawnPointList[groundNum].transform);
                break;

            case 2:
                curGroundUnit[0].transform.position = unitSpawnPointList[groundNum].transform.position + threeUnit1Pos;
                curGroundUnit[1].transform.position = unitSpawnPointList[groundNum].transform.position + threeUnit2Pos;

                instantiateUnit = Instantiate(unit, unitSpawnPointList[groundNum].transform.position + threeUnit3Pos,
                    Quaternion.identity, unitSpawnPointList[groundNum].transform);
                break;

            default:
                return;
        }

        addUnitData(instantiateUnit);
    }

    public void addUnitData(GameObject unit)
    {
        allUnitList.Add(unit);

        unitByGround[groundNum].Add(unit);

        onUnitCountChange?.Invoke();
    }

    public void removeUnitData(GameObject unit)
    {
        Transform groundTrs = unit.transform.parent.parent;
        groundNum = getSelectGroundNum(groundTrs);

        allUnitList.Remove(unit);
        Destroy(unit);

        unitByGround[groundNum].Clear();

        onUnitCountChange?.Invoke();
    }

    public List<GameObject> getUnitByGroundList(int groundNum)
    {
        if (!unitByGround.ContainsKey(groundNum)) return null;

        return unitByGround[groundNum];
    }

    public List<GameObject> getUnitByGradeList(EUnitGrade unitType)
    {
        switch (unitType)
        {
            case EUnitGrade.SS:
                return unitSS;
            case EUnitGrade.S:
                return unitS;
            case EUnitGrade.A:
                return unitA;
            case EUnitGrade.B:
                return unitB;
            case EUnitGrade.C:
                return unitC;
                default: 
                return null;
        }
    }

    public int getSelectGroundNum(Transform groundTrs)
    {
        if (groundTrs != null)
        {
            Ground ground = groundTrs.GetComponent<Ground>();

            if (ground != null)
            {
                return (int)ground.eGround;
            }
        }

        return -1;
    }

    public List<GameObject> getUnitSpawnPointList() { return unitSpawnPointList; }
    public int getGroundNum() { return groundNum; }
    public List<GameObject> getAllUnitList() { return allUnitList; }
}

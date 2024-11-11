using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitMng : MonoBehaviour
{
    public GameManager gameManager;
    public UnitMngData unitMngData;

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
        unitMngData = Resources.Load<UnitMngData>("GameManager/UnitMngData/UnitMngData");

        unitSpawner = FindObjectOfType<UnitSpawner>();
        unitMixer = FindObjectOfType<UnitMixer>();
        unitUpgrader = FindObjectOfType<UnitUpgrader>();
        unitRandomSpawner = FindObjectOfType<UnitRandomSpawner>();

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
}

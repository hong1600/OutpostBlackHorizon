using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UnitMng : MonoBehaviour
{
    public GameManager gameManager;
    public UnitMngData data;

    public UnitSpawner unitSpawner;
    public UnitMixer unitMixer;
    public UnitUpgrader unitUpgrader;
    public UnitRandomSpawner unitRandomSpawner;

    public List<Unit> curUnitList = new List<Unit>();
    public List<GameObject> unitSpawnPointList = new List<GameObject>();
    public List<GameObject> unitListSS, unitListS, unitListA, unitListB, unitListC;
    public int groundNum;

    public void Initialize(GameManager manager, UnitMngData unitMngdata)
    {
        gameManager = manager;
        data = unitMngdata;

        unitSpawnPointList = data.unitSpawnPointList;
        unitListSS = data.unitListSS;
        unitListS = data.unitListS;
        unitListA = data.unitListA;
        unitListB = data.unitListB;
        unitListC = data.unitListC;

        unitSpawner = gameObject.AddComponent<UnitSpawner>();
        unitSpawner.initialize(this);

        unitMixer = gameObject.AddComponent<UnitMixer>();
        unitMixer.initialize(this);

        unitUpgrader = gameObject.AddComponent<UnitUpgrader>();
        unitUpgrader.initialize(this);

        unitRandomSpawner = gameObject.AddComponent<UnitRandomSpawner>();
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

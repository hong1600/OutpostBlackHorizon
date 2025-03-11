using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitManager : MonoBehaviour
{
    [SerializeField] UnitFusion unitFusion;
    [SerializeField] UnitMixer unitMixer;
    [SerializeField] UnitRandomSpawner unitRandomSpawner;
    [SerializeField] UnitSpawner unitSpawner;
    [SerializeField] UnitUpgrader unitUpgrader;
    [SerializeField] UnitFieldMove unitFieldMove;
    [SerializeField] UnitData unitData;
    [SerializeField] UnitFieldData unitFieldData;

    private void Awake()
    {
        if (Shared.unitManager == null)
        {
            Shared.unitManager = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        UnitFusion = unitFusion;
        UnitMixer = unitMixer;
        UnitRandomSpawner = unitRandomSpawner;
        UnitSpawner = unitSpawner;
        UnitUpgrader = unitUpgrader;
        UnitFieldMove = unitFieldMove;
        UnitData = unitData;
        UnitFieldData = unitFieldData;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            unitSpawner.InstantiateUnit(unitData.GetUnitByGradeList(EUnitGrade.C)[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            unitSpawner.InstantiateUnit(unitData.GetUnitByGradeList(EUnitGrade.B)[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            unitSpawner.InstantiateUnit(unitData.GetUnitByGradeList(EUnitGrade.A)[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            unitSpawner.InstantiateUnit(unitData.GetUnitByGradeList(EUnitGrade.S)[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            unitSpawner.InstantiateUnit(unitData.GetUnitByGradeList(EUnitGrade.SS)[0]);
        }
    }

    public UnitFusion UnitFusion { get; private set; }
    public UnitMixer UnitMixer { get; private set; }
    public UnitRandomSpawner UnitRandomSpawner { get; private set; }
    public UnitSpawner UnitSpawner { get; private set; }
    public UnitUpgrader UnitUpgrader { get; private set; }
    public UnitFieldMove UnitFieldMove { get; private set; }
    public UnitData UnitData { get; private set; }
    public UnitFieldData UnitFieldData { get; private set; }
}

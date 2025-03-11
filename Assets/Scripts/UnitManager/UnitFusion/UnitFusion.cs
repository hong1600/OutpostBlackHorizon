using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class UnitFusion : MonoBehaviour
{
    UnitData unitData;
    UnitSpawner unitSpawner;

    private void Start()
    {
        unitData = Shared.unitManager.UnitData;
        unitSpawner = Shared.unitManager.UnitSpawner;
    }

    public void UnitFusionSpawn()
    {
        GameObject selectGround = InputManager.instance.FieldSelector.GetStartSelectField().
            transform.GetChild(0).gameObject;
        GameObject selectUnit = selectGround.transform.GetChild(0).gameObject;
        EUnitGrade grade = selectUnit.GetComponent<Unit>().eUnitGrade;
        GameObject spawnUnit = instantiateUnit(grade);

        for (int i = 2; i >= 0; i--)
        {
            unitData.RemoveUnitData(selectGround.transform.GetChild(i).gameObject);
        }

        unitSpawner.InstantiateUnit(spawnUnit);
    }

    public GameObject instantiateUnit(EUnitGrade _grade)
    {
        int rand = 0;
        GameObject unit;

        switch (_grade)
        {
            case EUnitGrade.C:
                rand = Random.Range(0, unitData.GetUnitByGradeList(EUnitGrade.B).Count);
                unit = unitData.GetUnitByGradeList(EUnitGrade.B)[rand];
                return unit;

            case EUnitGrade.B:
                rand = Random.Range(0, unitData.GetUnitByGradeList(EUnitGrade.A).Count);
                unit = unitData.GetUnitByGradeList(EUnitGrade.A)[rand];
                return unit;

            case EUnitGrade.A:
                rand = Random.Range(0, unitData.GetUnitByGradeList(EUnitGrade.S).Count);
                unit = unitData.GetUnitByGradeList(EUnitGrade.S)[rand];
                return unit;

            default:
                return null;
        }
    }
}

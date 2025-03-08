using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class UnitFusion : MonoBehaviour
{
    public void UnitFusionSpawn()
    {
        GameObject selectGround = InputManager.instance.FieldSelector.GetStartSelectField().
            transform.GetChild(0).gameObject;
        GameObject selectUnit = selectGround.transform.GetChild(0).gameObject;
        EUnitGrade grade = selectUnit.GetComponent<Unit>().eUnitGrade;
        GameObject spawnUnit = instantiateUnit(grade);

        for (int i = 2; i >= 0; i--)
        {
            Shared.unitManager.RemoveUnitData(selectGround.transform.GetChild(i).gameObject);
        }

        Shared.unitManager.UnitInstantiate(spawnUnit);
    }

    public GameObject instantiateUnit(EUnitGrade _grade)
    {
        int rand = 0;
        GameObject unit;

        switch (_grade)
        {
            case EUnitGrade.C:
                rand = Random.Range(0, Shared.unitManager.GetUnitByGradeList(EUnitGrade.B).Count);
                unit = Shared.unitManager.GetUnitByGradeList(EUnitGrade.B)[rand];
                return unit;

            case EUnitGrade.B:
                rand = Random.Range(0, Shared.unitManager.GetUnitByGradeList(EUnitGrade.A).Count);
                unit = Shared.unitManager.GetUnitByGradeList(EUnitGrade.A)[rand];
                return unit;

            case EUnitGrade.A:
                rand = Random.Range(0, Shared.unitManager.GetUnitByGradeList(EUnitGrade.S).Count);
                unit = Shared.unitManager.GetUnitByGradeList(EUnitGrade.S)[rand];
                return unit;

            default:
                return null;
        }
    }
}

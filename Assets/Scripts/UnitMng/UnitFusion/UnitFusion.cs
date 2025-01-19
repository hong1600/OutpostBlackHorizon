using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public interface IUnitFusion
{
    void UnitFusionSpawn();
}

public class UnitFusion : MonoBehaviour, IUnitFusion
{
    public void UnitFusionSpawn()
    {
        GameObject selectGround = Shared.inputMng.iFieldSelector.GetStartSelectField().transform.GetChild(0).gameObject;
        GameObject selectUnit = selectGround.transform.GetChild(0).gameObject;
        EUnitGrade grade = selectUnit.GetComponent<Unit>().eUnitGrade;
        GameObject spawnUnit = instantiateUnit(grade);

        for (int i = 2; i >= 0; i--)
        {
            Shared.unitMng.RemoveUnitData(selectGround.transform.GetChild(i).gameObject);
        }

        Shared.unitMng.iUnitMng.UnitInstantiate(spawnUnit);
    }

    public GameObject instantiateUnit(EUnitGrade _grade)
    {
        int rand = 0;
        GameObject unit;

        switch (_grade)
        {
            case EUnitGrade.C:
                rand = Random.Range(0, Shared.unitMng.GetUnitByGradeList(EUnitGrade.B).Count);
                unit = Shared.unitMng.GetUnitByGradeList(EUnitGrade.B)[rand];
                return unit;

            case EUnitGrade.B:
                rand = Random.Range(0, Shared.unitMng.GetUnitByGradeList(EUnitGrade.A).Count);
                unit = Shared.unitMng.GetUnitByGradeList(EUnitGrade.A)[rand];
                return unit;

            case EUnitGrade.A:
                rand = Random.Range(0, Shared.unitMng.GetUnitByGradeList(EUnitGrade.S).Count);
                unit = Shared.unitMng.GetUnitByGradeList(EUnitGrade.S)[rand];
                return unit;

            default:
                return null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitFusion
{
    IEnumerator unitFusion();
}

public class UnitFusion : MonoBehaviour, IUnitFusion
{
    public ObjectSelector objectSelector;
    public IObjectSelector iObjectSelector;
    public UnitMng unitMng;
    public IUnitMng iUnitMng;

    private void Awake()
    {
        iObjectSelector = objectSelector;
        iUnitMng = unitMng;
    }

    public IEnumerator unitFusion()
    {
        GameObject selectGround = iObjectSelector.getSelectObject().transform.GetChild(0).gameObject;
        GameObject selectUnit = selectGround.transform.GetChild(0).gameObject;
        EUnitGrade grade = selectUnit.GetComponent<Unit>().eUnitGrade;
        GameObject spawnUnit = instantiateUnit(grade);
        GameObject[] removeUnit = new GameObject[3];
        removeUnit[0] = selectGround.transform.GetChild(2).gameObject;
        removeUnit[1] = selectGround.transform.GetChild(1).gameObject;
        removeUnit[2] = selectGround.transform.GetChild(0).gameObject;

        if (selectGround.transform.childCount == 3)
        {
            iUnitMng.removeUnitData(removeUnit, selectGround, iUnitMng.getGroundNum(), 3);

            yield return new WaitForEndOfFrame();

            iUnitMng.checkGround(spawnUnit);
            iUnitMng.unitInstantiate(spawnUnit);
        }

        else yield return null;
    }

    public GameObject instantiateUnit(EUnitGrade grade)
    {
        int rand = 0;
        int groundNum = iUnitMng.getGroundNum();
        GameObject unit;

        switch (grade)
        {
            case EUnitGrade.C:
                rand = Random.Range(0, iUnitMng.getUnitByGradeList(EUnitGrade.B).Count);
                unit = iUnitMng.getUnitByGradeList(EUnitGrade.B)[rand];
                return unit;

            case EUnitGrade.B:
                rand = Random.Range(0, iUnitMng.getUnitByGradeList(EUnitGrade.A).Count);
                unit = iUnitMng.getUnitByGradeList(EUnitGrade.A)[rand];
                return unit;

            case EUnitGrade.A:
                rand = Random.Range(0, iUnitMng.getUnitByGradeList(EUnitGrade.S).Count);
                unit = iUnitMng.getUnitByGradeList(EUnitGrade.S)[rand];
                return unit;

            default:
                return null;
        }
    }
}

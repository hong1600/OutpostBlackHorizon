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
        GameObject selectGround = iObjectSelector.getSelectObject();
        GameObject selectUnit = selectGround.transform.GetChild(0).transform.GetChild(0).gameObject;
        EUnitGrade grade = selectUnit.GetComponent<Unit>().eUnitGrade;
        GameObject spawnUnit = instantiateUnit(grade);

        if (selectGround.transform.GetChild(0).transform.childCount == 3)
        {
            destroyUnit(selectGround);

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
                rand = Random.Range(0, iUnitMng.getUnitList(EUnitGrade.B).Count);
                unit = iUnitMng.getUnitList(EUnitGrade.B)[rand];
                return unit;

            case EUnitGrade.B:
                rand = Random.Range(0, iUnitMng.getUnitList(EUnitGrade.A).Count);
                unit = iUnitMng.getUnitList(EUnitGrade.A)[rand];
                return unit;

            case EUnitGrade.A:
                rand = Random.Range(0, iUnitMng.getUnitList(EUnitGrade.S).Count);
                unit = iUnitMng.getUnitList(EUnitGrade.S)[rand];
                return unit;

            default:
                return null;
        }
    }

    public void destroyUnit(GameObject ground)
    {
        for (int i = ground.transform.GetChild(0).transform.childCount - 1; i >= 0 ; i--) 
        {
            Destroy(ground.transform.GetChild(0).transform.GetChild(i).gameObject);
        }
    }
}

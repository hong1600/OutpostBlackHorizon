using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitData : MonoBehaviour
{
    public event Action onUnitCountEvent;

    [SerializeField] List<GameObject> unitObj = new List<GameObject>();
    [SerializeField] List<GameObject> allFieldUnitList = new List<GameObject>();
    Dictionary<EUnitGrade, List<GameObject>> unitByGradeDic = new Dictionary<EUnitGrade, List<GameObject>>();
    Dictionary<int, List<GameObject>> unitByGroundDic = new Dictionary<int, List<GameObject>>();

    public Dictionary<int, List<GameObject>> getUnitByGroundDic 
    {
        get { return unitByGroundDic; }
        set { unitByGroundDic = value; }
    }

    private void Awake()
    {
        foreach (EUnitGrade grade in Enum.GetValues(typeof(EUnitGrade)))
        {
            if (!unitByGradeDic.ContainsKey(grade))
            {
                unitByGradeDic[grade] = new List<GameObject>();
            }
        }

        foreach (var unit in unitObj)
        {
            EUnitGrade unitGrade = unit.GetComponent<Unit>().eUnitGrade;
            if (unitByGradeDic.ContainsKey(unitGrade))
            {
                unitByGradeDic[unitGrade].Add(unit);
            }
        }
    }

    public void AddUnitData(GameObject _unit)
    {
        allFieldUnitList.Add(_unit);

        unitByGroundDic[Shared.unitManager.UnitFieldData.fieldNum].Add(_unit);

        onUnitCountEvent?.Invoke();
    }

    public void RemoveUnitData(GameObject _unit)
    {
        Transform groundTrs = _unit.transform.parent.parent;
        Shared.unitManager.UnitFieldData.fieldNum = Shared.unitManager.UnitFieldData.GetSelectGroundNum(groundTrs);

        allFieldUnitList.Remove(_unit);
        Destroy(_unit);

        unitByGroundDic[Shared.unitManager.UnitFieldData.fieldNum].Clear();

        onUnitCountEvent?.Invoke();
    }

    public List<GameObject> GetUnitByGradeList(EUnitGrade _unitType)
    {
        if (unitByGradeDic.ContainsKey(_unitType))
        {
            return unitByGradeDic[_unitType];
        }
        return null;
    }
    public List<GameObject> GetAllUnitList() { return allFieldUnitList; }

}

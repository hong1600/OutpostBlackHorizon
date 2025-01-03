using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnitDataLoader
{
    public List<UnitData> units = new List<UnitData>();

    public Dictionary<EUnitGrade, List<UnitData>> unitGradeDataDic = new Dictionary<EUnitGrade, List<UnitData>>();

    public UnitDataLoader() 
    {
        units = DataMng.instance.unitDataList;
        UnitGradeData();
    }

    public void UnitGradeData()
    {
        unitGradeDataDic.Clear();

        foreach(var unitData in units) 
        {
            if (!unitGradeDataDic.ContainsKey(unitData.unitGrade))
            {
                unitGradeDataDic[unitData.unitGrade] = new List<UnitData>();
            }
            unitGradeDataDic[unitData.unitGrade].Add(unitData);
        }
    }

    public void LoadUnitState(PlayerData _playerdata)
    {
        for (int i = 0; i < _playerdata.unitStateList.Count; i++)
        {
            UnitState saveUnit = _playerdata.unitStateList[i]; 
            UnitData baseUnit = units.Find(u => u.index == saveUnit.index);

            if (baseUnit != null)
            {
                UnitState loadUnit = _playerdata.unitStateList[i];
                loadUnit.index = saveUnit.index;
                loadUnit.unitName = saveUnit.unitName;
                loadUnit.unitDamage = saveUnit.unitDamage;
                loadUnit.attackSpeed = saveUnit.attackSpeed;
                loadUnit.attackRange = saveUnit.attackRange;
                loadUnit.unitLevel = saveUnit.unitLevel;
                loadUnit.unitCurExp = saveUnit.unitCurExp;
                loadUnit.unitMaxExp = saveUnit.unitMaxExp;
                loadUnit.unitUpgradeCost = saveUnit.unitUpgradeCost;
                loadUnit.unitStoreCost = saveUnit.unitStoreCost;
                loadUnit.skill1Name = saveUnit.skill1Name;
                loadUnit.unitGrade = saveUnit.unitGrade;
                loadUnit.unitImg = Resources.Load<Sprite>("Units/" + saveUnit.index.ToString());
                _playerdata.unitStateList[i] = loadUnit;
            }
        }
    }

    public void InitializeUnits(PlayerData _playerData)
    {
        if (_playerData.unitStateList == null)
        {
            _playerData.unitStateList = new List<UnitState>();
        }

        foreach (var unitData in DataMng.instance.unitDataList)
        {
            UnitState unitState = new UnitState(unitData);
            _playerData.unitStateList.Add(unitState);
        }
    }

    public List<UnitData> GetUnitData(EUnitGrade _grade) 
    {
        if (unitGradeDataDic.ContainsKey(_grade))
        {
            return unitGradeDataDic[_grade];
        }
        return null;
    }
}

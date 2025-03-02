using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class UnitDataLoader : MonoBehaviour
{
    Table_Unit unitTable = new Table_Unit();
    public UnitDataBase unitDataBase;

    public List<UnitData> unitByGradeDataList = new List<UnitData>();
    public Dictionary<EUnitGrade, List<UnitData>> unitGradeDataDic = new Dictionary<EUnitGrade, List<UnitData>>();


    private void Awake()
    {
        unitTable.Init_Binary("UnitData");

        unitDataBase = ScriptableObject.CreateInstance<UnitDataBase>();

        foreach (var unitInfo in unitTable.Dictionary.Values)
        {
            UnitData unitData = ScriptableObject.CreateInstance<UnitData>();

            unitData.ID = unitInfo.ID;
            unitData.unitName = unitInfo.Name;
            unitData.unitGrade = unitInfo.Grade;
            unitData.unitDamage = unitInfo.Damage;
            unitData.unitAttackSpeed = unitInfo.AttackSpeed;
            unitData.unitAttackRange = unitInfo.AttackRange;
            unitData.unitImgPath = unitInfo.ImgPath;
            unitData.unitSkillName1 = unitInfo.Skill1Name;
            unitData.unitSkillName2 = unitInfo.Skill2Name;
            unitData.unitLevel = unitInfo.Level;
            unitData.unitCurExp = unitInfo.CurExp;
            unitData.unitMaxExp = unitInfo.MaxExp;
            unitData.unitUpgradeCost = unitInfo.UpgrdaeCost;
            unitData.unitStoreCost = unitInfo.StoreCost;
            unitData.unitMixNeedUnit = unitInfo.MixNeedUnit;
            unitData.unitDesc = unitInfo.Desc;

            if (!string.IsNullOrEmpty(unitInfo.MixNeedUnit))
            {
                ConvertStringToArray(unitData, unitInfo);
            }

            unitDataBase.unitList.Add(unitData);
            unitByGradeDataList.Add(unitData);
        }
    }

    public void UnitGradeData()
    {
        unitGradeDataDic.Clear();

        foreach (var unitData in unitByGradeDataList)
        {
            if (!unitGradeDataDic.ContainsKey(unitData.unitGrade))
            {
                unitGradeDataDic[unitData.unitGrade] = new List<UnitData>();
            }
            unitGradeDataDic[unitData.unitGrade].Add(unitData);
        }
    }

    public List<UnitData> GetUnitByGradeData(EUnitGrade _grade)
    {
        if (unitGradeDataDic.ContainsKey(_grade))
        {
            return unitGradeDataDic[_grade];
        }
        return null;
    }

    public void ConvertStringToArray(UnitData _unitData, Table_Unit.Info _info)
    {
        string[] unitNames = _unitData.unitMixNeedUnit.Split(',');
        _unitData.unitMixNeedUnits = new UnitData[unitNames.Length];

        for (int i = 0; i < unitNames.Length; i++)
        {
            string unitName = unitNames[i].Trim();

            UnitData mixUnitData = unitDataBase.GetUnitName(unitName);

            if (mixUnitData != null)
            {
                _unitData.unitMixNeedUnits[i] = mixUnitData;
            }
        }
    }
}

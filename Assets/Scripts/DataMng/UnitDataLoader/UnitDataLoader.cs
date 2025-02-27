using System.Collections;
using System.Collections.Generic;
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
}

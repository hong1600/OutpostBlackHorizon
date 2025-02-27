using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "UnitData", menuName = "Scriptble Object/UnitData")]
public class UnitData : ScriptableObject
{
    public int ID;
    public string unitName;
    public EUnitGrade unitGrade;
    public int unitDamage;
    public int unitAttackSpeed;
    public float unitAttackRange;
    public string unitImgPath;
    public string unitSkillName1;
    public string unitSkillName2;
    public float unitLevel;
    public float unitCurExp;
    public float unitMaxExp;
    public float unitUpgradeCost;
    public float unitStoreCost;
    public string unitMixNeedUnit;
    public string unitDesc;
    public UnitData[] unitMixNeedUnits;
}

public class UnitDataBase : ScriptableObject
{
    public List<UnitData> unitList = new List<UnitData>();

    public UnitData GetUnitID(int _id)
    {
        return unitList.Find(unit => unit.ID == _id);
    }
}
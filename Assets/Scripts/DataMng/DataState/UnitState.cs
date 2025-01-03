using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitState
{
    public int index;
    public string unitName;
    public int unitDamage;
    public int attackSpeed;
    public float attackRange;
    public float unitLevel;
    public string skill1Name;
    public float unitUpgradeCost;
    public float unitStoreCost;
    public float unitCurExp;
    public float unitMaxExp;
    public EUnitGrade unitGrade;
    public Sprite unitImg;

    public UnitState(UnitData _unitData)
    {
        index = _unitData.index;
        unitName = _unitData.unitName;
        unitDamage = _unitData.unitDamage;
        attackSpeed = _unitData.attackSpeed;
        attackRange = _unitData.attackRange;
        unitLevel = _unitData.unitLevel;
        skill1Name = _unitData.skill1Name;
        unitUpgradeCost = _unitData.unitUpgradeCost;
        unitStoreCost = _unitData.unitStoreCost;
        unitCurExp = _unitData.unitCurExp;
        unitMaxExp = _unitData.unitMaxExp;
        unitGrade = _unitData.unitGrade;
    }
}

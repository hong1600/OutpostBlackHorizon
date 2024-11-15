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
    public float unitGrade;
    public Sprite unitImg;

    public UnitState(UnitData unitData)
    {
        index = unitData.index;
        unitName = unitData.unitName;
        unitDamage = unitData.unitDamage;
        attackSpeed = unitData.attackSpeed;
        attackRange = unitData.attackRange;
        unitLevel = unitData.unitLevel;
        skill1Name = unitData.skill1Name;
        unitUpgradeCost = unitData.unitUpgradeCost;
        unitStoreCost = unitData.unitStoreCost;
        unitCurExp = unitData.unitCurExp;
        unitMaxExp = unitData.unitMaxExp;
        unitGrade = unitData.unitGrade;
    }
}

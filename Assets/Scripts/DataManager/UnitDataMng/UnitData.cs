using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EUnitGrade
{
    C,
    B,
    A,
    S,
    SS
}

[CreateAssetMenu(fileName = "Unit", menuName = "Scriptble Object/UnitData")]

public class UnitData : ScriptableObject
{
    public EUnitGrade unitGrade;
    public int index;
    public string unitName;
    public int unitDamage;
    public int attackSpeed;
    public float attackRange;
    public Sprite unitImg;
    public float unitLevel;
    public string skill1Name;
    public float unitUpgradeCost;
    public float unitStoreCost;
    public float unitCurExp;
    public float unitMaxExp;
    public UnitData[] mixUnit;
}

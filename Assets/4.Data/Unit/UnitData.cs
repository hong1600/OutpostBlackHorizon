using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitGrade { a, b ,c}

[CreateAssetMenu(fileName = "Unit", menuName = "Scriptble Object/UnitData")]
public class UnitData : ScriptableObject
{
    public string unitName;
    public int unitDamage;
    public int attackSpeed;
    public float attackRange;
    public UnitGrade unitGrade;
}

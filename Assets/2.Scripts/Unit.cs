using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] UnitData unitData;

    string unitName;
    float unitDamage;
    int attackSpeed;
    float attackRange;
    UnitGrade unitGrade;

    private void Start()
    {
        unitName = unitData.unitName;
        unitDamage = unitData.unitDamage;
        attackSpeed = unitData.attackSpeed;
        attackRange = unitData.attackRange;
        unitGrade = unitData.unitGrade;
    }
}

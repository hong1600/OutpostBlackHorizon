using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Unit", menuName = "Scriptble Object/UnitData")]
public class UnitData : ScriptableObject
{
    public int unitId;
    public float unitDamage;
    public int attackSpeed;
    public float attackRange;
}

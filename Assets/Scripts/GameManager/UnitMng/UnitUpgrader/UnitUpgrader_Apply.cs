using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UnitUpgrader : MonoBehaviour
{
    public void unitUpgradeApply(EUnitGrade grade)
    {
        foreach (var unit in iUnitMng.getCurUnitList())
        {
            if (unit.eUnitGrade == grade)
            {
                upgrade(grade, unit);
            }
        }
    }

    public void upgrade(EUnitGrade grade, Unit unit)
    {
        int curUpgradeLevel = 0;

        switch (grade)
        {
            case EUnitGrade.C: curUpgradeLevel = upgradeLevel[0]; break;
            case EUnitGrade.B: curUpgradeLevel = upgradeLevel[0]; break;
            case EUnitGrade.S: curUpgradeLevel = upgradeLevel[1]; break;
            case EUnitGrade.SS: curUpgradeLevel = upgradeLevel[2]; break;
        }

        switch (curUpgradeLevel)
        {
            case 2: if (upgrade2 == false) { unit.attackDamage *= 2; upgrade2 = true; } break;
            case 3: if (upgrade3 == false) { unit.attackDamage *= 2; upgrade3 = true; } break;
            case 4: if (upgrade4 == false) { unit.attackDamage *= 2; upgrade4 = true; } break;
            case 5: if (upgrade5 == false) { unit.attackDamage *= 2; upgrade5 = true; } break;
            case 6: if (upgrade6 == false) { unit.attackDamage *= 2; upgrade6 = true; } break;
        }
    }

}

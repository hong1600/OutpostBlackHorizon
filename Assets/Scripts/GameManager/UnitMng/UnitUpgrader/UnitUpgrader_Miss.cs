using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UnitUpgrader : MonoBehaviour
{
    public void missUpgrade(int lastUpgrade, Unit unit)
    {
        for (int i = 1; i <= lastUpgrade; i++)
        {
            switch (i)
            {
                case 2: if (upgrade2 == false) { unit.attackDamage *= 2; upgrade2 = true; } break;
                case 3: if (upgrade3 == false) { unit.attackDamage *= 2; upgrade3 = true; } break;
                case 4: if (upgrade4 == false) { unit.attackDamage *= 2; upgrade4 = true; } break;
                case 5: if (upgrade5 == false) { unit.attackDamage *= 2; upgrade5 = true; } break;
                case 6: if (upgrade6 == false) { unit.attackDamage *= 2; upgrade6 = true; } break;
            }
        }
    }
}

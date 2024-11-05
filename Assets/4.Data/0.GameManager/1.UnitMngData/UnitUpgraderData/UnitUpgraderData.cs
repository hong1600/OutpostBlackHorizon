using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitUpgraderData", menuName = "UnitManager/UnitUpgraderDataData", order = 4)]
public class UnitUpgraderData : ScriptableObject
{
    public int upgradeCost1;
    public int upgradeCost2;
    public int upgradeCost3;
    public int upgradeCost4;
    public int upgradeLevel1;
    public int upgradeLevel2;
    public int upgradeLevel3;
    public int upgradeLevel4;
}

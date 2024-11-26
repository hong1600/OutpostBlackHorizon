using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUpgradeHandlerBtn : MonoBehaviour
{
    public UnitUpgrader unitUpgrader;
    public IUnitUpgrader iUnitUpgrader;

    private void Awake()
    {
        iUnitUpgrader = unitUpgrader;
    }

    public void clickUpgradeBtn(int index)
    {
        iUnitUpgrader.unitUpgrade(index);
    }
}

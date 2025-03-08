using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUpgradeHandlerBtn : MonoBehaviour
{
    public void ClickUpgradeBtn(int _index)
    {
        Shared.unitManager.UnitUpgrader.UnitUpgrade(_index);
    }
}

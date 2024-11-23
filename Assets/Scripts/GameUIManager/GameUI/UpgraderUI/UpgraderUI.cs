using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgraderUI : MonoBehaviour
{
    public UnitUpgrader unitUpgrader;
    public UnitSpawner unitSpawner;

    public IGoldCoin iGoldCoin;

    public TextMeshProUGUI upgradeGoldText;
    public TextMeshProUGUI upgradeCoinText;
    public TextMeshProUGUI upgradeCost0Text;
    public TextMeshProUGUI upgradeCost1Text;
    public TextMeshProUGUI upgradeCost2Text;
    public TextMeshProUGUI upgradeCost3Text;
    public TextMeshProUGUI upgradeLevel0Text;
    public TextMeshProUGUI upgradeLevel1Text;
    public TextMeshProUGUI upgradeLevel2Text;
    public TextMeshProUGUI upgradeLevel3Text;
    public TextMeshProUGUI spawnPerText0;
    public TextMeshProUGUI spawnPerText1;
    public TextMeshProUGUI spawnPerText2;
    public TextMeshProUGUI spawnPerText3;

    private void upgradePanel()
    {
        upgradeGoldText.text = iGoldCoin.getGold().ToString();
        upgradeCoinText.text = iGoldCoin.getCoin().ToString();
        upgradeCost0Text.text = unitUpgrader.upgradeCost0.ToString();
        upgradeCost1Text.text = unitUpgrader.upgradeCost1.ToString();
        upgradeCost2Text.text = unitUpgrader.upgradeCost2.ToString();
        upgradeCost3Text.text = unitUpgrader.upgradeCost3.ToString();
        if (unitUpgrader.upgradeLevel0 < 6)
        {
            upgradeLevel0Text.text = "LV." + unitUpgrader.upgradeLevel0.ToString();
        }
        else
        {
            upgradeLevel0Text.text = "LV.MAX";
        }
        if (unitUpgrader.upgradeLevel1 < 6)
        {
            upgradeLevel1Text.text = "LV." + unitUpgrader.upgradeLevel1.ToString();
        }
        else
        {
            upgradeLevel1Text.text = "LV.MAX";
        }
        if (unitUpgrader.upgradeLevel2 < 6)
        {
            upgradeLevel2Text.text = "LV." + unitUpgrader.upgradeLevel2.ToString();
        }
        else
        {
            upgradeLevel2Text.text = "LV.MAX";
        }
        if (unitUpgrader.upgradeLevel3 < 6)
        {
            upgradeLevel3Text.text = "LV." + unitUpgrader.upgradeLevel3.ToString();
        }
        else
        {
            upgradeLevel3Text.text = "LV.MAX";
        }
        spawnPerText0.text =
            $"ÀÏ¹Ý : {unitSpawner.selectWeight[(int)unitUpgrader.upgradeLevel3 - 1][3]}%";
        spawnPerText1.text =
            $"<color=blue>Èñ±Í : {unitSpawner.selectWeight[(int)unitUpgrader.upgradeLevel3 - 1][2]}%</color>%";
        spawnPerText2.text =
            $"<color=purple>¿µ¿õ : {unitSpawner.selectWeight[(int)unitUpgrader.upgradeLevel3 - 1][1]}%</color>";
        spawnPerText3.text =
            $"<color=yellow>Àü¼³ : {unitSpawner.selectWeight[(int)unitUpgrader.upgradeLevel3 - 1][0]}%</color>";
    }
}

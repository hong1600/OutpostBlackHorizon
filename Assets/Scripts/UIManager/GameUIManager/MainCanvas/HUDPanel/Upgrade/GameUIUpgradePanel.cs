using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUIUpgradePanel : MonoBehaviour
{
    public UnitUpgrader unitUpgrader;
    public IUnitUpgrader iUnitUpgrader;
    public GoldCoin goldCoin;
    public IGoldCoin iGoldCoin;
    public UnitSpawner unitSpawner;
    public IUnitSpawner iUnitSpawner;

    public TextMeshProUGUI upgradeGoldText;
    public TextMeshProUGUI upgradeCoinText;
    public TextMeshProUGUI[] upgradeCostText;
    public TextMeshProUGUI[] upgradeLevelText;
    public TextMeshProUGUI[] spawnPerText;

    private void Awake()
    {
        iUnitUpgrader = unitUpgrader;
        iGoldCoin = goldCoin;
        iUnitSpawner = unitSpawner;
    }

    private void upgradePanel()
    {
        upgradeGoldText.text = iGoldCoin.getGold().ToString();
        upgradeCoinText.text = iGoldCoin.getCoin().ToString();
        upgradeCostText[0].text = unitUpgrader.upgradeCost[0].ToString();
        upgradeCostText[1].text = unitUpgrader.upgradeCost[1].ToString();
        upgradeCostText[2].text = unitUpgrader.upgradeCost[2].ToString();
        upgradeCostText[3].text = unitUpgrader.upgradeCost[3].ToString();
        if (iUnitUpgrader.getUpgradeLevel()[0] < iUnitUpgrader.getUpgradeMaxLevel())
        {
            upgradeLevelText[0].text = "LV." + iUnitUpgrader.getUpgradeLevel()[0].ToString();
        }
        else
        {
            upgradeLevelText[0].text = "LV.MAX";
        }
        if (iUnitUpgrader.getUpgradeLevel()[1] < iUnitUpgrader.getUpgradeMaxLevel())
        {
            upgradeLevelText[1].text = "LV." + iUnitUpgrader.getUpgradeLevel()[1].ToString();
        }
        else
        {
            upgradeLevelText[1].text = "LV.MAX";
        }
        if (iUnitUpgrader.getUpgradeLevel()[2] < iUnitUpgrader.getUpgradeMaxLevel())
        {
            upgradeLevelText[2].text = "LV." + iUnitUpgrader.getUpgradeLevel()[2].ToString();
        }
        else
        {
            upgradeLevelText[2].text = "LV.MAX";
        }
        if (iUnitUpgrader.getUpgradeLevel()[3] < iUnitUpgrader.getUpgradeMaxLevel())
        {
            upgradeLevelText[3].text = "LV." + iUnitUpgrader.getUpgradeLevel()[3].ToString();
        }
        else
        {
            upgradeLevelText[3].text = "LV.MAX";
        }
        spawnPerText[0].text =
            $"ÀÏ¹Ý : {unitSpawner.selectWeight[(int)iUnitUpgrader.getUpgradeLevel()[3] - 1][3]}%";
        spawnPerText[1].text =
            $"<color=blue>Èñ±Í : {unitSpawner.selectWeight[(int)iUnitUpgrader.getUpgradeLevel()[3] - 1][2]}%</color>%";
        spawnPerText[2].text =
            $"<color=purple>¿µ¿õ : {unitSpawner.selectWeight[(int)iUnitUpgrader.getUpgradeLevel()[3] - 1][1]}%</color>";
        spawnPerText[3].text =
            $"<color=yellow>Àü¼³ : {unitSpawner.selectWeight[(int)unitUpgrader.upgradeCost[2] - 1][0]}%</color>";
    }

}

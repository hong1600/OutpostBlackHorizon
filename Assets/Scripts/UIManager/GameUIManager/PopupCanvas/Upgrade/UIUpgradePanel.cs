using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIUpgradePanel : MonoBehaviour
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

        iGoldCoin.onGoldChanged += GoldCoinPanel;
        iGoldCoin.onCoinChanged += GoldCoinPanel;
        iUnitUpgrader.onUpgradeCostChange += upgradeCostPanel;
        iUnitUpgrader.onUpgradeLevelChange += upgradeLevelPanel;
        iUnitUpgrader.onUpgradePerChange += upgradePerPanel;
    }

    private void Start()
    {
        GoldCoinPanel();
        upgradeCostPanel();
        upgradeLevelPanel();
        upgradePerPanel();
    }

    private void GoldCoinPanel()
    {
        upgradeGoldText.text = iGoldCoin.getGold().ToString();
        upgradeCoinText.text = iGoldCoin.getCoin().ToString();
    }

    public void upgradeCostPanel()
    {
        for (int i = 0; i < upgradeCostText.Length; i++) 
        {
            upgradeCostText[i].text = iUnitUpgrader.getUpgradeCost()[i].ToString();
        }
    }

    public void upgradeLevelPanel()
    {
        for (int i = 0; i < upgradeLevelText.Length; i++)
        {
            if (iUnitUpgrader.getUpgradeLevel()[i] < iUnitUpgrader.getUpgradeMaxLevel())
            {
                upgradeLevelText[i].text = $"LV.{iUnitUpgrader.getUpgradeLevel()[i]}";
            }
            else if (iUnitUpgrader.getUpgradeLevel()[i] >= iUnitUpgrader.getUpgradeMaxLevel())
            {
                upgradeLevelText[i].text = "LV.MAX";
            }
        }
    }

    public void upgradePerPanel()
    {
        spawnPerText[0].text =
            $"ÀÏ¹Ý : {iUnitSpawner.getSelectWeight()[(int)iUnitUpgrader.getUpgradeLevel()[3] - 1][3]}%";
        spawnPerText[1].text =
            $"<color=blue>Èñ±Í : {iUnitSpawner.getSelectWeight()[(int)iUnitUpgrader.getUpgradeLevel()[3] - 1][2]}%</color>%";
        spawnPerText[2].text =
            $"<color=purple>¿µ¿õ : {iUnitSpawner.getSelectWeight()[(int)iUnitUpgrader.getUpgradeLevel()[3] - 1][1]}%</color>";
        spawnPerText[3].text =
            $"<color=yellow>Àü¼³ : {iUnitSpawner.getSelectWeight()[(int)iUnitUpgrader.getUpgradeCost()[2] - 1][0]}%</color>";
    }
}

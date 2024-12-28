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
            if (iUnitUpgrader.getUpgradeLevel()[i] == 6)
            {
                upgradeCostText[i].text = "MAX";
            }
            else
            {
                upgradeCostText[i].text = iUnitUpgrader.getUpgradeCost()[i].ToString();
            }
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
        int level = (int)iUnitUpgrader.getUpgradeLevel()[(int)EUnitUpgrdae.per] - 1;

        spawnPerText[0].text =
            $"ÀÏ¹Ý : {iUnitSpawner.getSelectWeight()[level][0]}%";
        spawnPerText[1].text =
            $"<color=blue>Èñ±Í : " +
            $"{iUnitSpawner.getSelectWeight()[level][1]}%</color>%";
        spawnPerText[2].text =
            $"<color=purple>¿µ¿õ : " +
            $"{iUnitSpawner.getSelectWeight()[level][2]}%</color>";
        spawnPerText[3].text =
            $"<color=yellow>Àü¼³ : " +
            $"{iUnitSpawner.getSelectWeight()[level][3]}%</color>";
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIUpgradePanel : MonoBehaviour
{
    UnitUpgrader unitUpgrader;
    UnitSpawner unitSpawner;
    GoldCoin goldCoin;

    [SerializeField] TextMeshProUGUI upgradeGoldText;
    [SerializeField] TextMeshProUGUI upgradeCoinText;
    [SerializeField] TextMeshProUGUI[] upgradeCostTexts;
    [SerializeField] TextMeshProUGUI[] upgradeLevelTexts;
    [SerializeField] TextMeshProUGUI[] spawnPerTexts;

    private void Start()
    {
        unitUpgrader = UnitManager.instance.UnitUpgrader;
        unitSpawner = UnitManager.instance.UnitSpawner;
        goldCoin = GameManager.instance.GoldCoin;

        goldCoin.onGoldChanged += GoldCoinPanel;
        goldCoin.onCoinChanged += GoldCoinPanel;
        unitUpgrader.onUpgradeCostChange += UpgradeCostPanel;
        unitUpgrader.onUpgradeLevelChange += UpgradeLevelPanel;
        unitUpgrader.onUpgradePerChange += UpgradePerPanel;

        GoldCoinPanel();
        UpgradeCostPanel();
        UpgradeLevelPanel();
        UpgradePerPanel();
    }

    private void GoldCoinPanel()
    {
        upgradeGoldText.text = goldCoin.GetGold().ToString();
        upgradeCoinText.text = goldCoin.GetCoin().ToString();
    }

    private void UpgradeCostPanel()
    {
        for (int i = 0; i < upgradeCostTexts.Length; i++) 
        {
            if (unitUpgrader.GetUpgradeLevel()[i] == 6)
            {
                upgradeCostTexts[i].text = "MAX";
            }
            else
            {
                upgradeCostTexts[i].text = unitUpgrader.GetUpgradeCost()[i].ToString();
            }
        }
    }

    private void UpgradeLevelPanel()
    {
        for (int i = 0; i < upgradeLevelTexts.Length; i++)
        {
            if (unitUpgrader.GetUpgradeLevel()[i] < unitUpgrader.GetUpgradeMaxLevel())
            {
                upgradeLevelTexts[i].text = $"LV.{unitUpgrader.GetUpgradeLevel()[i]}";
            }
            else if (unitUpgrader.GetUpgradeLevel()[i] >= unitUpgrader.GetUpgradeMaxLevel())
            {
                upgradeLevelTexts[i].text = "LV.MAX";
            }
        }
    }

    private void UpgradePerPanel()
    {
        int level = (int)unitUpgrader.GetUpgradeLevel()[(int)EUnitUpgrade.per] - 1;

        spawnPerTexts[0].text =
            $"ÀÏ¹Ý : {unitSpawner.GetSelectWeight()[level][0]}%";
        spawnPerTexts[1].text =
            $"Èñ±Í : " +
            $"{unitSpawner.GetSelectWeight()[level][1]}%";
        spawnPerTexts[2].text =
            $"¿µ¿õ : " +
            $"{unitSpawner.GetSelectWeight()[level][2]}%";
        spawnPerTexts[3].text =
            $"Àü¼³ : " +
            $"{unitSpawner.GetSelectWeight()[level][3]}%";
    }
}

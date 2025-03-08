using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIUpgradePanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI upgradeGoldText;
    [SerializeField] TextMeshProUGUI upgradeCoinText;
    [SerializeField] TextMeshProUGUI[] upgradeCostTexts;
    [SerializeField] TextMeshProUGUI[] upgradeLevelTexts;
    [SerializeField] TextMeshProUGUI[] spawnPerTexts;

    private void OnEnable()
    {
        Shared.gameManager.GoldCoin.onGoldChanged += GoldCoinPanel;
        Shared.gameManager.GoldCoin.onCoinChanged += GoldCoinPanel;
        Shared.unitManager.UnitUpgrader.onUpgradeCostChange += UpgradeCostPanel;
        Shared.unitManager.UnitUpgrader.onUpgradeLevelChange += UpgradeLevelPanel;
        Shared.unitManager.UnitUpgrader.onUpgradePerChange += UpgradePerPanel;
    }

    private void OnDisable()
    {
        Shared.gameManager.GoldCoin.onGoldChanged -= GoldCoinPanel;
        Shared.gameManager.GoldCoin.onCoinChanged -= GoldCoinPanel;
        Shared.unitManager.UnitUpgrader.onUpgradeCostChange -= UpgradeCostPanel;
        Shared.unitManager.UnitUpgrader.onUpgradeLevelChange -= UpgradeLevelPanel;
        Shared.unitManager.UnitUpgrader.onUpgradePerChange -= UpgradePerPanel;
    }

    private void Start()
    {
        GoldCoinPanel();
        UpgradeCostPanel();
        UpgradeLevelPanel();
        UpgradePerPanel();
    }

    private void GoldCoinPanel()
    {
        upgradeGoldText.text = Shared.gameManager.GoldCoin.GetGold().ToString();
        upgradeCoinText.text = Shared.gameManager.GoldCoin.GetCoin().ToString();
    }

    private void UpgradeCostPanel()
    {
        for (int i = 0; i < upgradeCostTexts.Length; i++) 
        {
            if (Shared.unitManager.UnitUpgrader.GetUpgradeLevel()[i] == 6)
            {
                upgradeCostTexts[i].text = "MAX";
            }
            else
            {
                upgradeCostTexts[i].text = Shared.unitManager.UnitUpgrader.GetUpgradeCost()[i].ToString();
            }
        }
    }

    private void UpgradeLevelPanel()
    {
        for (int i = 0; i < upgradeLevelTexts.Length; i++)
        {
            if (Shared.unitManager.UnitUpgrader.GetUpgradeLevel()[i] < Shared.unitManager.UnitUpgrader.GetUpgradeMaxLevel())
            {
                upgradeLevelTexts[i].text = $"LV.{Shared.unitManager.UnitUpgrader.GetUpgradeLevel()[i]}";
            }
            else if (Shared.unitManager.UnitUpgrader.GetUpgradeLevel()[i] >= Shared.unitManager.UnitUpgrader.GetUpgradeMaxLevel())
            {
                upgradeLevelTexts[i].text = "LV.MAX";
            }
        }
    }

    private void UpgradePerPanel()
    {
        int level = (int)Shared.unitManager.UnitUpgrader.GetUpgradeLevel()[(int)EUnitUpgrade.per] - 1;

        spawnPerTexts[0].text =
            $"ÀÏ¹Ý : {Shared.unitManager.UnitSpawner.GetSelectWeight()[level][0]}%";
        spawnPerTexts[1].text =
            $"<color=blue>Èñ±Í : " +
            $"{Shared.unitManager.UnitSpawner.GetSelectWeight()[level][1]}%</color>%";
        spawnPerTexts[2].text =
            $"<color=purple>¿µ¿õ : " +
            $"{Shared.unitManager.UnitSpawner.GetSelectWeight()[level][2]}%</color>";
        spawnPerTexts[3].text =
            $"<color=yellow>Àü¼³ : " +
            $"{Shared.unitManager.UnitSpawner.GetSelectWeight()[level][3]}%</color>";
    }
}

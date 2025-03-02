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
        Shared.gameMng.GoldCoin.onGoldChanged += GoldCoinPanel;
        Shared.gameMng.GoldCoin.onCoinChanged += GoldCoinPanel;
        Shared.unitMng.UnitUpgrader.onUpgradeCostChange += UpgradeCostPanel;
        Shared.unitMng.UnitUpgrader.onUpgradeLevelChange += UpgradeLevelPanel;
        Shared.unitMng.UnitUpgrader.onUpgradePerChange += UpgradePerPanel;
    }

    private void OnDisable()
    {
        Shared.gameMng.GoldCoin.onGoldChanged -= GoldCoinPanel;
        Shared.gameMng.GoldCoin.onCoinChanged -= GoldCoinPanel;
        Shared.unitMng.UnitUpgrader.onUpgradeCostChange -= UpgradeCostPanel;
        Shared.unitMng.UnitUpgrader.onUpgradeLevelChange -= UpgradeLevelPanel;
        Shared.unitMng.UnitUpgrader.onUpgradePerChange -= UpgradePerPanel;
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
        upgradeGoldText.text = Shared.gameMng.GoldCoin.GetGold().ToString();
        upgradeCoinText.text = Shared.gameMng.GoldCoin.GetCoin().ToString();
    }

    private void UpgradeCostPanel()
    {
        for (int i = 0; i < upgradeCostTexts.Length; i++) 
        {
            if (Shared.unitMng.UnitUpgrader.GetUpgradeLevel()[i] == 6)
            {
                upgradeCostTexts[i].text = "MAX";
            }
            else
            {
                upgradeCostTexts[i].text = Shared.unitMng.UnitUpgrader.GetUpgradeCost()[i].ToString();
            }
        }
    }

    private void UpgradeLevelPanel()
    {
        for (int i = 0; i < upgradeLevelTexts.Length; i++)
        {
            if (Shared.unitMng.UnitUpgrader.GetUpgradeLevel()[i] < Shared.unitMng.UnitUpgrader.GetUpgradeMaxLevel())
            {
                upgradeLevelTexts[i].text = $"LV.{Shared.unitMng.UnitUpgrader.GetUpgradeLevel()[i]}";
            }
            else if (Shared.unitMng.UnitUpgrader.GetUpgradeLevel()[i] >= Shared.unitMng.UnitUpgrader.GetUpgradeMaxLevel())
            {
                upgradeLevelTexts[i].text = "LV.MAX";
            }
        }
    }

    private void UpgradePerPanel()
    {
        int level = (int)Shared.unitMng.UnitUpgrader.GetUpgradeLevel()[(int)EUnitUpgrade.per] - 1;

        spawnPerTexts[0].text =
            $"ÀÏ¹Ý : {Shared.unitMng.UnitSpawner.GetSelectWeight()[level][0]}%";
        spawnPerTexts[1].text =
            $"<color=blue>Èñ±Í : " +
            $"{Shared.unitMng.UnitSpawner.GetSelectWeight()[level][1]}%</color>%";
        spawnPerTexts[2].text =
            $"<color=purple>¿µ¿õ : " +
            $"{Shared.unitMng.UnitSpawner.GetSelectWeight()[level][2]}%</color>";
        spawnPerTexts[3].text =
            $"<color=yellow>Àü¼³ : " +
            $"{Shared.unitMng.UnitSpawner.GetSelectWeight()[level][3]}%</color>";
    }
}

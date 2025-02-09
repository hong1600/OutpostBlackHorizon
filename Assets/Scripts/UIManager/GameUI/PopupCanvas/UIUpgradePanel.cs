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
        Shared.gameMng.iGoldCoin.onGoldChanged += GoldCoinPanel;
        Shared.gameMng.iGoldCoin.onCoinChanged += GoldCoinPanel;
        Shared.unitMng.iUnitUpgrader.onUpgradeCostChange += UpgradeCostPanel;
        Shared.unitMng.iUnitUpgrader.onUpgradeLevelChange += UpgradeLevelPanel;
        Shared.unitMng.iUnitUpgrader.onUpgradePerChange += UpgradePerPanel;
    }

    private void OnDisable()
    {
        Shared.gameMng.iGoldCoin.onGoldChanged -= GoldCoinPanel;
        Shared.gameMng.iGoldCoin.onCoinChanged -= GoldCoinPanel;
        Shared.unitMng.iUnitUpgrader.onUpgradeCostChange -= UpgradeCostPanel;
        Shared.unitMng.iUnitUpgrader.onUpgradeLevelChange -= UpgradeLevelPanel;
        Shared.unitMng.iUnitUpgrader.onUpgradePerChange -= UpgradePerPanel;
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
        upgradeGoldText.text = Shared.gameMng.iGoldCoin.GetGold().ToString();
        upgradeCoinText.text = Shared.gameMng.iGoldCoin.GetCoin().ToString();
    }

    private void UpgradeCostPanel()
    {
        for (int i = 0; i < upgradeCostTexts.Length; i++) 
        {
            if (Shared.unitMng.iUnitUpgrader.GetUpgradeLevel()[i] == 6)
            {
                upgradeCostTexts[i].text = "MAX";
            }
            else
            {
                upgradeCostTexts[i].text = Shared.unitMng.iUnitUpgrader.GetUpgradeCost()[i].ToString();
            }
        }
    }

    private void UpgradeLevelPanel()
    {
        for (int i = 0; i < upgradeLevelTexts.Length; i++)
        {
            if (Shared.unitMng.iUnitUpgrader.GetUpgradeLevel()[i] < Shared.unitMng.iUnitUpgrader.GetUpgradeMaxLevel())
            {
                upgradeLevelTexts[i].text = $"LV.{Shared.unitMng.iUnitUpgrader.GetUpgradeLevel()[i]}";
            }
            else if (Shared.unitMng.iUnitUpgrader.GetUpgradeLevel()[i] >= Shared.unitMng.iUnitUpgrader.GetUpgradeMaxLevel())
            {
                upgradeLevelTexts[i].text = "LV.MAX";
            }
        }
    }

    private void UpgradePerPanel()
    {
        int level = (int)Shared.unitMng.iUnitUpgrader.GetUpgradeLevel()[(int)EUnitUpgrade.per] - 1;

        spawnPerTexts[0].text =
            $"ÀÏ¹Ý : {Shared.unitMng.iUnitSpawner.GetSelectWeight()[level][0]}%";
        spawnPerTexts[1].text =
            $"<color=blue>Èñ±Í : " +
            $"{Shared.unitMng.iUnitSpawner.GetSelectWeight()[level][1]}%</color>%";
        spawnPerTexts[2].text =
            $"<color=purple>¿µ¿õ : " +
            $"{Shared.unitMng.iUnitSpawner.GetSelectWeight()[level][2]}%</color>";
        spawnPerTexts[3].text =
            $"<color=yellow>Àü¼³ : " +
            $"{Shared.unitMng.iUnitSpawner.GetSelectWeight()[level][3]}%</color>";
    }
}

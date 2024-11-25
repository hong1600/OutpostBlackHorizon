using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitUpgrader 
{
    int[] getUpgradeLevel();
    int getUpgradeCost3();
    int getUpgradeMaxLevel();
}

public class UnitUpgrader : MonoBehaviour, IUnitUpgrader
{
    public UnitMng unitMng;
    public IUnitMng iUnitMng;
    public GoldCoin goldCoin;
    public IGoldCoin iGoldCoin;

    public int[] upgradeCost;
    public int[] upgradeLevel;
    public int upgradeMaxLevel;

    private void Awake()
    {
        iUnitMng = unitMng;
        iGoldCoin = goldCoin;

        upgradeCost[0] = 30;
        upgradeCost[1] = 50;
        upgradeCost[2] = 2;
        upgradeCost[3] = 100;
        upgradeLevel[0] = 1;
        upgradeLevel[1] = 1;
        upgradeLevel[2] = 1;
        upgradeLevel[3] = 1;
        upgradeMaxLevel = 6;
    }

    public void unitUpgradeCost(ref int cost, int amount, string type = "Gold")
    {
        if (type == "Gold")
            iGoldCoin.setGold(-cost);
        else if (type == "Coin")
            iGoldCoin.setCoin(-cost);

        cost += amount;
    }

    public void unitUpgradeApply(int grade)
    {
        foreach (var unit in iUnitMng.getCurUnitList())
        {
            if (unit.unitGrade == grade)
            {
                unit.upgrade();
            }
        }
    }

    public void upgradeGrade0()
    {
        if (upgradeLevel[0] < upgradeMaxLevel)
        {
            unitUpgradeCost(ref upgradeCost[0], 30);
            upgradeLevel[0]++;
            unitUpgradeApply(0);
        }
    }

    public void upgradeGrade1()
    {
        if (upgradeLevel[1] < upgradeMaxLevel)
        {
            unitUpgradeCost(ref upgradeCost[1], 50);
            upgradeLevel[1]++;
            unitUpgradeApply(1);
        }
    }

    public void upgradeGrade2()
    {
        if (upgradeLevel[2] < upgradeMaxLevel)
        {
            unitUpgradeCost(ref upgradeCost[2], 1, "Coin");
            upgradeLevel[2]++;
            unitUpgradeApply(2);
        }
    }

    public void upgradeGrade3()
    {
        if (upgradeLevel[3] < upgradeMaxLevel)
        {
            unitUpgradeCost(ref upgradeCost[3], 100);
            upgradeLevel[3]++;
            unitUpgradeApply(3);
        }
    }

    public void unitUpgrade(int index)
    {
        switch (index) 
        {
            case 0:
                upgradeGrade0(); break;
            case 1:
                upgradeGrade1(); break;
            case 2:
                upgradeGrade2(); break;
            case 3:
                upgradeGrade3(); break;
        }
    }

    public int getUpgradeCost3() { return upgradeCost[3]; }
    public int getUpgradeMaxLevel() { return upgradeMaxLevel; }
    public int[] getUpgradeLevel() { return upgradeLevel; }
}

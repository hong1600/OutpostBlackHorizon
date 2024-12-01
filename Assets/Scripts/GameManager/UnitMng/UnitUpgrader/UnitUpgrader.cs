using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitUpgrader 
{
    void unitUpgrade(int index);
    void missUpgrade(int lastUpgrade, Unit unit);
    int[] getUpgradeLevel();
    int getUpgradeCost3();

    int getUpgradeMaxLevel();
}

public partial class UnitUpgrader : MonoBehaviour, IUnitUpgrader
{
    public UnitMng unitMng;
    public IUnitMng iUnitMng;
    public GoldCoin goldCoin;
    public IGoldCoin iGoldCoin;

    public int[] upgradeCost = new int[4];
    public int[] upgradeLevel = new int[4];
    public int upgradeMaxLevel;

    public bool upgrade2;
    public bool upgrade3;
    public bool upgrade4;
    public bool upgrade5;
    public bool upgrade6;

    private void Awake()
    {
        iUnitMng = unitMng;
        iGoldCoin = goldCoin;

        upgradeLevel[0] = 1;
        upgradeLevel[1] = 1;
        upgradeLevel[2] = 1;
        upgradeLevel[3] = 1;
        upgradeMaxLevel = 6;

        upgrade2 = false;
        upgrade3 = false;
        upgrade4 = false;
        upgrade5 = false;
        upgrade6 = false;
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

    public void upgradeGrade0()
    {
        if (upgradeLevel[0] < upgradeMaxLevel)
        {
            unitUpgradeCost(ref upgradeCost[0], 30);
            upgradeLevel[0]++;
            unitUpgradeApply(eUnitGrade.C);
        }
    }

    public void upgradeGrade1()
    {
        if (upgradeLevel[1] < upgradeMaxLevel)
        {
            unitUpgradeCost(ref upgradeCost[1], 50);
            upgradeLevel[1]++;
            unitUpgradeApply(eUnitGrade.B);
        }
    }

    public void upgradeGrade2()
    {
        if (upgradeLevel[2] < upgradeMaxLevel)
        {
            unitUpgradeCost(ref upgradeCost[2], 1, "Coin");
            upgradeLevel[2]++;
            unitUpgradeApply(eUnitGrade.A);
        }
    }

    public void upgradeGrade3()
    {
        if (upgradeLevel[3] < upgradeMaxLevel)
        {
            unitUpgradeCost(ref upgradeCost[3], 100);
            upgradeLevel[3]++;
            unitUpgradeApply(eUnitGrade.S);
        }
    }

    public int getUpgradeCost3() { return upgradeCost[3]; }

    public int getUpgradeMaxLevel() { return upgradeMaxLevel; }
    public int[] getUpgradeLevel() { return upgradeLevel; }
}

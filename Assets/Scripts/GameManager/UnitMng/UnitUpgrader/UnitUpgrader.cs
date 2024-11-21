using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitUpgrader 
{
    int getUpgradeCost3();
}

public class UnitUpgrader : MonoBehaviour, IUnitUpgrader
{
    public UnitMng unitMng;
    public IUnitMng iUnitMng;
    public GoldCoin goldCoin;
    public IGoldCoin iGoldCoin;

    public int upgradeCost0;
    public int upgradeCost1;
    public int upgradeCost2;
    public int upgradeCost3;
    public int upgradeLevel0;
    public int upgradeLevel1;
    public int upgradeLevel2;
    public int upgradeLevel3;

    private void Awake()
    {
        iUnitMng = unitMng;
        iGoldCoin = goldCoin;

        upgradeCost0 = 30;
        upgradeCost1 = 50;
        upgradeCost2 = 2;
        upgradeCost3 = 100;
        upgradeLevel0 = 1;
        upgradeLevel1 = 1;
        upgradeLevel2 = 1;
        upgradeLevel3 = 1;
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
        if (upgradeLevel0 < 6)
        {
            unitUpgradeCost(ref upgradeCost0, 30);
            upgradeLevel0++;
            unitUpgradeApply(0);
        }
    }

    public void upgradeGrade1()
    {
        if (upgradeLevel1 < 6)
        {
            unitUpgradeCost(ref upgradeCost1, 50);
            upgradeLevel1++;
            unitUpgradeApply(1);
        }
    }

    public void upgradeGrade2()
    {
        if (upgradeLevel2 < 6)
        {
            unitUpgradeCost(ref upgradeCost2, 1, "Coin");
            upgradeLevel2++;
            unitUpgradeApply(2);
        }
    }

    public void upgradeGrade3()
    {
        if (upgradeLevel3 < 6)
        {
            unitUpgradeCost(ref upgradeCost3, 100);
            upgradeLevel3++;
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

    public int getUpgradeCost3() { return upgradeCost3; }
}

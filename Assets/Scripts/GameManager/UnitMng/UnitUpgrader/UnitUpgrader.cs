using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitUpgrader 
{
    void unitUpgrade(int index);
    void missUpgrade(int lastUpgrade, Unit unit);
    int[] getUpgradeLevel();
    int[] getUpgradeCost();
    int getUpgradeMaxLevel();
    public event Action onUpgradeCostChange;
    public event Action onUpgradeLevelChange;
    public event Action onUpgradePerChange;
}

public partial class UnitUpgrader : MonoBehaviour, IUnitUpgrader
{
    public UnitMng unitMng;
    public IUnitMng iUnitMng;
    public GoldCoin goldCoin;
    public IGoldCoin iGoldCoin;

    public event Action onUpgradeCostChange;
    public event Action onUpgradeLevelChange;
    public event Action onUpgradePerChange;

    public int[] upgradeCost = new int[4];
    public int[] upgradeLevel = new int[4];
    public int upgradeMaxLevel;

    private void Awake()
    {
        iUnitMng = unitMng;
        iGoldCoin = goldCoin;

        upgradeCost[0] = 30;
        upgradeCost[1] = 50;
        upgradeCost[2] = 1;
        upgradeCost[3] = 100;

        upgradeLevel[0] = 1;
        upgradeLevel[1] = 1;
        upgradeLevel[2] = 1;
        upgradeLevel[3] = 1;

        upgradeMaxLevel = 6;
    }

    public void unitUpgrade(int index)
    {
        if (upgradeLevel[index] < upgradeMaxLevel) 
        {
            int cost = 0;
            int amount = 0;
            string type = "Gold";
            List<EUnitGrade> grades = new List<EUnitGrade>();

            switch (index)
            {
                case 0:
                    cost = upgradeCost[index];
                    amount = 30;
                    grades.Add(EUnitGrade.C);
                    grades.Add(EUnitGrade.B);
                    break;
                case 1:
                    cost = upgradeCost[index];
                    grades.Add(EUnitGrade.A);
                    amount = 50;
                    break;
                case 2:
                    cost = upgradeCost[index];
                    grades.Add(EUnitGrade.S);
                    grades.Add(EUnitGrade.SS);
                    amount = 1;
                    type = "Coin";
                    break;
                case 3:
                    cost = upgradeCost[index];
                    amount = 100;
                    break;
            }

            upgradeGrade(index, cost, amount, grades, type);
        }
    }

    public void upgradeGrade(int index, int cost, int amount, List<EUnitGrade> grades, string type = "Gold")
    {
        if (type == "Gold" && iGoldCoin.useGold(cost))
        {
            unitUpgradeCost(ref upgradeCost[index], cost, type);

            upgradeLevel[index]++;
        }
        else if (type == "Coin" && iGoldCoin.useCoin(cost))
        {
            unitUpgradeCost(ref upgradeCost[index], cost, type);

            upgradeLevel[index]++;
        }
        else return;

        if (index != 3)
        {
            foreach (var grade in grades)
            {
                unitUpgradeApply(grade);
            }
        }

        onUpgradeLevelChange.Invoke();
        onUpgradeCostChange.Invoke();
        onUpgradePerChange.Invoke();
    }

    public void unitUpgradeCost(ref int cost, int amount, string type = "Gold")
    {
        if (type == "Gold")
            iGoldCoin.setGold(-cost);
        else if (type == "Coin")
            iGoldCoin.setCoin(-cost);

        cost += amount;
    }

    public void unitUpgradeApply(EUnitGrade grade)
    {
        List<GameObject> unitList = iUnitMng.getAllUnitList();

        for (int i = 0; i < unitList.Count; i++)
        {
            if (unitList[i].GetComponent<Unit>().eUnitGrade == grade)
            {
                upgrade(unitList[i].GetComponent<Unit>(), grade);
            }
        }
    }

    public void upgrade(Unit unit, EUnitGrade grade)
    {
        int curUpgradeLevel = 0;

        switch (grade)
        {
            case EUnitGrade.C:
                curUpgradeLevel = upgradeLevel[0]; 
                break;
            case EUnitGrade.B: 
                curUpgradeLevel = upgradeLevel[0];
                break;
            case EUnitGrade.S:
                curUpgradeLevel = upgradeLevel[1];
                break;
            case EUnitGrade.SS:
                curUpgradeLevel = upgradeLevel[2];
                break;
        }

        switch (curUpgradeLevel)
        {
            case 2: 
                unit.attackDamage *= 2;
                break;
            case 3:
                unit.attackDamage *= 2; 
                break;
            case 4:
                unit.attackDamage *= 2;
                break;
            case 5:
                unit.attackDamage *= 2;
                break;
            case 6:
                unit.attackDamage *= 2;
                break;
        }
    }

    public void missUpgrade(int lastUpgrade, Unit unit)
    {
        for (int i = 1; i <= lastUpgrade; i++)
        {
            switch (i)
            {
                case 2:
                    unit.attackDamage *= 2;
                    break;
                case 3:
                    unit.attackDamage *= 2;
                    break;
                case 4:
                    unit.attackDamage *= 2;
                    break;
                case 5:
                    unit.attackDamage *= 2;
                    break;
                case 6:
                    unit.attackDamage *= 2;
                    break;
            }
        }
    }

    public int[] getUpgradeCost() { return upgradeCost; }

    public int getUpgradeMaxLevel() { return upgradeMaxLevel; }
    public int[] getUpgradeLevel() { return upgradeLevel; }
}

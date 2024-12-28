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

public enum EUnitUpgrdae
{
    CB,
    A,
    SSS,
    per
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

        upgradeCost[(int)EUnitUpgrdae.CB] = 30;
        upgradeCost[(int)EUnitUpgrdae.A] = 50;
        upgradeCost[(int)EUnitUpgrdae.SSS] = 1;
        upgradeCost[(int)EUnitUpgrdae.per] = 100;

        upgradeLevel[(int)EUnitUpgrdae.CB] = 1;
        upgradeLevel[(int)EUnitUpgrdae.A] = 1;
        upgradeLevel[(int)EUnitUpgrdae.SSS] = 1;
        upgradeLevel[(int)EUnitUpgrdae.per] = 1;

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
                    type = "Gold";
                    break;
                case 1:
                    cost = upgradeCost[index];
                    grades.Add(EUnitGrade.A);
                    amount = 50;
                    type = "Gold";
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
                    type = "Gold";
                    break;
            }

            upgradeGrade(index, cost, amount, grades, type);
        }
    }

    public void upgradeGrade(int index, int cost, int amount, List<EUnitGrade> grades, string type)
    {
        if (type == "Gold" && iGoldCoin.getGold() >= cost)
        {
            unitUpgradeCost(ref upgradeCost[index], cost, type, index);

            upgradeLevel[index]++;
        }
        else if (type == "Coin" && iGoldCoin.getCoin() >= cost)
        {
            unitUpgradeCost(ref upgradeCost[index], cost, type, index);

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

        onUpgradeCostChange.Invoke();
        onUpgradeLevelChange.Invoke();
        onUpgradePerChange.Invoke();
    }

    public void unitUpgradeCost(ref int cost, int amount, string type, int index)
    {
        if (type == "Gold")
            iGoldCoin.useGold(cost);
        else if (type == "Coin")
            iGoldCoin.useCoin(cost);

        if (upgradeLevel[index] < 5)
        {
            cost += amount;
        }
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

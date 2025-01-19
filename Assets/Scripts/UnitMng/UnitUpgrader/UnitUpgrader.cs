using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitUpgrader 
{
    public event Action onUpgradeCostChange;
    public event Action onUpgradeLevelChange;
    public event Action onUpgradePerChange;
    void UnitUpgrade(int _index);
    void MissUpgrade(int _lastUpgrade, Unit _unit);
    int[] GetUpgradeLevel();
    int[] GetUpgradeCost();
    int GetUpgradeMaxLevel();
}

public partial class UnitUpgrader : MonoBehaviour, IUnitUpgrader
{
    public event Action onUpgradeCostChange;
    public event Action onUpgradeLevelChange;
    public event Action onUpgradePerChange;

    [SerializeField] int[] upgradeCosts = new int[4];
    [SerializeField] int[] upgradeLevels = new int[4];
    [SerializeField] int upgradeMaxLevel;

    private void Awake()
    {
        upgradeCosts[(int)EUnitUpgrade.CB] = 30;
        upgradeCosts[(int)EUnitUpgrade.A] = 50;
        upgradeCosts[(int)EUnitUpgrade.SSS] = 1;
        upgradeCosts[(int)EUnitUpgrade.per] = 100;

        upgradeLevels[(int)EUnitUpgrade.CB] = 1;
        upgradeLevels[(int)EUnitUpgrade.A] = 1;
        upgradeLevels[(int)EUnitUpgrade.SSS] = 1;
        upgradeLevels[(int)EUnitUpgrade.per] = 1;

        upgradeMaxLevel = 6;
    }

    public void UnitUpgrade(int _index)
    {
        if (upgradeLevels[_index] < upgradeMaxLevel) 
        {
            int cost = 0;
            int amount = 0;
            string type = "Gold";
            List<EUnitGrade> grades = new List<EUnitGrade>();

            switch (_index)
            {
                case 0:
                    cost = upgradeCosts[_index];
                    amount = 30;
                    grades.Add(EUnitGrade.C);
                    grades.Add(EUnitGrade.B);
                    type = "Gold";
                    break;
                case 1:
                    cost = upgradeCosts[_index];
                    grades.Add(EUnitGrade.A);
                    amount = 50;
                    type = "Gold";
                    break;
                case 2:
                    cost = upgradeCosts[_index];
                    grades.Add(EUnitGrade.S);
                    grades.Add(EUnitGrade.SS);
                    amount = 1;
                    type = "Coin";
                    break;
                case 3:
                    cost = upgradeCosts[_index];
                    amount = 100;
                    type = "Gold";
                    break;
            }

            UpgradeGrade(_index, cost, amount, grades, type);
        }
    }

    public void UpgradeGrade(int _index, int _cost, int _amount, List<EUnitGrade> _grades, string _type)
    {
        if (_type == "Gold" && Shared.gameMng.iGoldCoin.GetGold() >= _cost)
        {
            UnitUpgradeCost(ref upgradeCosts[_index], _cost, _type, _index);

            upgradeLevels[_index]++;
        }
        else if (_type == "Coin" && Shared.gameMng.iGoldCoin.GetGold() >= _cost)
        {
            UnitUpgradeCost(ref upgradeCosts[_index], _cost, _type, _index);

            upgradeLevels[_index]++;
        }
        else return;

        if (_index != 3)
        {
            foreach (var grade in _grades)
            {
                UnitUpgradeApply(grade);
            }
        }

        onUpgradeCostChange.Invoke();
        onUpgradeLevelChange.Invoke();
        onUpgradePerChange.Invoke();
    }

    public void UnitUpgradeCost(ref int _cost, int _amount, string _type, int _index)
    {
        if (_type == "Gold")
            Shared.gameMng.iGoldCoin.UseGold(_cost);
        else if (_type == "Coin")
            Shared.gameMng.iGoldCoin.UseGold(_cost);

        if (upgradeLevels[_index] < 5)
        {
            _cost += _amount;
        }
    }

    public void UnitUpgradeApply(EUnitGrade _grade)
    {
        List<GameObject> unitList = Shared.unitMng.GetAllUnitList();

        for (int i = 0; i < unitList.Count; i++)
        {
            if (unitList[i].GetComponent<Unit>().eUnitGrade == _grade)
            {
                Upgrade(unitList[i].GetComponent<Unit>(), _grade);
            }
        }
    }

    public void Upgrade(Unit _unit, EUnitGrade _grade)
    {
        int curUpgradeLevel = 0;

        switch (_grade)
        {
            case EUnitGrade.C:
                curUpgradeLevel = upgradeLevels[0]; 
                break;
            case EUnitGrade.B: 
                curUpgradeLevel = upgradeLevels[0];
                break;
            case EUnitGrade.S:
                curUpgradeLevel = upgradeLevels[1];
                break;
            case EUnitGrade.SS:
                curUpgradeLevel = upgradeLevels[2];
                break;
        }

        switch (curUpgradeLevel)
        {
            case 2: 
                _unit.attackDamage *= 2;
                break;
            case 3:
                _unit.attackDamage *= 2; 
                break;
            case 4:
                _unit.attackDamage *= 2;
                break;
            case 5:
                _unit.attackDamage *= 2;
                break;
            case 6:
                _unit.attackDamage *= 2;
                break;
        }
    }

    public void MissUpgrade(int _lastUpgrade, Unit _unit)
    {
        for (int i = 1; i <= _lastUpgrade; i++)
        {
            switch (i)
            {
                case 2:
                    _unit.attackDamage *= 2;
                    break;
                case 3:
                    _unit.attackDamage *= 2;
                    break;
                case 4:
                    _unit.attackDamage *= 2;
                    break;
                case 5:
                    _unit.attackDamage *= 2;
                    break;
                case 6:
                    _unit.attackDamage *= 2;
                    break;
            }
        }
    }

    public int[] GetUpgradeCost() { return upgradeCosts; }

    public int GetUpgradeMaxLevel() { return upgradeMaxLevel; }
    public int[] GetUpgradeLevel() { return upgradeLevels; }
}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string name = "";
    public int level = 1;
    public float curExp = 0f;
    public float maxExp = 100f;
    public float gold = 0;
    public int gem = 0;
    public int paper = 0;
    public bool first = true;

    public List<ItemState> items = new List<ItemState>();
    public List<UnitState> units = new List<UnitState>();
}

[System.Serializable]
public class UnitState
{
    public int index;
    public string unitName;
    public int unitDamage;
    public int attackSpeed;
    public float attackRange;
    public Sprite unitImg;
    public float unitLevel;
    public string skill1Name;
    public float unitUpgradeCost;
    public float unitStoreCost;
    public float unitCurExp;
    public float unitMaxExp;
    public float unitGrade;

    public UnitState(UnitData unitData)
    {
        index = unitData.index;
        unitName = unitData.unitName;
        unitDamage = unitData.unitDamage;
        attackSpeed = unitData.attackSpeed;
        attackRange = unitData.attackRange;
        unitLevel = unitData.unitLevel;
        skill1Name = unitData.skill1Name;
        unitUpgradeCost = unitData.unitUpgradeCost;
        unitStoreCost = unitData.unitStoreCost;
        unitCurExp = unitData.unitCurExp;
        unitMaxExp = unitData.unitMaxExp;
        unitGrade = unitData.unitGrade;
    }
}

public class ItemState
{
    public int index;
    public string treasureName;
    public int treasureLevel;
    public int treasureCurExp;
    public int treasureMaxExp;
    public int treasureCost;
    public Sprite treasureImg;
    public float treasureBase;
    public float treasureUpgrade;
    public string treasureDc;
    public float storeCost;

    public ItemState(TreasureData itemData)
    {
        index = itemData.index;
        treasureName = itemData.treasureName;
        treasureLevel = itemData.treasureLevel;
        treasureCurExp = itemData.treasureCurExp;
        treasureMaxExp = itemData.treasureMaxExp;
        treasureCost = itemData.treasureCost;
        treasureBase = itemData.treasureBase;
        treasureUpgrade = itemData.treasureUpgrade;
        treasureDc = itemData.treasureDc;
        storeCost = itemData.storeCost;
    }
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public PlayerData playerdata = new PlayerData();
    public string path;

    public List<UnitData> allUnits;
    public List<UnitState> playerUnits;
    public List<TreasureData> allItem;
    public List<ItemState> playerItem;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        path = Application.persistentDataPath + "/save";
    }

    public void saveData()
    {
        playerdata.units.Clear();
        for(int i = 0; i < playerUnits.Count; i++) 
        {
            playerdata.units.Add(playerUnits[i]);
        }

        string data = JsonUtility.ToJson(playerdata);
        File.WriteAllText(path, data);
    }

    public void loadData()
    {
        if (File.Exists(path))
        {
            string data = File.ReadAllText(path);
            playerdata = JsonUtility.FromJson<PlayerData>(data);

            playerUnits.Clear();

            for (int i = 0; i < playerdata.units.Count; i++)
            {
                UnitState saveUnitState = playerdata.units[i];
                UnitData baseUnit = allUnits.Find(u => u.index == saveUnitState.index);

                if (baseUnit != null)
                {
                    UnitState newUnitState = new UnitState(baseUnit)
                    {
                        index = saveUnitState.index,
                        unitName = saveUnitState.unitName,
                        unitDamage = saveUnitState.unitDamage,
                        attackSpeed = saveUnitState.attackSpeed,
                        attackRange = saveUnitState.attackRange,
                        unitLevel = saveUnitState.unitLevel,
                        skill1Name = saveUnitState.skill1Name,
                        unitUpgradeCost = saveUnitState.unitUpgradeCost,
                        unitStoreCost = saveUnitState.unitStoreCost,
                        unitCurExp = saveUnitState.unitCurExp,
                        unitMaxExp = saveUnitState.unitMaxExp,
                        unitGrade = saveUnitState.unitGrade
                    };
                    playerUnits.Add(newUnitState);
                }
            }
        }
        else
        {
            playerUnits.Clear();

            for (int i = 0; i < playerdata.units.Count; i++)
            {
                UnitData baseUnit = allUnits[i];
                UnitState newUnitState = new UnitState(baseUnit)
                {
                    index = baseUnit.index,
                    unitName = baseUnit.unitName,
                    unitDamage = baseUnit.unitDamage,
                    attackSpeed = baseUnit.attackSpeed,
                    attackRange = baseUnit.attackRange,
                    unitLevel = baseUnit.unitLevel,
                    skill1Name = baseUnit.skill1Name,
                    unitUpgradeCost = baseUnit.unitUpgradeCost,
                    unitStoreCost = baseUnit.unitStoreCost,
                    unitCurExp = baseUnit.unitCurExp,
                    unitMaxExp = baseUnit.unitMaxExp,
                    unitGrade = baseUnit.unitGrade
                };
            }
        }
    }
}

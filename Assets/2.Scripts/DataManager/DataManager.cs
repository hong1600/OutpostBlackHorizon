using System.Collections;
using System.Collections.Generic;
using System.IO;
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

    public List<UnitState> units = null;
    public List<ItemState> items = null;
}

[System.Serializable]
public class UnitState
{
    public int index;
    public string unitName;
    public int unitDamage;
    public int attackSpeed;
    public float attackRange;
    public float unitLevel;
    public string skill1Name;
    public float unitUpgradeCost;
    public float unitStoreCost;
    public float unitCurExp;
    public float unitMaxExp;
    public float unitGrade;
    public Sprite unitImg;

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

[System.Serializable]
public class ItemState
{
    public int index;
    public string treasureName;
    public int treasureLevel;
    public int treasureCurExp;
    public int treasureMaxExp;
    public int treasureCost;
    public float treasureBase;
    public float treasureUpgrade;
    public string treasureDc;
    public float storeCost;
    public Sprite treasureImg;

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

    public PlayerDataMng playerDataMng;
    public SaveLoadMng saveLoadMng;
    public UnitDataMng unitDataMng;
    public ItemDataMng itemDataMng;

    public PlayerData playerdata = new PlayerData();
    public List<TreasureData> item = new List<TreasureData>();
    public List<UnitData> unit = new List<UnitData>();

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

        playerDataMng = new PlayerDataMng();
        unitDataMng = new UnitDataMng();
        itemDataMng = new ItemDataMng();
        saveLoadMng = new SaveLoadMng();

        saveLoadMng.loadData();
        playerDataMng.initialized();
    }
}


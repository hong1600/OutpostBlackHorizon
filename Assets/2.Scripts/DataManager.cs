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

    public PlayerData playerdata = new PlayerData();
    public string path;

    public List<TreasureData> item;
    public List<UnitData> unit;
    public List<TreasureData> itemBase;
    public List<UnitData> unitBase;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            saveData();
        }
        if (Input.GetKeyDown(KeyCode.L))
        { 
            loadData();
        }
    }

    public void saveData()
    {
        string data = JsonUtility.ToJson(playerdata, true);
        File.WriteAllText(path, data);
        Debug.Log("Save!");
    }

    public void loadData()
    {
        if (File.Exists(path))
        {
            string data = File.ReadAllText(path);
            playerdata = JsonUtility.FromJson<PlayerData>(data);

            for (int i = 0; i < playerdata.units.Count; i++)
            {
                UnitState saveUnit = playerdata.units[i];

                UnitData baseUnit = unit.Find(u => u.index == saveUnit.index);

                if (baseUnit != null)
                {
                    UnitState loadUnit = new UnitState(baseUnit);
                    loadUnit.index = saveUnit.index;
                    loadUnit.unitName = saveUnit.unitName;
                    loadUnit.unitDamage = saveUnit.unitDamage;
                    loadUnit.attackSpeed = saveUnit.attackSpeed;
                    loadUnit.attackRange = saveUnit.attackRange;
                    loadUnit.unitLevel = saveUnit.unitLevel;
                    loadUnit.unitCurExp = saveUnit.unitCurExp;
                    loadUnit.unitMaxExp = saveUnit.unitMaxExp;
                    loadUnit.unitUpgradeCost = saveUnit.unitUpgradeCost;
                    loadUnit.unitStoreCost = saveUnit.unitStoreCost;
                    loadUnit.skill1Name = saveUnit.skill1Name;
                    loadUnit.unitGrade = saveUnit.unitGrade;
                    loadUnit.unitImg = Resources.Load<Sprite>("Units/" + saveUnit.index.ToString());

                    playerdata.units[i] = loadUnit;
                }
            }

            for (int i = 0; i < playerdata.items.Count; i++)
            {
                ItemState saveItem = playerdata.items[i];

                TreasureData baseItem = item.Find(i => i.index == saveItem.index);

                if (baseItem != null)
                {
                    ItemState loadItem = new ItemState(baseItem);
                    loadItem.index = saveItem.index;
                    loadItem.treasureName = saveItem.treasureName;
                    loadItem.treasureLevel = saveItem.treasureLevel;
                    loadItem.treasureCurExp = saveItem.treasureCurExp;
                    loadItem.treasureMaxExp = saveItem.treasureMaxExp;
                    loadItem.treasureCost = saveItem.treasureCost;
                    loadItem.treasureBase = saveItem.treasureBase;
                    loadItem.treasureUpgrade = saveItem.treasureUpgrade;
                    loadItem.treasureDc = saveItem.treasureDc;
                    loadItem.storeCost = saveItem.storeCost;
                    loadItem.treasureImg = Resources.Load<Sprite>("Items/" + saveItem.index.ToString());

                    playerdata.items[i] = loadItem;
                }
            }
            Debug.Log("Load!");
        }
        else
        {
            playerdata.units = new List<UnitState>();

            for (int i = 0; i < unit.Count; i++)
            {
                UnitState newUnit = new UnitState(unit[i]);
                playerdata.units.Add(newUnit);
            }
            for (int i = 0; i < item.Count; i++)
            {
                ItemState newItem = new ItemState(item[i]);
                playerdata.items.Add(newItem);
            }
        }
    }
    public UnitState curUnit(int index)
    {
        return playerdata.units.Find(u => u.index == index);
    }
    public ItemState curItem(int index)
    {
        return playerdata.items.Find(i => i.index == index);
    }
}


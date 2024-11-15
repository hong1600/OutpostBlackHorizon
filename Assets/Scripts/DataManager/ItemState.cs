using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

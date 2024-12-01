using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemState
{
    public int index;
    public string itemName;
    public int itemLevel;
    public int itemCurExp;
    public int itemMaxExp;
    public int itemCost;
    public float itemBase;
    public float itemUpgrade;
    public string itemDc;
    public float itemStoreCost;
    public Sprite itemImg;

    public ItemState(ItemData itemData)
    {
        index = itemData.index;
        itemName = itemData.itemName;
        itemLevel = itemData.itemLevel;
        itemCurExp = itemData.itemCurExp;
        itemMaxExp = itemData.itemMaxExp;
        itemCost = itemData.itemCost;
        itemBase = itemData.itemBase;
        itemUpgrade = itemData.itemUpgrade;
        itemDc = itemData.itemDc;
        itemStoreCost = itemData.itemStoreCost;
    }
}

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

    public ItemState(ItemData _itemData)
    {
        index = _itemData.index;
        itemName = _itemData.itemName;
        itemLevel = _itemData.itemLevel;
        itemCurExp = _itemData.itemCurExp;
        itemMaxExp = _itemData.itemMaxExp;
        itemCost = _itemData.itemCost;
        itemBase = _itemData.itemBase;
        itemUpgrade = _itemData.itemUpgrade;
        itemDc = _itemData.itemDc;
        itemStoreCost = _itemData.itemStoreCost;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptble Object/ItemData")]

public class ItemData : ScriptableObject
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
}

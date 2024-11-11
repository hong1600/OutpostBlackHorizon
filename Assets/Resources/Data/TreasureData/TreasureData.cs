using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Treasure", menuName = "Scriptble Object/TreasureData")]

public class TreasureData : ScriptableObject
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
}

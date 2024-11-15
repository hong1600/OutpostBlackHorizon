using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public string name = "";
    public int level = 1;
    public int curExp = 0;
    public int maxExp = 100;
    public int gold = 0;
    public int gem = 0;
    public int paper = 0;
    public bool first = true;

    public List<UnitState> units = null;
    public List<ItemState> items = null;
}

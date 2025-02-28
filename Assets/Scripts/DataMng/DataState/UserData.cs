using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData
{
    public string userID;
    public string userName;
    public int userLevel;
    public int curExp;
    public int maxExp;
    public int gold;
    public int gem;
    public int paper;
    public bool first;

    public List<UnitState> unitStateList = new List<UnitState>();
    public List<ItemState> itemStateList = new List<ItemState>();
}

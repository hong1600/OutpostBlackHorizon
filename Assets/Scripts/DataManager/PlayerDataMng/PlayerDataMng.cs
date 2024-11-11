using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataMng
{
    public PlayerData playerData => DataManager.instance.playerdata;

    public void initialized()
    {
        if (playerData.units == null || playerData.units.Count == 0)
        {
            DataManager.instance.unitDataMng.InitializeUnits(playerData);
        }
        else
        {
            DataManager.instance.unitDataMng.loadUnitState(playerData);
        }

        if (playerData.items == null || playerData.items.Count == 0)
        {
            DataManager.instance.itemDataMng.InitializeItems(playerData);
        }
        else
        {
            DataManager.instance.itemDataMng.LoadItemStates(playerData);
        }

    }

    public UnitState getUnit(int index)
    {
        return playerData.units.Find(u => u.index == index);
    }

    public ItemState getItem(int index)
    {
        return playerData.items.Find(i => i.index == index);
    }

}

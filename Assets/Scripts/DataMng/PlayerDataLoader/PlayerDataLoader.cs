using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataLoader
{
    public PlayerData playerData;

    public PlayerDataLoader()
    {
        playerData = DataMng.instance.playerData;
    }

    public void Initialize()
    {
        if (playerData.unitStateList == null || playerData.unitStateList.Count == 0)
        {
            DataMng.instance.unitDataLoader.InitializeUnits(playerData);
        }
        else
        {
            DataMng.instance.unitDataLoader.LoadUnitState(playerData);
        }

        if (playerData.itemStateList == null || playerData.itemStateList.Count == 0)
        {
            DataMng.instance.itemDataLoader.InitializeItems(playerData);
        }
        else
        {
            DataMng.instance.itemDataLoader.LoadItemStates(playerData);
        }

    }

    public UnitState GetUnit(int _index)
    {
        return playerData.unitStateList.Find(unit => unit.index == _index);
    }

    public ItemState GetItem(int _index)
    {
        return playerData.itemStateList.Find(item => item.index == _index);
    }

}

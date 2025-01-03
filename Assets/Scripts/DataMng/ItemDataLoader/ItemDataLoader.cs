using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataLoader
{
    public List<ItemData> items;

    public ItemDataLoader()
    {
        items = DataMng.instance.itemDataList;
    }

    public void LoadItemStates(PlayerData _playerData)
    {
        for (int i = 0; i < _playerData.itemStateList.Count; i++)
        {
            ItemState saveItem = _playerData.itemStateList[i];
            ItemData baseItem = items.Find(item => item.index == saveItem.index);

            if (baseItem != null)
            {
                ItemState loadItem = _playerData.itemStateList[i];
                loadItem.index = saveItem.index;
                loadItem.itemName = saveItem.itemName;
                loadItem.itemLevel = saveItem.itemLevel;
                loadItem.itemCurExp = saveItem.itemCurExp;
                loadItem.itemMaxExp = saveItem.itemMaxExp;
                loadItem.itemCost = saveItem.itemCost;
                loadItem.itemBase = saveItem.itemBase;
                loadItem.itemUpgrade = saveItem.itemUpgrade;
                loadItem.itemDc = saveItem.itemDc;
                loadItem.itemStoreCost = saveItem.itemStoreCost;
                loadItem.itemImg = Resources.Load<Sprite>("Items/" + saveItem.index.ToString());
                _playerData.itemStateList[i] = loadItem;
            }
        }
    }

    public void InitializeItems(PlayerData _playerData)
    {
        if (_playerData.itemStateList == null)
        {
            _playerData.itemStateList = new List<ItemState>();
        }

        foreach (var itemData in DataMng.instance.itemDataList)
        {
            ItemState itemState = new ItemState(itemData);
            _playerData.itemStateList.Add(itemState);
        }
    }
}

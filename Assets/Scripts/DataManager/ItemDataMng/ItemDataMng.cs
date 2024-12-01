using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataMng
{
    public List<ItemData> item;

    public ItemDataMng()
    {
        item = DataManager.instance.item;
    }

    public void LoadItemStates(PlayerData playerData)
    {
        for (int i = 0; i < playerData.items.Count; i++)
        {
            ItemState saveItem = playerData.items[i];
            ItemData baseItem = item.Find(i => i.index == saveItem.index);

            if (baseItem != null)
            {
                ItemState loadItem = playerData.items[i];
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
                playerData.items[i] = loadItem;
            }
        }
    }

    public void InitializeItems(PlayerData playerData)
    {
        if (playerData.items == null)
        {
            playerData.items = new List<ItemState>();
        }

        foreach (var itemData in DataManager.instance.item)
        {
            ItemState itemState = new ItemState(itemData);
            playerData.items.Add(itemState);
        }
    }
}

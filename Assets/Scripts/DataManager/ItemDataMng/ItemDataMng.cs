using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataMng
{
    public List<TreasureData> item;

    public ItemDataMng()
    {
        item = DataManager.instance.item;
    }

    public void LoadItemStates(PlayerData playerData)
    {
        for (int i = 0; i < playerData.items.Count; i++)
        {
            ItemState saveItem = playerData.items[i];
            TreasureData baseItem = item.Find(i => i.index == saveItem.index);

            if (baseItem != null)
            {
                ItemState loadItem = playerData.items[i];
                loadItem.index = saveItem.index;
                loadItem.treasureName = saveItem.treasureName;
                loadItem.treasureLevel = saveItem.treasureLevel;
                loadItem.treasureCurExp = saveItem.treasureCurExp;
                loadItem.treasureMaxExp = saveItem.treasureMaxExp;
                loadItem.treasureCost = saveItem.treasureCost;
                loadItem.treasureBase = saveItem.treasureBase;
                loadItem.treasureUpgrade = saveItem.treasureUpgrade;
                loadItem.treasureDc = saveItem.treasureDc;
                loadItem.storeCost = saveItem.storeCost;
                loadItem.treasureImg = Resources.Load<Sprite>("Items/" + saveItem.index.ToString());
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

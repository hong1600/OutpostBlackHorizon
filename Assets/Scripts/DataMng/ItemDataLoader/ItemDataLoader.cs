using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataLoader : MonoBehaviour
{
    public List<ItemData> items;

    private void Awake()
    {
        
    }

    public void LoadItemStates(UserData _playerData)
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
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreUI : MonoBehaviour
{
    public MainUI mainUI;
    public TreasureUI treasureUI;

    public UnitState curUnit;
    public ItemState curItem;
    public Button[] storeUnitBtn;
    public UnitData[] storeUnits;
    public TreasureData[] storeItems;
    public Image[] storeUnitsImg;
    public TextMeshProUGUI[] storeUnitsNameText;
    public TextMeshProUGUI[] storeUnitsCostText;
    public GameObject storeUnitDcPanel;
    public Image storeUnitsDcImg;
    public TextMeshProUGUI storeUnitsDcCost;
    bool clickUnit;

    private void Start()
    {
        storeSlotReset();
    }

    public void storeSlotClick(int index, string Name)
    {
        if (Name == "Unit")
        {
            curUnit = DataManager.instance.playerDataMng.getUnit(index);
            clickUnit = true;

            storeUnitsDcImg.sprite = curUnit.unitImg;
            storeUnitsDcCost.text = curUnit.unitStoreCost.ToString();

            mainUI.showPanelOpen(storeUnitDcPanel);

        }
        else if (Name == "Treasure")
        {
            curItem = DataManager.instance.playerDataMng.getItem(index);
            clickUnit = false;

            storeUnitsDcImg.sprite = curItem.treasureImg;
            storeUnitsDcCost.text = curItem.storeCost.ToString();

            mainUI.showPanelOpen(storeUnitDcPanel);
        }
    }

    public void storeSlotReset()
    {
        for (int i = 0; i < storeUnitsImg.Length; i++)
        {
            int or = UnityEngine.Random.Range(0, 2);

            if (or == 0)
            {
                int slotRand = UnityEngine.Random.Range(0, storeUnits.Length);
                int curUnitIndex = storeUnits[slotRand].index;

                storeUnitsImg[i].sprite = storeUnits[slotRand].unitImg;
                storeUnitsNameText[i].text = storeUnits[slotRand].unitName;
                storeUnitsCostText[i].text = storeUnits[slotRand].unitStoreCost.ToString();

                storeUnitBtn[i].GetComponent<Button>().onClick.RemoveAllListeners();

                int index = curUnitIndex;
                string Name = "Unit";
                storeUnitBtn[i].GetComponent<Button>().onClick.AddListener
                    (() => mainUI.btn.storeUnitBtn(index, Name));

            }
            else if (or == 1)
            {
                int slotRand = UnityEngine.Random.Range(0, treasureUI.treasureSlider.Count);
                int curItemIndex = storeItems[slotRand].index;

                storeUnitsImg[i].sprite = storeItems[slotRand].treasureImg;
                storeUnitsNameText[i].text = storeItems[slotRand].treasureName;
                storeUnitsCostText[i].text = storeItems[slotRand].storeCost.ToString();

                storeUnitBtn[i].GetComponent<Button>().onClick.RemoveAllListeners();

                int index = curItemIndex;
                string Name = "Treasure";
                storeUnitBtn[i].GetComponent<Button>().onClick.AddListener
                    (() => mainUI.btn.storeUnitBtn(index, Name));
            }
        }
    }

    public void storeBuy()
    {
        if (clickUnit == true)
        {
            if (DataManager.instance.playerdata.gold >= curUnit.unitStoreCost)
            {
                DataManager.instance.playerdata.gold -= (int)curUnit.unitStoreCost;
                curUnit.unitCurExp += 1f;
                storeUnitDcPanel.SetActive(false);
            }
        }

        else
        {
            if (DataManager.instance.playerdata.gold >= curItem.storeCost)
            {
                DataManager.instance.playerdata.gold -= (int)curItem.storeCost;
                curItem.treasureCurExp += 1;
                treasureUI.treasureUpdate1();
                storeUnitDcPanel.SetActive(false);

            }
        }
    }
}

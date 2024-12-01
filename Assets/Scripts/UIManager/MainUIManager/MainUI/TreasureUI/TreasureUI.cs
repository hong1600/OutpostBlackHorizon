using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TreasureUI : MonoBehaviour
{
    public MainUI mainUI;

    public GameObject itemDcPanel;
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemLevelText;
    public TextMeshProUGUI itemExpText;
    public TextMeshProUGUI itemUpgradeText;
    public TextMeshProUGUI itemCostText;
    public Image itemImg;
    public Image itemFill;
    public List<Image> itemSlider = new List<Image>();
    public List<TextMeshProUGUI> itemLevel = new List<TextMeshProUGUI>();
    public List<TextMeshProUGUI> itemExp = new List<TextMeshProUGUI>();

    private void Start()
    {
        itemUpdate1();
    }

    public void itemDc(int index)
    {
        mainUI.curItems = DataManager.instance.playerDataMng.getItem(index);

        itemUpdate2();

        mainUI.showPanelOpen(itemDcPanel);
    }

    public void itemUpdate1()
    {
        for (int i = 0; i < itemSlider.Count; i++)
        {
            mainUI.curItems = DataManager.instance.playerDataMng.getItem(i);

            itemSlider[i].fillAmount = mainUI.curItems.itemCurExp / mainUI.curItems.itemMaxExp;
            itemLevel[i].text = "LV." + mainUI.curItems.itemLevel.ToString();
            itemExp[i].text = $"{mainUI.curItems.itemCurExp}/{mainUI.curItems.itemMaxExp}";
        }
    }

    public void itemUpdate2()
    {
        itemNameText.text = mainUI.curItems.itemName;
        itemLevelText.text = "LV." + mainUI.curItems.itemLevel.ToString();
        itemExpText.text = $"{mainUI.curItems.itemCurExp}/{mainUI.curItems.itemMaxExp}";
        itemCostText.text = mainUI.curItems.itemCost.ToString();
        itemUpgradeText.text = string.Format
            (mainUI.curItems.itemDc, mainUI.curItems.itemBase, mainUI.curItems.itemUpgrade);
        itemFill.fillAmount = mainUI.curItems.itemCurExp / mainUI.curItems.itemMaxExp;
        itemImg.sprite = mainUI.curItems.itemImg;
    }

    public void itemUpgrade()
    {
        if (mainUI.curItems.itemCost < DataManager.instance.playerdata.gold)
        {
            switch (mainUI.curItems.itemLevel)
            {
                case 1:
                    mainUI.curItems.itemCost = 2000;
                    mainUI.curItems.itemLevel = 2;
                    mainUI.curItems.itemCurExp -= mainUI.curItems.itemMaxExp;
                    mainUI.curItems.itemMaxExp++;
                    mainUI.curItems.itemBase += mainUI.curItems.itemUpgrade;
                    break;

                case 2:
                    mainUI.curItems.itemCost = 3000;
                    mainUI.curItems.itemLevel = 3;
                    mainUI.curItems.itemCurExp -= mainUI.curItems.itemMaxExp;
                    mainUI.curItems.itemMaxExp++;
                    mainUI.curItems.itemBase += mainUI.curItems.itemUpgrade;
                    break;

                case 3:
                        mainUI.curItems.itemCost = 4000;
                    mainUI.curItems.itemLevel = 4;
                    mainUI.curItems.itemCurExp -= mainUI.curItems.itemMaxExp;
                    mainUI.curItems.itemMaxExp++;
                    mainUI.curItems.itemBase += mainUI.curItems.itemUpgrade;
                    break;
            }
            itemUpdate2();
            itemUpdate1();
        }
    }
}

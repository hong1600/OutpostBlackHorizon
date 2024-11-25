using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TreasureUI : MonoBehaviour
{
    public MainUI mainUI;

    public GameObject treasureDcPanel;
    public TextMeshProUGUI treasureNameText;
    public TextMeshProUGUI treasureLevelText;
    public TextMeshProUGUI treasureExpText;
    public TextMeshProUGUI treasureUpgradeText;
    public TextMeshProUGUI treasureCostText;
    public Image treasureImg;
    public Image treasureFill;
    public List<Image> treasureSlider = new List<Image>();
    public List<TextMeshProUGUI> treasureLevel = new List<TextMeshProUGUI>();
    public List<TextMeshProUGUI> treasureExp = new List<TextMeshProUGUI>();

    private void Start()
    {
        treasureUpdate1();
    }

    public void treasureDc(int index)
    {
        mainUI.curItems = DataManager.instance.playerDataMng.getItem(index);

        treasureUpdate2();

        mainUI.showPanelOpen(treasureDcPanel);
    }

    public void treasureUpdate1()
    {
        for (int i = 0; i < treasureSlider.Count; i++)
        {
            mainUI.curItems = DataManager.instance.playerDataMng.getItem(i);

            treasureSlider[i].fillAmount = mainUI.curItems.treasureCurExp / mainUI.curItems.treasureMaxExp;
            treasureLevel[i].text = "LV." + mainUI.curItems.treasureLevel.ToString();
            treasureExp[i].text = $"{mainUI.curItems.treasureCurExp}/{mainUI.curItems.treasureMaxExp}";
        }
    }

    public void treasureUpdate2()
    {
        treasureNameText.text = mainUI.curItems.treasureName;
        treasureLevelText.text = "LV." + mainUI.curItems.treasureLevel.ToString();
        treasureExpText.text = $"{mainUI.curItems.treasureCurExp}/{mainUI.curItems.treasureMaxExp}";
        treasureCostText.text = mainUI.curItems.treasureCost.ToString();
        treasureUpgradeText.text = string.Format
            (mainUI.curItems.treasureDc, mainUI.curItems.treasureBase, mainUI.curItems.treasureUpgrade);
        treasureFill.fillAmount = mainUI.curItems.treasureCurExp / mainUI.curItems.treasureMaxExp;
        treasureImg.sprite = mainUI.curItems.treasureImg;
    }

    public void treasureUpgrade()
    {
        if (mainUI.curItems.treasureCost < DataManager.instance.playerdata.gold)
        {
            switch (mainUI.curItems.treasureLevel)
            {
                case 1:
                    mainUI.curItems.treasureCost = 2000;
                    mainUI.curItems.treasureLevel = 2;
                    mainUI.curItems.treasureCurExp -= mainUI.curItems.treasureMaxExp;
                    mainUI.curItems.treasureMaxExp++;
                    mainUI.curItems.treasureBase += mainUI.curItems.treasureUpgrade;
                    break;

                case 2:
                    mainUI.curItems.treasureCost = 3000;
                    mainUI.curItems.treasureLevel = 3;
                    mainUI.curItems.treasureCurExp -= mainUI.curItems.treasureMaxExp;
                    mainUI.curItems.treasureMaxExp++;
                    mainUI.curItems.treasureBase += mainUI.curItems.treasureUpgrade;
                    break;

                case 3:
                        mainUI.curItems.treasureCost = 4000;
                    mainUI.curItems.treasureLevel = 4;
                    mainUI.curItems.treasureCurExp -= mainUI.curItems.treasureMaxExp;
                    mainUI.curItems.treasureMaxExp++;
                    mainUI.curItems.treasureBase += mainUI.curItems.treasureUpgrade;
                    break;
            }
            treasureUpdate2();
            treasureUpdate1();
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI gemText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] Slider expSlider;

    [SerializeField] GameObject treasureDcPanel; 
    [SerializeField] TextMeshProUGUI treasureNameText;
    [SerializeField] TextMeshProUGUI treasureLevelText;
    [SerializeField] TextMeshProUGUI treasureExpText;
    [SerializeField] TextMeshProUGUI treasureUpgradeText;
    [SerializeField] TextMeshProUGUI treasureCostText;
    [SerializeField] Image treasureFill;

    [SerializeField] TreasureData curItems;

    [SerializeField] List<Image> treasureSlider = new List<Image>();
    [SerializeField] List<TextMeshProUGUI> treasureLevel = new List<TextMeshProUGUI>();
    [SerializeField] List<TextMeshProUGUI> treasureExp = new List<TextMeshProUGUI>();

    private void Start()
    {
        treasureUpdate1();
    }

    private void Update()
    {
        main();
    }

    private void main()
    {
        nameText.text = DataManager.instance.playerdata.name.ToString();

        levelText.text = DataManager.instance.playerdata.level.ToString();

        expSlider.value = DataManager.instance.playerdata.curExp / DataManager.instance.playerdata.maxExp;

        goldText.text = DataManager.instance.playerdata.gold.ToString();

        gemText.text = DataManager.instance.playerdata.gem.ToString();
    }

    public void treasureDc(int index)
    {
        curItems = DataManager.instance.playerdata.items[index];

        treasureUpdate2();

        treasureDcPanel.SetActive(true);
    }

    private void treasureUpdate1()
    {
        for (int i = 0; i < treasureSlider.Count; i++)
        {
            curItems = DataManager.instance.playerdata.items[i];

            treasureSlider[i].fillAmount = curItems.treasureCurExp / curItems.treasureMaxExp;
            treasureLevel[i].text = "LV." + curItems.treasureLevel.ToString();
            treasureExp[i].text = $"{curItems.treasureCurExp}/{curItems.treasureMaxExp}";
        }
    }

    private void treasureUpdate2()
    {
        treasureNameText.text = curItems.treasureName;
        treasureLevelText.text = "LV." + curItems.treasureLevel.ToString();
        treasureExpText.text = $"{curItems.treasureCurExp}/{curItems.treasureMaxExp}";
        treasureCostText.text = curItems.treasureCost.ToString();
        treasureUpgradeText.text = string.Format
            (curItems.treasureDc, curItems.treasureBase, curItems.treasureUpgrade);
        treasureFill.fillAmount = curItems.treasureCurExp / curItems.treasureMaxExp;
    }

    public void treasureUpgrade()
    {
        if (curItems.treasureCost < DataManager.instance.playerdata.gold)
        {
            switch (curItems.treasureLevel)
            {
                case 1:
                    curItems.treasureCost = 2000;
                    curItems.treasureLevel = 2;
                    curItems.treasureCurExp = 0;
                    curItems.treasureMaxExp++;
                    curItems.treasureBase += curItems.treasureUpgrade;
                    break;

                case 2:
                    curItems.treasureCost = 3000;
                    curItems.treasureLevel = 3;
                    curItems.treasureCurExp = 0;
                    curItems.treasureMaxExp++;
                    curItems.treasureBase += curItems.treasureUpgrade;
                    break;

                case 3:
                    curItems.treasureCost = 4000;
                    curItems.treasureLevel = 4;
                    curItems.treasureCurExp = 0;
                    curItems.treasureMaxExp++;
                    curItems.treasureBase += curItems.treasureUpgrade;
                    break;
            }

            treasureUpdate2();
            treasureUpdate1();

        }
    }
}

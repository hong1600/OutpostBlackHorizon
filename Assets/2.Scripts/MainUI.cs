using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public UnitData CurHero { get { return curHero; } set { curHero = value; } }

    [Header("메인")]
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI gemText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] Slider expSlider;

    [Header("유물")]
    [SerializeField] TreasureData curItems;
    [SerializeField] GameObject treasureDcPanel; 
    [SerializeField] TextMeshProUGUI treasureNameText;
    [SerializeField] TextMeshProUGUI treasureLevelText;
    [SerializeField] TextMeshProUGUI treasureExpText;
    [SerializeField] TextMeshProUGUI treasureUpgradeText;
    [SerializeField] TextMeshProUGUI treasureCostText;
    [SerializeField] Image treasureFill;
    [SerializeField] List<Image> treasureSlider = new List<Image>();
    [SerializeField] List<TextMeshProUGUI> treasureLevel = new List<TextMeshProUGUI>();
    [SerializeField] List<TextMeshProUGUI> treasureExp = new List<TextMeshProUGUI>();

    [Header("영웅")]
    [SerializeField] UnitData curHero;
    [SerializeField] GameObject heroDcPanel;
    [SerializeField] TextMeshProUGUI heroNameText;
    [SerializeField] TextMeshProUGUI heroLevelText;
    [SerializeField] TextMeshProUGUI heroDamageText;
    [SerializeField] TextMeshProUGUI heroAttackSpeedText;
    [SerializeField] TextMeshProUGUI heroSkillNameText1;
    [SerializeField] TextMeshProUGUI heroSkillNameText2;
    [SerializeField] TextMeshProUGUI heroUpgradeCost;

    [Header("상점")]
    [SerializeField] float store;


    private void Start()
    {
        treasureUpdate1();
    }

    private void Update()
    {
        main();

        if(Input.GetKeyDown(KeyCode.G)) 
        {
            DataManager.instance.playerdata.gold += 10000;
        }
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

    public void heroDc(int index)
    {
        curHero = DataManager.instance.playerdata.units[index];

        heroUpdate2();

        heroDcPanel.SetActive(true);
    }

    private void heroUpdate2()
    {
        heroNameText.text = curHero.unitName;
        heroLevelText.text = "LV." + curHero.unitLevel.ToString();
        heroDamageText.text = curHero.unitDamage.ToString();
        heroAttackSpeedText.text = curHero.attackSpeed.ToString();
        heroUpgradeCost.text = curHero.unitUpgradeCost.ToString();
        heroSkillNameText1.text = $"<u><color=yellow>[{curHero.skill1Name}]</color></u> 스킬이 해금됩니다";
        heroSkillNameText2.text = $"<u><color=yellow>[{curHero.skill1Name}]</color></u> 스킬의 발동확률이 +80% 증가합니다";
    }

    public void HeroUpgrade()
    {
        if (curHero.unitUpgradeCost < DataManager.instance.playerdata.gold)
        {
            switch (curHero.unitLevel)
            {
                case 1:
                    curHero.unitUpgradeCost += 1000f;
                    curHero.unitLevel += 1f;
                    curHero.unitDamage += 5;
                    break;

                case 2:
                    curHero.unitUpgradeCost += 1000f;
                    curHero.unitLevel += 1f;
                    curHero.unitDamage += 5;
                    break;

                case 3:
                    curHero.unitUpgradeCost += 1000f;
                    curHero.unitLevel += 1f;
                    curHero.unitDamage += 5;
                    break;
                case 4:
                    curHero.unitUpgradeCost += 1000f;
                    curHero.unitLevel += 1f;
                    curHero.unitDamage += 5;
                    break;

                case 5:
                    curHero.unitUpgradeCost += 1000f;
                    curHero.unitLevel += 1f;
                    curHero.unitDamage += 5;
                    break;

                case 6:
                    curHero.unitUpgradeCost += 1000f;
                    curHero.unitLevel += 1f;
                    curHero.unitDamage += 5;
                    break;

                case 7:
                    curHero.unitUpgradeCost += 1000f;
                    curHero.unitLevel += 1f;
                    curHero.unitDamage += 5;
                    break;

                case 8:
                    curHero.unitUpgradeCost += 1000f;
                    curHero.unitLevel += 1f;
                    curHero.unitDamage += 5;
                    break;

                case 9:
                    curHero.unitUpgradeCost += 1000f;
                    curHero.unitLevel += 1f;
                    curHero.unitDamage += 5;
                    break;

                case 10:
                    curHero.unitUpgradeCost += 1000f;
                    curHero.unitLevel += 1f;
                    curHero.unitDamage += 5;
                    break;

                case 11:
                    curHero.unitUpgradeCost += 1000f;
                    curHero.unitLevel += 1f;
                    curHero.unitDamage += 5;
                    break;

                case 12:
                    curHero.unitUpgradeCost += 1000f;
                    curHero.unitLevel += 1f;
                    curHero.unitDamage += 5;
                    break;

                case 13:
                    curHero.unitUpgradeCost += 1000f;
                    curHero.unitLevel += 1f;
                    curHero.unitDamage += 5;
                    break;

                case 14:
                    curHero.unitUpgradeCost += 1000f;
                    curHero.unitLevel += 1f;
                    curHero.unitDamage += 5;
                    break;

                case 15:
                    break;
            }
            heroUpdate2();
        }
    }

}

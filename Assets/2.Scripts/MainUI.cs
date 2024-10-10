using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    [SerializeField] MainBtn btn;

    [Header("메인")]
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI gemText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] Slider expSlider;

    [Header("유물")]
    [SerializeField] ItemState curItems;
    [SerializeField] GameObject treasureDcPanel;
    [SerializeField] TextMeshProUGUI treasureNameText;
    [SerializeField] TextMeshProUGUI treasureLevelText;
    [SerializeField] TextMeshProUGUI treasureExpText;
    [SerializeField] TextMeshProUGUI treasureUpgradeText;
    [SerializeField] TextMeshProUGUI treasureCostText;
    [SerializeField] Image treasureImg;
    [SerializeField] Image treasureFill;
    [SerializeField] List<Image> treasureSlider = new List<Image>();
    [SerializeField] List<TextMeshProUGUI> treasureLevel = new List<TextMeshProUGUI>();
    [SerializeField] List<TextMeshProUGUI> treasureExp = new List<TextMeshProUGUI>();

    [Header("영웅")]
    [SerializeField] UnitState curHero;
    [SerializeField] GameObject heroDcPanel;
    [SerializeField] TextMeshProUGUI heroNameText;
    [SerializeField] TextMeshProUGUI heroLevelText;
    [SerializeField] TextMeshProUGUI heroDamageText;
    [SerializeField] TextMeshProUGUI heroAttackSpeedText;
    [SerializeField] TextMeshProUGUI heroSkillNameText1;
    [SerializeField] TextMeshProUGUI heroSkillNameText2;
    [SerializeField] TextMeshProUGUI heroUpgradeCost;
    [SerializeField] Image heroImg;
    [SerializeField] Image heroSlider;
    [SerializeField] TextMeshProUGUI heroSliderText;

    [Header("상점")]
    [SerializeField] UnitState curUnit;
    [SerializeField] ItemState curItem;
    [SerializeField] Button[] storeUnitBtn;
    [SerializeField] UnitData[] storeUnits;
    [SerializeField] TreasureData[] storeItems;
    [SerializeField] Image[] storeUnitsImg;
    [SerializeField] TextMeshProUGUI[] storeUnitsNameText;
    [SerializeField] TextMeshProUGUI[] storeUnitsCostText;
    [SerializeField] GameObject storeUnitDcPanel;
    [SerializeField] Image storeUnitsDcImg;
    [SerializeField] TextMeshProUGUI storeUnitsDcCost;
    bool clickUnit;

    private void Start()
    {
        storeSlotReset();

        treasureUpdate1();

        DataManager.instance.saveData();
    }

    private void Update()
    {
        main();

        if (Input.GetKeyDown(KeyCode.G))
        {
            DataManager.instance.playerdata.gold += 10000;
        }
    }

    private void showPanelOpen(GameObject targetPanel)
    {
        if (targetPanel.activeSelf == false)
        {
            targetPanel.SetActive(true);
        }
        targetPanel.transform.localScale = Vector3.zero;
        targetPanel.transform.DOScale(Vector3.one, 0.2f);
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
        curItems = DataManager.instance.curItem(index);

        treasureUpdate2();

        showPanelOpen(treasureDcPanel);
    }

    private void treasureUpdate1()
    {
        for (int i = 0; i < treasureSlider.Count; i++)
        {
            curItems = DataManager.instance.curItem(i);

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
        treasureImg.sprite = curItems.treasureImg;
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
                    curItems.treasureCurExp -= curItems.treasureMaxExp;
                    curItems.treasureMaxExp++;
                    curItems.treasureBase += curItems.treasureUpgrade;
                    break;

                case 2:
                    curItems.treasureCost = 3000;
                    curItems.treasureLevel = 3;
                    curItems.treasureCurExp -= curItems.treasureMaxExp;
                    curItems.treasureMaxExp++;
                    curItems.treasureBase += curItems.treasureUpgrade;
                    break;

                case 3:
                    curItems.treasureCost = 4000;
                    curItems.treasureLevel = 4;
                    curItems.treasureCurExp -= curItems.treasureMaxExp;
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
        curHero = DataManager.instance.curUnit(index);

        heroUpdate2();

        showPanelOpen(heroDcPanel);
    }

    private void heroUpdate2()
    {
        heroImg.sprite = curHero.unitImg;
        heroNameText.text = curHero.unitName;
        heroLevelText.text = "LV." + curHero.unitLevel.ToString();
        heroDamageText.text = curHero.unitDamage.ToString();
        heroAttackSpeedText.text = curHero.attackSpeed.ToString();
        heroUpgradeCost.text = curHero.unitUpgradeCost.ToString();
        heroSkillNameText1.text =
            $"<u><color=yellow>[{curHero.skill1Name}]</color></u> 스킬이 해금됩니다";
        heroSkillNameText2.text =
            $"<u><color=yellow>[{curHero.skill1Name}]</color></u> 스킬의 발동확률이 +80% 증가합니다";
        heroSlider.fillAmount = curHero.unitCurExp / curHero.unitMaxExp;
        heroSliderText.text = $"{curHero.unitCurExp} / {curHero.unitMaxExp}";
    }

    public void HeroUpgrade()
    {
        if (curHero.unitUpgradeCost < DataManager.instance.playerdata.gold
            && curHero.unitMaxExp <= curHero.unitCurExp)
        {
            switch (curHero.unitLevel)
            {
                case 1:
                    curHero.unitUpgradeCost += 1000f;
                    curHero.unitLevel += 1f;
                    curHero.unitDamage += 5;
                    curHero.unitCurExp -= curHero.unitMaxExp;
                    curHero.unitMaxExp += 1f;
                    break;

                case 2:
                    curHero.unitUpgradeCost += 1000f;
                    curHero.unitLevel += 1f;
                    curHero.unitDamage += 5;
                    curHero.unitCurExp -= curHero.unitMaxExp;
                    curHero.unitMaxExp += 1f;
                    break;

                case 3:
                    curHero.unitUpgradeCost += 1000f;
                    curHero.unitLevel += 1f;
                    curHero.unitDamage += 5;
                    curHero.unitCurExp -= curHero.unitMaxExp;
                    curHero.unitMaxExp += 1f;
                    break;

                case 4:
                    curHero.unitUpgradeCost += 1000f;
                    curHero.unitLevel += 1f;
                    curHero.unitDamage += 5;
                    curHero.unitCurExp -= curHero.unitMaxExp;
                    curHero.unitMaxExp += 1f;
                    break;

                case 5:
                    curHero.unitUpgradeCost += 1000f;
                    curHero.unitLevel += 1f;
                    curHero.unitDamage += 5;
                    curHero.unitCurExp -= curHero.unitMaxExp;
                    curHero.unitMaxExp += 1f;
                    break;

                case 6:
                    curHero.unitUpgradeCost += 1000f;
                    curHero.unitLevel += 1f;
                    curHero.unitDamage += 5;
                    curHero.unitCurExp -= curHero.unitMaxExp;
                    curHero.unitMaxExp += 1f;
                    break;

                case 7:
                    curHero.unitUpgradeCost += 1000f;
                    curHero.unitLevel += 1f;
                    curHero.unitDamage += 5;
                    curHero.unitCurExp -= curHero.unitMaxExp;
                    curHero.unitMaxExp += 1f;
                    break;

                case 8:
                    curHero.unitUpgradeCost += 1000f;
                    curHero.unitLevel += 1f;
                    curHero.unitDamage += 5;
                    curHero.unitCurExp -= curHero.unitMaxExp;
                    curHero.unitMaxExp += 1f;
                    break;

                case 9:
                    curHero.unitUpgradeCost += 1000f;
                    curHero.unitLevel += 1f;
                    curHero.unitDamage += 5;
                    curHero.unitCurExp -= curHero.unitMaxExp;
                    curHero.unitMaxExp += 1f;

                    break;
                case 10:
                    curHero.unitUpgradeCost += 1000f;
                    curHero.unitLevel += 1f;
                    curHero.unitDamage += 5;
                    curHero.unitCurExp -= curHero.unitMaxExp;
                    curHero.unitMaxExp += 1f;
                    break;

                case 11:
                    curHero.unitUpgradeCost += 1000f;
                    curHero.unitLevel += 1f;
                    curHero.unitDamage += 5;
                    curHero.unitCurExp -= curHero.unitMaxExp;
                    curHero.unitMaxExp += 1f;
                    break;

                case 12:
                    curHero.unitUpgradeCost += 1000f;
                    curHero.unitLevel += 1f;
                    curHero.unitDamage += 5;
                    curHero.unitCurExp -= curHero.unitMaxExp;
                    curHero.unitMaxExp += 1f;
                    break;

                case 13:
                    curHero.unitUpgradeCost += 1000f;
                    curHero.unitLevel += 1f;
                    curHero.unitDamage += 5;
                    curHero.unitCurExp -= curHero.unitMaxExp;
                    curHero.unitMaxExp += 1f;
                    break;

                case 14:
                    curHero.unitUpgradeCost += 1000f;
                    curHero.unitLevel += 1f;
                    curHero.unitDamage += 5;
                    curHero.unitCurExp -= curHero.unitMaxExp;
                    curHero.unitMaxExp += 1f;
                    break;

                case 15:
                    break;
            }
            heroUpdate2();
        }
    }

    public void storeSlotClick(int index, string Name)
    {
        if (Name == "Unit")
        {
            curUnit = DataManager.instance.curUnit(index);
            clickUnit = true;

            storeUnitsDcImg.sprite = curUnit.unitImg;
            storeUnitsDcCost.text = curUnit.unitStoreCost.ToString();

            showPanelOpen(storeUnitDcPanel);

        }
        else if (Name == "Treasure")
        {
            curItem = DataManager.instance.curItem(index);
            clickUnit = false;

            storeUnitsDcImg.sprite = curItem.treasureImg;
            storeUnitsDcCost.text = curItem.storeCost.ToString();

            showPanelOpen(storeUnitDcPanel);
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
                    (() => btn.storeUnitBtn(index, Name));

            }
            else if (or == 1)
            {
                int slotRand = UnityEngine.Random.Range(0, treasureSlider.Count);
                int curItemIndex = storeItems[slotRand].index;

                storeUnitsImg[i].sprite = storeItems[slotRand].treasureImg;
                storeUnitsNameText[i].text = storeItems[slotRand].treasureName;
                storeUnitsCostText[i].text = storeItems[slotRand].storeCost.ToString();

                storeUnitBtn[i].GetComponent<Button>().onClick.RemoveAllListeners();

                int index = curItemIndex;
                string Name = "Treasure";
                storeUnitBtn[i].GetComponent<Button>().onClick.AddListener
                    (() => btn.storeUnitBtn(index, Name));
            }
        }
    }

    public void storeBuy()
    {
        if (clickUnit == true)
        {
            if (DataManager.instance.playerdata.gold >= curUnit.unitStoreCost)
            {
                DataManager.instance.playerdata.gold -= curUnit.unitStoreCost;
                curUnit.unitCurExp += 1f;
                storeUnitDcPanel.SetActive(false);
            }
        }

        else
        {
            if (DataManager.instance.playerdata.gold >= curItem.storeCost)
            {
                DataManager.instance.playerdata.gold -= curItem.storeCost;
                curItem.treasureCurExp += 1;
                treasureUpdate1();
                storeUnitDcPanel.SetActive(false);

            }
        }
    }
}

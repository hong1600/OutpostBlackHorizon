using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitUI : MonoBehaviour
{
    public MainUI mainUI;

    public UnitState curHero;
    public GameObject heroDcPanel;
    public TextMeshProUGUI heroNameText;
    public TextMeshProUGUI heroLevelText;
    public TextMeshProUGUI heroDamageText;
    public TextMeshProUGUI heroAttackSpeedText;
    public TextMeshProUGUI heroSkillNameText1;
    public TextMeshProUGUI heroSkillNameText2;
    public TextMeshProUGUI heroUpgradeCost;
    public Image heroImg;
    public Image heroSlider;
    public TextMeshProUGUI heroSliderText;

    public void heroDc(int index)
    {
        curHero = DataManager.instance.playerDataMng.getUnit(index);

        heroUpdate2();

        mainUI.showPanelOpen(heroDcPanel);
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
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroSlot : MonoBehaviour
{
    public UnitUI unitUI;

    public Image heroImg;
    public TextMeshProUGUI heroNameText;
    public UnitData curHero;
    public int heroNum;

    public void setHero(UnitData unit)
    {
        curHero = unit;
        heroImg.sprite = unit.unitImg;
        heroNameText.text = unit.unitName;
        heroNum = unit.index;
    }

    public void heroDcBtn(int num)
    {
        num = heroNum;

        unitUI.heroDc(num);
    }
}

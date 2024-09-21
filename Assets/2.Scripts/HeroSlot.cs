using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HeroSlot : MonoBehaviour
{
    [SerializeField] Image heroImg;
    [SerializeField] TextMeshProUGUI heroNameText;
    [SerializeField] TextMeshProUGUI heroLevelText;

    [SerializeField] UnitData curHero;

    public void setHero(UnitData unit)
    {
        curHero = unit;
        heroImg.sprite = unit.unitImg;
        heroNameText.text = unit.unitName;
        heroLevelText.text = "Lv." + unit.unitLevel.ToString();
    }
}

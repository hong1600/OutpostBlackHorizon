using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroSlot : MonoBehaviour
{
    [SerializeField] Image heroImg;
    [SerializeField] TextMeshProUGUI heroNameText;
    [SerializeField] UnitData curHero;
    public int heroNum;

    [SerializeField] MainUI mainUI;

    private void Start()
    {
        if(mainUI == null) 
        {
            mainUI = GameObject.Find("MainUI").GetComponent<MainUI>();
        }
    }

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

        mainUI.heroDc(num);
    }
}

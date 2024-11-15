using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MixSlot : MonoBehaviour
{
    public MixUI mixUI;
    public UnitMixer unitMixer;

    public Image unitImg;
    public TextMeshProUGUI unitNameText;
    int num;


    public void setUnit(UnitData unitdata, int index)
    {
        unitImg.sprite = unitdata.unitImg;
        unitNameText.text = unitdata.unitName;
        num = index;
    }

    public void MixBtn()
    {
        mixUI.curMixPanel(num);
        GameInventoryManager.Instance.loadRightUnit(num);
        unitMixer.unitCanMix();
    }
}

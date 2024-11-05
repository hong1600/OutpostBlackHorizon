using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MixSlot : MonoBehaviour
{
    [SerializeField] Image unitImg;
    [SerializeField] TextMeshProUGUI unitNameText;
    int num;


    public void setUnit(UnitData unitdata, int index)
    {
        unitImg.sprite = unitdata.unitImg;
        unitNameText.text = unitdata.unitName;
        num = index;
    }

    public void MixBtn()
    {
        GameUI.instance.curMixPanel(num);
        GameInventoryManager.Instance.loadRightUnit(num);
        GameManager.Instance.unitMng.unitMixer.unitCanMix();
    }
}

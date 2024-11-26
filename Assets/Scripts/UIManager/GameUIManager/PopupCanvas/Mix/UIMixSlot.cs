using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMixSlot : MonoBehaviour
{
    public Image unitImg;
    public TextMeshProUGUI unitNameText;
    public int num;

    public void setUnit(UnitData unitdata, int index)
    {
        unitImg.sprite = unitdata.unitImg;
        unitNameText.text = unitdata.unitName;
        num = index;
    }
}

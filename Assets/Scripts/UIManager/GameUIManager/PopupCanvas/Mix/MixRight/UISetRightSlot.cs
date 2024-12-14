using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISetRightSlot : MonoBehaviour
{
    public Image unitImg;
    public TextMeshProUGUI unitName;

    public void setUnit(UnitData unitdata)
    {
        unitName.text = unitdata.unitName;
        unitImg.sprite = unitdata.unitImg;
    }
}

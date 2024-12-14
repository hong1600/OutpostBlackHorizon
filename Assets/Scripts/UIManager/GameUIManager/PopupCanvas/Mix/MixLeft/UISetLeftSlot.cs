using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISetLeftSlot : MonoBehaviour
{
    public Image unitImg;
    public int num;

    public void setUnit(UnitData unitdata, int index)
    {
        unitImg.sprite = unitdata.unitImg;
        num = index;
    }
}

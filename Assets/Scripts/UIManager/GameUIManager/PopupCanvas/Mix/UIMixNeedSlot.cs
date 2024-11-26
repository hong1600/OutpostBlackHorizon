using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMixNeedSlot : MonoBehaviour
{
    public Image UnitImg;
    public TextMeshProUGUI haveText;

    public void setUnit(UnitData unitdata)
    {
        UnitImg.sprite = unitdata.unitImg;
    }
}

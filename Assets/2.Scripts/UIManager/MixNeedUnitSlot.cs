using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MixNeedUnitSlot : MonoBehaviour
{
    [SerializeField] Image UnitImg;
    [SerializeField] TextMeshProUGUI haveText;

    public void setUnit(UnitData unitdata)
    {
        UnitImg.sprite = unitdata.unitImg;
    }
}

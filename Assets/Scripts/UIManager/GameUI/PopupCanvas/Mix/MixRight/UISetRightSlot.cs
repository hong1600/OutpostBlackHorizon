using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISetRightSlot : MonoBehaviour
{
    public Image unitImg;
    public TextMeshProUGUI unitName;

    public void SetUnit(UnitData _unitdata)
    {
        unitName.text = _unitdata.unitName;
        unitImg.sprite = _unitdata.unitImg;
    }
}

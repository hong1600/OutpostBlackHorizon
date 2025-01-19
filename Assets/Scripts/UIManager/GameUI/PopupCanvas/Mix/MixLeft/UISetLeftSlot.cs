using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISetLeftSlot : MonoBehaviour
{
    [SerializeField] Image unitImg;
    [SerializeField] int num;

    public void SetUnit(UnitData _unitdata, int _index)
    {
        unitImg.sprite = _unitdata.unitImg;
        num = _index;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMixRightPanel : MonoBehaviour
{
    public TextMeshProUGUI mixUnitNameText;
    public Image mixUnitImg;
    public List<UnitData> mixUnitDataList = new List<UnitData>();

    private void Start()
    {
        mixUnitDataList = DataMng.instance.unitDataLoader.GetUnitData(EUnitGrade.SS);

        UnitData curUnit = mixUnitDataList[0];
        mixUnitNameText.text = mixUnitDataList[0].unitName;
        mixUnitImg.sprite = mixUnitDataList[0].unitImg;
    }

    public void CurMixPanel(int _index)
    {
        UnitData curUnit = mixUnitDataList[_index];
        mixUnitNameText.text = mixUnitDataList[_index].unitName;
        mixUnitImg.sprite = mixUnitDataList[_index].unitImg;
    }
}

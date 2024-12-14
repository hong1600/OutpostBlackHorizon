using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMixRightPanel : MonoBehaviour
{
    public TextMeshProUGUI mixUnitNameText;
    public Image mixUnitImg;
    public List<UnitData> mixUnitData = new List<UnitData>();

    private void Start()
    {
        mixUnitData = DataManager.instance.unitDataMng.getUnitData(EUnitGrade.SS);

        UnitData curUnit = mixUnitData[0];
        mixUnitNameText.text = mixUnitData[0].unitName;
        mixUnitImg.sprite = mixUnitData[0].unitImg;
    }

    public void curMixPanel(int index)
    {
        UnitData curUnit = mixUnitData[index];
        mixUnitNameText.text = mixUnitData[index].unitName;
        mixUnitImg.sprite = mixUnitData[index].unitImg;
    }
}

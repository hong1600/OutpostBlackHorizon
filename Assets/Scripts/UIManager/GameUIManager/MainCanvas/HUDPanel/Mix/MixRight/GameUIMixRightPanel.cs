using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIMixRightPanel : MonoBehaviour
{
    public TextMeshProUGUI mixUnitNameText;
    public Image mixUnitImg1;
    public Image mixUnitImg2;
    public UnitData[] mixUnitData;

    private void Start()
    {
        UnitData curUnit = mixUnitData[0];
        mixUnitNameText.text = mixUnitData[0].unitName;
        mixUnitImg1.sprite = mixUnitData[0].unitImg;
        mixUnitImg2.sprite = mixUnitData[0].unitImg;
    }

    public void curMixPanel(int index)
    {
        UnitData curUnit = mixUnitData[index];
        mixUnitNameText.text = mixUnitData[index].unitName;
        mixUnitImg1.sprite = mixUnitData[index].unitImg;
        mixUnitImg2.sprite = mixUnitData[index].unitImg;
    }
}

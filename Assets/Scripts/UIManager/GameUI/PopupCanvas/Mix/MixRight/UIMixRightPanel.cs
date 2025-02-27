using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMixRightPanel : MonoBehaviour
{
    [SerializeField] List<UnitData> mixUnitDataList = new List<UnitData>();

    private void Start()
    {
        mixUnitDataList = DataMng.instance.UnitDataLoader.GetUnitByGradeData(EUnitGrade.SS);

        UnitData curUnit = mixUnitDataList[0];
    }

    private void CurMixPanel(int _index)
    {
        UnitData curUnit = mixUnitDataList[_index];
    }
}

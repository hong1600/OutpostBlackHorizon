using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMixRightPanel : MonoBehaviour
{
    [SerializeField] List<TableUnit.Info> mixUnitDataList = new List<TableUnit.Info>();

    private void Start()
    {
        mixUnitDataList = DataMng.instance.TableUnit.GetUnitByGradeData(EUnitGrade.SS);

        TableUnit.Info curUnit = mixUnitDataList[0];
    }

    private void CurMixPanel(int _index)
    {
        TableUnit.Info curUnit = mixUnitDataList[_index];
    }
}

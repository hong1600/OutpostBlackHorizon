using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMixRightSlot : MonoBehaviour
{
    [SerializeField] Transform mixRightContent;
    [SerializeField] int curMixUnit;

    [SerializeField] GameObject[] unitPanels;
    Dictionary<EUnitGrade, GameObject> unitPanelDic = new Dictionary<EUnitGrade, GameObject>();

    [SerializeField] List<TableUnit.Info> unitList = new List<TableUnit.Info>();


    private void Start()
    {
        unitPanelDic[EUnitGrade.C] = unitPanels[0];
        unitPanelDic[EUnitGrade.B] = unitPanels[1];
        unitPanelDic[EUnitGrade.A] = unitPanels[2];
        unitPanelDic[EUnitGrade.S] = unitPanels[3];

        LoadRightUnit(0);
    }

    private void LoadRightUnit(int _num)
    {
        int childCount = mixRightContent.childCount;

        for (int i = childCount - 1; i >= 0; i--)
        {
            Destroy(mixRightContent.GetChild(i).gameObject);
        }

        unitList.Clear();
        curMixUnit = _num;

        List<TableUnit.Info> curUnitList = DataMng.instance.TableUnit.GetUnitByGradeData(EUnitGrade.SS);

        if (_num >= curUnitList.Count) return;

        TableUnit.Info unitData = curUnitList[_num];
        TableUnit.Info[] mixUnits = unitData.mixNeedUnits;

        int j = 0;
        while (j < mixUnits.Length)
        {
            TableUnit.Info unit = mixUnits[j];

            if (unitPanelDic.ContainsKey(unit.Grade))
            {
                GameObject panel = Instantiate(unitPanelDic[0]);
                GameObject newSlot = Instantiate(panel, mixRightContent);
                UISetRightSlot setRightSlot = newSlot.GetComponent<UISetRightSlot>();
                setRightSlot.SetUnit(unit);
                unitList.Add(unit);
            }
            j++;
        }
    }

    public List<TableUnit.Info> GetUnitList()
    {
        return unitList;
    }
    public int GetCurMixUnit()
    {
        return curMixUnit;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMixRightSlot : MonoBehaviour
{
    [SerializeField] Transform mixRightContent;
    [SerializeField] int curMixUnit;

    [SerializeField] GameObject unitPanel;

    [SerializeField] List<TableUnit.Info> unitList = new List<TableUnit.Info>();

    [SerializeField] List<TableUnit.Info> unitDataList = new List<TableUnit.Info>();


    private void Start()
    {
        unitDataList.Add(DataManager.instance.TableUnit.GetUnitData(101));
        unitDataList.Add(DataManager.instance.TableUnit.GetUnitData(102));
        unitDataList.Add(DataManager.instance.TableUnit.GetUnitData(103));

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

        List<TableUnit.Info> curUnitList = DataManager.instance.TableUnit.GetUnitByGradeData(EUnitGrade.SS);

        if (_num >= curUnitList.Count) return;

        TableUnit.Info unitData = curUnitList[_num];

        TableUnit.Info[] mixUnits = unitData.mixNeedUnits;

        int j = 0;
        while (j < unitDataList.Count)
        {
            TableUnit.Info unit = unitDataList[j];

            GameObject newSlot = Instantiate(unitPanel, mixRightContent);
            UISetRightSlot setRightSlot = newSlot.GetComponent<UISetRightSlot>();
            setRightSlot.SetUnit(unit);
            unitList.Add(unit);

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

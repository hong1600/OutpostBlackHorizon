using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iUIMixRightSlot
{
    List<UnitData> getNeedUnitList();
    int getCurMixUnit();
}

public class UIMixRightSlot : MonoBehaviour, iUIMixRightSlot
{
    public UnitDataMng unitDataMng;
    public IUnitDataMng iUnitDataMng;

    public Transform mixRightContent;
    public GameObject[] mixPanelPre;
    public int curMixUnit;
    public List<UnitData> needUnitList = new List<UnitData>();

    private void Awake()
    {
        iUnitDataMng = unitDataMng;
        loadRightUnit(0);
    }

    public void loadRightUnit(int num)
    {
        foreach (Transform child in mixRightContent)
        {
            Destroy(child.gameObject);
        }

        needUnitList.Clear();

        curMixUnit = num;

        for (int i = 0; i < iUnitDataMng.getUnitData(EUnitGrade.SS)[num].mixUnit.Length; i++)
        {
            switch (iUnitDataMng.getUnitData(EUnitGrade.SS)[num].mixUnit[i].unitGrade)
            {
                case EUnitGrade.C:
                    GameObject newslot0 = Instantiate(mixPanelPre[0], mixRightContent);
                    UIMixSlot mNeed0 = newslot0.GetComponent<UIMixSlot>();
                    mNeed0.setUnit(iUnitDataMng.getUnitData(EUnitGrade.SS)[num].mixUnit[i], i);
                    needUnitList.Add(iUnitDataMng.getUnitData(EUnitGrade.SS)[num].mixUnit[i]);
                    break;
                case EUnitGrade.B:
                    GameObject newslot1 = Instantiate(mixPanelPre[1], mixRightContent);
                    UIMixSlot mNeed1 = newslot1.GetComponent<UIMixSlot>();
                    mNeed1.setUnit(iUnitDataMng.getUnitData(EUnitGrade.SS)[num].mixUnit[i], i);
                    needUnitList.Add(iUnitDataMng.getUnitData(EUnitGrade.SS)[num].mixUnit[i]);
                    break;
                case EUnitGrade.A:
                    GameObject newslot2 = Instantiate(mixPanelPre[2], mixRightContent);
                    UIMixSlot mNeed2 = newslot2.GetComponent<UIMixSlot>();
                    mNeed2.setUnit(iUnitDataMng.getUnitData(EUnitGrade.SS)[num].mixUnit[i], i);
                    needUnitList.Add(iUnitDataMng.getUnitData(EUnitGrade.SS)[num].mixUnit[i]);
                    break;
                case EUnitGrade.S:
                    GameObject newslot3 = Instantiate(mixPanelPre[3], mixRightContent);
                    UIMixSlot mNeed3 = newslot3.GetComponent<UIMixSlot>();
                    mNeed3.setUnit(iUnitDataMng.getUnitData(EUnitGrade.SS)[num].mixUnit[i], i);
                    needUnitList.Add(iUnitDataMng.getUnitData(EUnitGrade.SS)[num].mixUnit[i]);
                    break;
            }
        }
    }

    public List<UnitData> getNeedUnitList() { return needUnitList; }
    public int getCurMixUnit() { return curMixUnit; }
}

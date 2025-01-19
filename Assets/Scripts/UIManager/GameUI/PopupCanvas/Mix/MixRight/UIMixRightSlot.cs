using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUIMixRightSlot
{
    List<UnitData> GetSacUnitList();
    int GetCurMixUnit();
}

public class UIMixRightSlot : MonoBehaviour, IUIMixRightSlot
{
    [SerializeField] Transform mixRightContent;
    [SerializeField] int curMixUnit;
    [SerializeField] GameObject[] sacUnitPanels;
    [SerializeField] List<UnitData> sacUnitList = new List<UnitData>();

    private void Start()
    {
        LoadRightUnit(0);
    }

    private void LoadRightUnit(int _num)
    {
        foreach (Transform child in mixRightContent)
        {
            Destroy(child.gameObject);
        }

        sacUnitList.Clear();

        curMixUnit = _num;

        for (int i = 0; i < DataMng.instance.unitDataLoader.GetUnitData(EUnitGrade.SS)[_num].mixUnit.Length; i++)
        {
            switch (DataMng.instance.unitDataLoader.GetUnitData(EUnitGrade.SS)[_num].mixUnit[i].unitGrade)
            {
                case EUnitGrade.C:
                    GameObject newslot0 = Instantiate(sacUnitPanels[0], mixRightContent);
                    UISetRightSlot mNeed0 = newslot0.GetComponent<UISetRightSlot>();
                    mNeed0.SetUnit(DataMng.instance.unitDataLoader.GetUnitData(EUnitGrade.SS)[_num].mixUnit[i]);
                    sacUnitList.Add(DataMng.instance.unitDataLoader.GetUnitData(EUnitGrade.SS)[_num].mixUnit[i]);
                    break;
                case EUnitGrade.B:
                    GameObject newslot1 = Instantiate(sacUnitPanels[1], mixRightContent);
                    UISetRightSlot mNeed1 = newslot1.GetComponent<UISetRightSlot>();
                    mNeed1.SetUnit(DataMng.instance.unitDataLoader.GetUnitData(EUnitGrade.SS)[_num].mixUnit[i]);
                    sacUnitList.Add(DataMng.instance.unitDataLoader.GetUnitData(EUnitGrade.SS)[_num].mixUnit[i]);
                    break;
                case EUnitGrade.A:
                    GameObject newslot2 = Instantiate(sacUnitPanels[2], mixRightContent);
                    UISetRightSlot mNeed2 = newslot2.GetComponent<UISetRightSlot>();
                    mNeed2.SetUnit(DataMng.instance.unitDataLoader.GetUnitData(EUnitGrade.SS)[_num].mixUnit[i]);
                    sacUnitList.Add(DataMng.instance.unitDataLoader.GetUnitData(EUnitGrade.SS)[_num].mixUnit[i]);
                    break;
                case EUnitGrade.S:
                    GameObject newslot3 = Instantiate(sacUnitPanels[3], mixRightContent);
                    UISetRightSlot mNeed3 = newslot3.GetComponent<UISetRightSlot>();
                    mNeed3.SetUnit(DataMng.instance.unitDataLoader.GetUnitData(EUnitGrade.SS)[_num].mixUnit[i]);
                    sacUnitList.Add(DataMng.instance.unitDataLoader.GetUnitData(EUnitGrade.SS)[_num].mixUnit[i]);
                    break;
            }
        }
    }

    public List<UnitData> GetSacUnitList() { return sacUnitList; }
    public int GetCurMixUnit() { return curMixUnit; }
}

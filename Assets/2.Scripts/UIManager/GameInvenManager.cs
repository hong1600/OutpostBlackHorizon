using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInventoryManager : MonoBehaviour
{
    public static GameInventoryManager Instance;

    [SerializeField] Transform mixLeftContent;
    [SerializeField] GameObject MixBtnPre;
    [SerializeField] UnitData[] mixUnitData;

    [SerializeField] Transform mixRightContent;
    [SerializeField] GameObject[] mixPanelPre;

    public int curMixUnit;

    public List<UnitData> needUnitList = new List<UnitData>();

    private void Start()
    {
        loadLeftUnit();
        loadRightUnit(0);
    }

    private void loadLeftUnit()
    {
        for (int i = 0; i < mixUnitData.Length; i++)
        {
            GameObject newslot = Instantiate(MixBtnPre, mixLeftContent);
            MixSlot mixslot = newslot.GetComponent<MixSlot>();
            mixslot.setUnit(mixUnitData[i], i);
        }
    }

    public void loadRightUnit(int num)
    {
        foreach (Transform child in mixRightContent) 
        {
            Destroy(child.gameObject);
        }

        needUnitList.Clear();

        curMixUnit = num;

        for (int i = 0; i < mixUnitData[num].mixUnit.Length; i++)
        {
            switch (mixUnitData[num].mixUnit[i].unitGrade)
            {
                case 0:
                    GameObject newslot0 = Instantiate(mixPanelPre[0], mixRightContent);
                    MixNeedUnitSlot mNeed0 = newslot0.GetComponent<MixNeedUnitSlot>();
                    mNeed0.setUnit(mixUnitData[num].mixUnit[i]);
                    needUnitList.Add(mixUnitData[num].mixUnit[i]);
                    break;
                case 1:
                    GameObject newslot1 = Instantiate(mixPanelPre[1], mixRightContent);
                    MixNeedUnitSlot mNeed1 = newslot1.GetComponent<MixNeedUnitSlot>();
                    mNeed1.setUnit(mixUnitData[num].mixUnit[i]);
                    needUnitList.Add(mixUnitData[num].mixUnit[i]);
                    break;
                case 2:
                    GameObject newslot2 = Instantiate(mixPanelPre[2], mixRightContent);
                    MixNeedUnitSlot mNeed2 = newslot2.GetComponent<MixNeedUnitSlot>();
                    mNeed2.setUnit(mixUnitData[num].mixUnit[i]);
                    needUnitList.Add(mixUnitData[num].mixUnit[i]);
                    break;
                case 3:
                    GameObject newslot3 = Instantiate(mixPanelPre[3], mixRightContent);
                    MixNeedUnitSlot mNeed3 = newslot3.GetComponent<MixNeedUnitSlot>();
                    mNeed3.setUnit(mixUnitData[num].mixUnit[i]);
                    needUnitList.Add(mixUnitData[num].mixUnit[i]);
                    break;
            }
        }
    }
}

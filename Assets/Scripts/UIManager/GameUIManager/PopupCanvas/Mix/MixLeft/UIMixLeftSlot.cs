using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMixLeftSlot : MonoBehaviour
{
    public UnitDataMng unitDataMng;
    public IUnitDataMng iUnitDataMng;

    public Transform mixLeftContent;
    public GameObject MixBtnPre;


    private void Awake()
    {
        iUnitDataMng = unitDataMng;
        loadLeftUnit();
    }

    private void loadLeftUnit()
    {
        for (int i = 0; i < iUnitDataMng.getUnitData(4).Count; i++)
        {
            GameObject newslot = Instantiate(MixBtnPre, mixLeftContent);
            UIMixSlot mixslot = newslot.GetComponent<UIMixSlot>();
            mixslot.setUnit(iUnitDataMng.getUnitData(4)[i], i);
        }
    }
}

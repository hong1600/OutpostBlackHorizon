using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMixLeftPanel : MonoBehaviour
{
    public IUnitDataMng iUnitDataMng;

    public Transform mixLeftContent;
    public GameObject MixBtnPre;


    private void Awake()
    {
        iUnitDataMng = DataManager.instance.unitDataMng;
        loadLeftPanel();
    }

    private void loadLeftPanel()
    {
        for (int i = 0; i < iUnitDataMng.getUnitData(EUnitGrade.SS).Count; i++)
        {
            GameObject newBtn = Instantiate(MixBtnPre, mixLeftContent);
            UISetLeftSlot mixslot = newBtn.GetComponent<UISetLeftSlot>();
            mixslot.setUnit(iUnitDataMng.getUnitData(EUnitGrade.SS)[i], i);
        }
    }
}

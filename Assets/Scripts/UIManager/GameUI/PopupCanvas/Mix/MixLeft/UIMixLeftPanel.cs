using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMixLeftPanel : MonoBehaviour
{
    [SerializeField] Transform mixLeftContent;
    [SerializeField] GameObject MixBtnPre;

    private void Awake()
    {
        LoadLeftPanel();
    }

    private void LoadLeftPanel()
    {
        for (int i = 0; i < DataMng.instance.TableUnit.GetUnitByGradeData(EUnitGrade.SS).Count; i++)
        {
            GameObject newBtn = Instantiate(MixBtnPre, mixLeftContent);
            UISetLeftSlot mixslot = newBtn.GetComponent<UISetLeftSlot>();
            mixslot.SetUnit(DataMng.instance.TableUnit.GetUnitByGradeData(EUnitGrade.SS)[i], i);
        }
    }
}

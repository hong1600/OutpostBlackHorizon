using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUIFusionBtn
{
    void showFusionBtn(Vector3 fieldPos);
    void hideFusionBtn();
}

public class UIFusionBtn : MonoBehaviour, IUIFusionBtn
{
    public FieldSelector fieldSelector;
    public IFieldSelector iFieldSelector;
    public UnitFusion unitFusion;
    public IUnitFusion iUnitFusion;

    public Vector3 offset;

    private void Awake()
    {
        iFieldSelector = fieldSelector;
        iUnitFusion = unitFusion;
        this.gameObject.SetActive(false);
    }

    public void onFusion()
    {
        if (iFieldSelector.getSelectField().transform.GetChild(0).transform.childCount == 3)
        {
            StartCoroutine(iUnitFusion.unitFusion());

            this.gameObject.SetActive(false);
        }
    }

    public void showFusionBtn(Vector3 fieldPos)
    {
        if (iFieldSelector.getSelectField().transform.GetChild(0).transform.childCount == 0) return;

        if (iFieldSelector.getSelectField().transform.GetChild(0).transform.childCount == 3
            || iFieldSelector.getSelectField().transform.GetChild(0).transform.GetChild(0).
            GetComponent<Unit>().eUnitGrade != EUnitGrade.S)
        {
            this.gameObject.transform.position = fieldPos + offset;

            Quaternion rotation = Quaternion.Euler(90f, 0f, 0f);
            this.gameObject.transform.rotation = rotation;

            this.gameObject.SetActive(true);
        }
        else return;
    }

    public void hideFusionBtn()
    {
        this.gameObject.SetActive(false);
    }
}

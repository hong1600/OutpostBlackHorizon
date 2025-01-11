using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUIFusionBtn
{
    void ShowFusionBtn(Vector3 _fieldPos);
    void HideFusionBtn();
}

public class UIFusionBtn : MonoBehaviour, IUIFusionBtn
{
    public Vector3 offset;

    private void Awake()
    {
        this.gameObject.SetActive(false);
    }

    public void ClickFusion()
    {
        if (Shared.inputMng.iFieldSelector.GetStartSelectField().transform.GetChild(0).transform.childCount == 3)
        {
            Shared.unitMng.iUnitFusion.UnitFusionSpawn();

            this.gameObject.SetActive(false);
        }
    }

    public void ShowFusionBtn(Vector3 _fieldPos)
    {
        if (Shared.inputMng.iFieldSelector.GetStartSelectField().transform.GetChild(0).transform.childCount == 0) return;

        if (Shared.inputMng.iFieldSelector.GetStartSelectField().transform.GetChild(0).transform.childCount == 3
            || Shared.inputMng.iFieldSelector.GetStartSelectField().transform.GetChild(0).transform.GetChild(0).
            GetComponent<Unit>().eUnitGrade != EUnitGrade.S)
        {
            this.gameObject.transform.position = _fieldPos + offset;

            Quaternion rotation = Quaternion.Euler(90f, 0f, 0f);
            this.gameObject.transform.rotation = rotation;

            this.gameObject.SetActive(true);
        }
        else return;
    }

    public void HideFusionBtn()
    {
        this.gameObject.SetActive(false);
    }
}

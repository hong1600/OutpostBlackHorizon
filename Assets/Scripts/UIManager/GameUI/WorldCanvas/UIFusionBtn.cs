using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFusionBtn : MonoBehaviour
{
    [SerializeField] Vector3 offset;

    private void Start()
    {
        this.gameObject.SetActive(false);
        offset = new Vector3(0, 3, -1);
    }

    public void ClickFusion()
    {
        if (InputMng.instance.iFieldSelector.GetStartSelectField().transform.GetChild(0).transform.childCount == 3)
        {
            Shared.unitMng.UnitFusion.UnitFusionSpawn();

            this.gameObject.SetActive(false);
        }
    }

    public void ShowFusionBtn(Vector3 _fieldPos)
    {
        if (InputMng.instance.iFieldSelector.GetStartSelectField().transform.GetChild(0).transform.childCount == 0) return;

        if (InputMng.instance.iFieldSelector.GetStartSelectField().transform.GetChild(0).transform.childCount == 3
            || InputMng.instance.iFieldSelector.GetStartSelectField().transform.GetChild(0).transform.GetChild(0).
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

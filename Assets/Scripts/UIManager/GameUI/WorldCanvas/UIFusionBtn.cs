using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFusionBtn : MonoBehaviour
{
    UnitFusion unitFusion;
    FieldSelector fieldSelector;

    [SerializeField] Vector3 offset;

    private void Start()
    {
        unitFusion = UnitManager.instance.UnitFusion;
        fieldSelector = InputManager.instance.fieldSelector;

        this.gameObject.SetActive(false);
        offset = new Vector3(0, 3, -1);
    }

    public void ClickFusion()
    {
        if (fieldSelector.GetStartSelectField().
            transform.GetChild(0).transform.childCount == 3)
        {
            unitFusion.UnitFusionSpawn();

            this.gameObject.SetActive(false);
        }
    }

    public void ShowFusionBtn(Vector3 _fieldPos)
    {
        if (fieldSelector.GetStartSelectField().
            transform.GetChild(0).transform.childCount == 0) return;

        if (fieldSelector.GetStartSelectField().transform.GetChild(0).transform.childCount == 3
            || fieldSelector.GetStartSelectField().transform.GetChild(0).transform.GetChild(0).
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

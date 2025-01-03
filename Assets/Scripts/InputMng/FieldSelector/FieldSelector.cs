using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFieldSelector
{
    GameObject GetSelectField();
}

public class FieldSelector : MonoBehaviour, IFieldSelector
{
    public Material originMat;
    public Material selectMat;
    public GameObject selectField;
    public MeshRenderer selectFieldRenderer;
    public GameObject fusionBtn;

    private void Start()
    {
        InputMng.onLeftClick -= FieldClick;
        InputMng.onLeftClick += FieldClick;
    }

    public void FieldClick(Vector2 _mousePosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(_mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity);
        RaycastHit closeHit = new RaycastHit();
        closeHit.distance = Mathf.Infinity;

        if (selectFieldRenderer != null) { selectFieldRenderer.material = originMat; }

        for (int i = 0; i < hits.Length; i++) 
        {
            if (hits[i].collider.gameObject.layer == LayerMask.NameToLayer("Field") 
                && hits[i].distance < closeHit.distance)
            {
                closeHit = hits[i];
            }
        }


        if (closeHit.collider != null)
        {
            selectField = null;
            selectFieldRenderer = null;
            Shared.gameUI.iUIFusionBtn.HideFusionBtn();

            selectField = closeHit.collider.gameObject;
            selectFieldRenderer = selectField.GetComponent<MeshRenderer>();
            selectFieldRenderer.material = selectMat;

            Shared.gameUI.iUIFusionBtn.ShowFusionBtn(selectField.transform.position);
        }
        else
        {
            selectFieldRenderer = null;
            Shared.gameUI.iUIFusionBtn.HideFusionBtn();
        }
    }

    public GameObject GetSelectField() { return selectField; }
}

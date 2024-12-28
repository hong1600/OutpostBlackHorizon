using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFieldSelector
{
    GameObject getSelectField();
}

public class FieldSelector : MonoBehaviour, IFieldSelector
{
    public UIFusionBtn uIFusionBtn;
    public IUIFusionBtn iUIFusionBtn;

    public MeshRenderer selectFieldRenderer;
    public Material originMat;
    public Material selectMat;

    public GameObject selectField;
    public GameObject fusionBtn;

    private void Awake()
    {
        iUIFusionBtn = uIFusionBtn;
        InputManager.onLeftClick -= fieldClick;
        InputManager.onLeftClick += fieldClick;
    }

    public void fieldClick(Vector2 mousePosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
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
            iUIFusionBtn.hideFusionBtn();

            selectField = closeHit.collider.gameObject;
            selectFieldRenderer = selectField.GetComponent<MeshRenderer>();
            selectFieldRenderer.material = selectMat;

            iUIFusionBtn.showFusionBtn(selectField.transform.position);
        }
        else
        {
            selectFieldRenderer = null;
            iUIFusionBtn.hideFusionBtn();
        }
    }

    public GameObject getSelectField() { return selectField; }
}

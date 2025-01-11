using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFieldSelector
{
    GameObject GetStartSelectField();
    GameObject GetCurSelectField();
}

public class FieldSelector : MonoBehaviour, IFieldSelector
{
    [SerializeField] Material originMat;
    [SerializeField] Material selectMat;
    [SerializeField] GameObject startSelectField;
    [SerializeField] GameObject curSelectField;
    [SerializeField] MeshRenderer startFieldRenderer;
    [SerializeField] MeshRenderer curFieldRenderer;
    [SerializeField] GameObject fusionBtn;

    private void Start()
    {
        InputMng.onLeftClickDown -= OnFieldClickDown;
        InputMng.onLeftClickDrag -= OnFieldDrag;
        InputMng.onLeftClickUp -= OnFieldClickUp;
        InputMng.onLeftClickDown += OnFieldClickDown;
        InputMng.onLeftClickDrag += OnFieldDrag;
        InputMng.onLeftClickUp += OnFieldClickUp;
    }

    private void OnFieldClickDown(Vector2 _mousePos)
    {
        if (GetFieldMousePos(_mousePos) == null) 
        {
            startSelectField = null;
            return;
        }

        startSelectField = GetFieldMousePos(_mousePos);

        if (startFieldRenderer != null) { startFieldRenderer.material = originMat; }

        if (startSelectField != null)
        {
            startFieldRenderer = null;
            startFieldRenderer = startSelectField.GetComponent<MeshRenderer>();
            startFieldRenderer.material = selectMat;
        }
        else
        {
            startFieldRenderer = null;
        }
    }

    private void OnFieldDrag(Vector2 _mousePos)
    {
        if (GetFieldMousePos(_mousePos) == null) { return; }

        curSelectField = GetFieldMousePos(_mousePos);

        if (curSelectField != startSelectField && curSelectField != null)
        {
            if (curFieldRenderer != null) { curFieldRenderer.material = originMat; }

            curFieldRenderer = curSelectField.GetComponent<MeshRenderer>();
            curFieldRenderer.material = selectMat;
        }
    }

    private void OnFieldClickUp(Vector2 _mousePos)
    {
        if (GetFieldMousePos(_mousePos) == null)
        {
            if (startFieldRenderer != null) { startFieldRenderer.material = originMat; }
            if (curFieldRenderer != null) { curFieldRenderer.material = originMat; }
            Shared.gameUI.iUIFusionBtn.HideFusionBtn();
        }
        else if (startSelectField != curSelectField)
        {
            if (startFieldRenderer != null) { startFieldRenderer.material = originMat; }
            if (curFieldRenderer != null) { curFieldRenderer.material = originMat; }

            Shared.unitMng.iUnitFieldMove.CheckUnitField(
                startSelectField.transform.GetChild(0).gameObject,
                curSelectField.transform.GetChild(0).gameObject);
        }
        else if(startSelectField == curSelectField)
        {
            Shared.gameUI.iUIFusionBtn.HideFusionBtn();

            if (startSelectField != null)
            {
                Shared.gameUI.iUIFusionBtn.ShowFusionBtn(startSelectField.transform.position);
            }
            else
            {
                startSelectField = curSelectField;

                Shared.gameUI.iUIFusionBtn.ShowFusionBtn(startSelectField.transform.position);
            }
        }
        else return;
    }


    public GameObject GetFieldMousePos(Vector2 _mousePos)
    {
        Ray ray = Camera.main.ScreenPointToRay(_mousePos);
        RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity);
        RaycastHit closeHit = new RaycastHit();
        closeHit.distance = Mathf.Infinity;

        for (int i = 0; i < hits.Length; i++) 
        {
            if (hits[i].collider.gameObject.layer == LayerMask.NameToLayer("Field") 
                && hits[i].distance < closeHit.distance)
            {
                closeHit = hits[i];
            }
        }
        if (closeHit.collider == null) return null;

        return closeHit.collider.gameObject;
    }

    public GameObject GetStartSelectField() { return startSelectField; }
    public GameObject GetCurSelectField() { return curSelectField; }
}

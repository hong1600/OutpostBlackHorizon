using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldBuild : MonoBehaviour
{
    [SerializeField] GameObject buildingPre;
    [SerializeField] Material preview;

    GameObject previewObj;

    [SerializeField] LayerMask buildLayer;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) && previewObj == null)
        {
            BuildPreview();
        }

        if (previewObj != null)
        {
            MovePreview(Shared.inputMng.iCustomMouse.GetMousePos());
            UpdatePreviewColor();

            if (Input.GetMouseButtonDown(0) && CanBuild())
            {
                Build();
            }
        }
    }

    private void BuildPreview()
    {
        previewObj = Instantiate(buildingPre);
        previewObj.layer = LayerMask.NameToLayer("Field");

        for(int i = 0; i < previewObj.transform.childCount; i++) 
        {
            previewObj.transform.GetChild(i).gameObject.layer = LayerMask.NameToLayer("Field");
            Renderer rend = previewObj.transform.GetChild(i).GetComponent<Renderer>();

            if (rend != null)
            {
                rend.material = preview;
            }
        }
    }

    private void MovePreview(Vector3 _mousePos)
    {
        Ray ray = Camera.main.ScreenPointToRay(_mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, buildLayer))
        {
            Vector3 gridPos = GetGridPos(hit.point);
            previewObj.transform.position = gridPos;
        }
    }

    private Vector3 GetGridPos(Vector3 originPos)
    {
        float gridSize = 1.0f;
        float x = Mathf.Round(originPos.x / gridSize) * gridSize;
        float z = Mathf.Round(originPos.z / gridSize) * gridSize;

        return new Vector3 (x, originPos.y, z);
    }

    private bool CanBuild()
    {
        Collider[] colls = Physics.OverlapBox
            (previewObj.transform.position, previewObj.transform.localScale / 2,
            Quaternion.identity, ~LayerMask.GetMask("Ground", "Field"));

        return colls.Length == 0;
    }

    private void UpdatePreviewColor()
    {
        Color color = CanBuild() ? new Color(0,1,0,0.5f) : new Color(1,0,0,0.5f);

        for (int i = 0; i < previewObj.transform.childCount; i++)
        {
            Renderer rend = previewObj.transform.GetChild(i).GetComponent<Renderer>();
            rend.material.color = color;
        }
    }

    private void Build()
    {
        Instantiate(buildingPre, previewObj.transform.position, Quaternion.Euler(90, 0, 0));
        Destroy(previewObj);
        previewObj = null;
    }
}

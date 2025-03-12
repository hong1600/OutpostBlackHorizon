using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldBuild : MonoBehaviour
{
    public event Action onDecreaseField;

    CustomMouse customMouse;
    Terrain terrain;
    BoxCollider box;

    [SerializeField] CenterLine centerLine;
    [SerializeField] Transform centerBuilding;

    [SerializeField] GameObject buildingPre;
    [SerializeField] Material preview;
    [SerializeField] Transform parent;
    [SerializeField] LayerMask canBuildLayer;
    [SerializeField] float gridSize = 1.0f;

    GameObject previewObj;
    FieldData fieldData;

    private void Start()
    {
        terrain = Terrain.activeTerrain;
        customMouse = Shared.gameUI.CustomMouse;
    }

    private void Update()
    {
        Vector3 mousePos = customMouse.GetMousePos();

        if (previewObj != null)
        {
            MovePreview(customMouse.GetMousePos());
            UpdatePreviewColor();

            if (Input.GetMouseButtonDown(0) && CanBuild())
            {
                Build();
            }
        }
    }

    public void BuildPreview(GameObject _fieldObj, FieldData _fieldData)
    {
        if(previewObj == null && Shared.gameManager.ViewState.GetViewState() == EViewState.TOP) 
        {
            previewObj = _fieldObj;
            fieldData = _fieldData;
            previewObj = Instantiate(buildingPre);
            previewObj.layer = LayerMask.NameToLayer("BluePrint");

            Renderer rend = previewObj.GetComponent<Renderer>();
            box = previewObj.GetComponent<BoxCollider>();
            box.isTrigger = true;

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

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, canBuildLayer))
        {
            Vector3 gridPos = GetGridPos(hit.point);
            previewObj.transform.position = gridPos;
        }
    }

    private Vector3 GetGridPos(Vector3 originPos)
    {
        float x = Mathf.Round(originPos.x / gridSize) * gridSize;
        float z = Mathf.Round(originPos.z / gridSize) * gridSize;
        float y = terrain.SampleHeight(new Vector3(x, 0, z));

        return new Vector3 (x, y + 1.5f, z);
    }

    private bool CanBuild()
    {
        Vector3 center = new Vector3(centerBuilding.position.x, 0, centerBuilding.position.z);

        float distanceToCenter = Vector3.Distance(center, 
            new Vector3(previewObj.transform.position.x, 0, previewObj.transform.position.z));

        if (distanceToCenter <= centerLine.radius)
        {
            return CheckCanBuild(previewObj.transform.position);
        }
        else return false;
    }

    private bool CheckCanBuild(Vector3 position)
    {
        Collider[] colls = Physics.OverlapSphere(position, gridSize / 2);

        foreach (Collider coll in colls)
        {
            if (coll.gameObject.layer != LayerMask.NameToLayer("Ground") &&
                coll.gameObject.layer != LayerMask.NameToLayer("BluePrint")) return false;
        }

        return true;
    }

    private void UpdatePreviewColor()
    {
        Color color = CanBuild() ? new Color(0,1,0,0.5f) : new Color(1,0,0,0.5f);
        Renderer rend = previewObj.GetComponent<Renderer>();
        rend.material.color = color;
    }

    private void Build()
    {
        box.isTrigger = false;
        Instantiate(buildingPre, previewObj.transform.position, Quaternion.identity, parent.transform);
        Destroy(previewObj);
        previewObj = null;
        Shared.fieldManager.DecreaseFieldAmount(fieldData);
        Shared.gameManager.GoldCoin.UseGold(fieldData.fieldPrice);
        onDecreaseField?.Invoke();
    }
}

using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldBuild : MonoBehaviour
{
    public event Action onDecreaseField;

    CustomCursor cursor;
    Terrain terrain;
    BoxCollider box;
    FieldManager fieldManager;
    GoldCoin goldCoin;
    ViewState viewState;
    UnitFieldData unitFieldData;

    [SerializeField] Center center;
    [SerializeField] Transform centerBuilding;

    [SerializeField] GameObject buildingPre;
    [SerializeField] Material preview;
    [SerializeField] Transform parent;
    [SerializeField] LayerMask canBuildLayer;
    [SerializeField] float gridSize = 1.0f;

    GameObject previewObj;
    FieldData fieldData;

    Vector2 mousePos;

    private void Start()
    {
        terrain = Terrain.activeTerrain;
        cursor = InputManager.instance.cursor;
        fieldManager = Shared.fieldManager;
        goldCoin = Shared.gameManager.GoldCoin;
        viewState = Shared.gameManager.ViewState;
        unitFieldData = Shared.unitManager.UnitFieldData;
    }

    private void Update()
    {
        if(cursor != null) 
        {
            mousePos = cursor.GetMousePos();
        }

        if (previewObj != null)
        {
            MovePreview(mousePos);
            UpdatePreviewColor();

            if (Input.GetMouseButtonDown(0) && CanBuild())
            {
                Build();
            }
        }
    }

    public void BuildPreview(GameObject _fieldObj, FieldData _fieldData)
    {
        if(previewObj == null && viewState.GetViewState() == EViewState.TOP) 
        {
            buildingPre = _fieldObj;
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

        return new Vector3(x, y + (buildingPre.transform.localScale.y * 0.5f), z);
    }

    private bool CanBuild()
    {
        Vector3 centerPos = new Vector3(centerBuilding.position.x, 0, centerBuilding.position.z);

        float distanceToCenter = Vector3.Distance(centerPos, 
            new Vector3(previewObj.transform.position.x, 0, previewObj.transform.position.z));

        if (distanceToCenter <= center.radius)
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
        GameObject unitField = Instantiate(buildingPre, previewObj.transform.position, Quaternion.identity, parent.transform);
        Destroy(previewObj);
        previewObj = null;
        fieldManager.DecreaseFieldAmount(fieldData);
        goldCoin.UseGold(fieldData.fieldPrice);
        if (buildingPre.CompareTag("UnitField"))
        {
            unitFieldData.SetUnitSpawnPointList(unitField.transform);
        }
        else if (buildingPre.CompareTag("TurretField"))
        {
        }
        onDecreaseField?.Invoke();
    }
}

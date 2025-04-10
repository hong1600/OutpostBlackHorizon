using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildBase : MonoBehaviour
{
    public event Action onDecreaseField;

    Terrain terrain;
    CustomCursor cursor;
    BoxCollider box;
    FieldManager fieldManager;
    GoldCoin goldCoin;
    ViewState viewState;
    UnitFieldData unitFieldData;

    [SerializeField] Center center;
    [SerializeField] Transform centerBuilding;

    [SerializeField] GameObject prefabObj;
    [SerializeField] Material preview;
    [SerializeField] Transform parent;
    [SerializeField] LayerMask canBuildLayer;
    [SerializeField] protected float gridSize = 1.0f;

    GameObject previewObj;
    Vector3 gridPos;
    float offsetY;
    int amount;
    int cost;

    Vector2 mousePos;

    private void Start()
    {
        terrain = Terrain.activeTerrain;
        cursor = InputManager.instance.cursor;
        fieldManager = FieldManager.instance;
        goldCoin = GameManager.instance.GoldCoin;
        viewState = GameManager.instance.ViewState;
        unitFieldData = UnitManager.instance.UnitFieldData;
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

    public void BuildPreview(GameObject _prefabObj, int _amount, int _cost)
    {
        if(previewObj == null && viewState.CurViewState == EViewState.TOP) 
        {
            prefabObj = _prefabObj;

            if (_amount != 0)
            {
                amount = _amount;
            }
            cost = _cost;
            previewObj = Instantiate(prefabObj, parent);
            previewObj.layer = LayerMask.NameToLayer("BluePrint");

            Renderer rend = previewObj.GetComponent<Renderer>();
            if (previewObj.GetComponent<BoxCollider>() != null)
            {
                box = previewObj.GetComponent<BoxCollider>();
            }
            else
            {
                box = previewObj.GetComponentInChildren<BoxCollider>();
            }
            box.isTrigger = true;

            if (rend != null)
            {
                rend.material = preview;
            }

            float terrainY = terrain.SampleHeight(gridPos);

            if (prefabObj != null)
            {
                float pivotY = prefabObj.transform.position.y;
                float bottomY = box.bounds.min.y;
                offsetY = pivotY - bottomY;
            }

            float y = terrainY + offsetY;
        }
    }

    private void MovePreview(Vector3 _mousePos)
    {
        Ray ray = Camera.main.ScreenPointToRay(_mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, canBuildLayer))
        {
            gridPos = GetGridPos(hit.point);

            if (Vector3.Distance(previewObj.transform.position, gridPos) > 0.01f)
            {
                previewObj.transform.position = gridPos;
            }
        }
    }

    private Vector3 GetGridPos(Vector3 _originPos)
    {
        float x = Mathf.Round(_originPos.x / gridSize) * gridSize;
        float z = Mathf.Round(_originPos.z / gridSize) * gridSize;

        return new Vector3(x, offsetY, z);
    }

    private bool CanBuild()
    {
        Vector3 centerPos = new Vector3(centerBuilding.position.x, 0, centerBuilding.position.z);

        float distanceToCenter = Vector3.Distance(centerPos, new Vector3(previewObj.transform.position.x,
            0, previewObj.transform.position.z));

        if (distanceToCenter <= center.radius)
        {
            return CheckCanBuild(previewObj.transform.position);
        }
        else return false;
    }

    protected virtual bool CheckCanBuild(Vector3 _position)
    {
        Vector3 size = box.size;
        Vector3 scale = prefabObj.transform.lossyScale;
        Vector3 halfExtents = Vector3.Scale(size, scale) * 0.49f;

        Collider[] colls = Physics.OverlapBox(_position, halfExtents, prefabObj.transform.rotation);

        for (int i = 0; i < colls.Length; i++)
        {
            if (colls[i].gameObject != prefabObj &&
                colls[i].gameObject.layer != LayerMask.NameToLayer("Ground") &&
                colls[i].gameObject.layer != LayerMask.NameToLayer("BluePrint"))
            {
                return false;
            }
        }

        return true;
    }

    private void UpdatePreviewColor()
    {
        Color color = CanBuild() ? new Color(0,1,0,0.5f) : new Color(1,0,0,0.5f);
        Renderer rend;

        if (previewObj.GetComponent<Renderer>() != null)
        {
            rend = previewObj.GetComponent<Renderer>();
        }
        else
        {
            rend = previewObj.GetComponentInChildren<Renderer>();
        }

        rend.material.color = color;
    }

    private void Build()
    {
        box.isTrigger = false;

        GameObject Obj =
            Instantiate(prefabObj, previewObj.transform.position, Quaternion.identity, parent.transform);

        Destroy(previewObj);

        previewObj = null;

        fieldManager.DecreaseFieldAmount(amount);

        goldCoin.UseGold(cost);

        if (prefabObj.CompareTag("UnitField"))
        {
            unitFieldData.SetUnitSpawnPointList(Obj.transform);
        }

        onDecreaseField?.Invoke();
    }
}

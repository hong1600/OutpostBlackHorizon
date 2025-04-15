using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildBase : MonoBehaviour
{
    Terrain terrain;
    protected BoxCollider box;

    CustomCursor cursor;
    FieldManager fieldManager;
    GoldCoin goldCoin;
    ViewState viewState;
    UnitFieldData unitFieldData;
    protected UIBuild uiBuild;

    [SerializeField] Center center;
    [SerializeField] Transform centerBuilding;
    [SerializeField] protected GameObject prefabObj;
    [SerializeField] protected GameObject previewObj;
    protected int id;

    Renderer[] rends;
    protected float gridSize = 1.0f;
    [SerializeField] Transform parent;
    [SerializeField] LayerMask canBuildLayer;

    Transform hitTrs;
    Vector3 gridPos;
    float offsetY;
    int cost;
    bool isBuilding = false;

    Vector2 mousePos;

    private void Start()
    {
        terrain = Terrain.activeTerrain;
        cursor = InputManager.instance.cursor;
        fieldManager = FieldManager.instance;
        goldCoin = GameManager.instance.GoldCoin;
        viewState = GameManager.instance.ViewState;
        unitFieldData = UnitManager.instance.UnitFieldData;
        uiBuild = GameUI.instance.UIBuild;
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

        if(isBuilding && Input.GetKeyDown(KeyCode.B)) 
        {
            CancleBuild();
        }
    }

    public void CreatePreview(GameObject _prefabObj, int _cost, int _id)
    {
        CancleBuild();

        if(previewObj == null && viewState.CurViewState == EViewState.TOP) 
        {
            isBuilding = true;

            cost = _cost;
            prefabObj = _prefabObj;
            id = _id;

            previewObj = Instantiate(prefabObj, parent);

            previewObj.layer = LayerMask.NameToLayer("BluePrint");

            rends = previewObj.GetComponentsInChildren<Renderer>();

            if (previewObj.GetComponent<BoxCollider>() != null)
            {
                box = previewObj.GetComponent<BoxCollider>();
            }
            else
            {
                box = previewObj.GetComponentInChildren<BoxCollider>();
            }
            box.isTrigger = true;

            float terrainY = terrain.SampleHeight(gridPos);

            if (prefabObj != null)
            {
                float pivotY = previewObj.transform.position.y;
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
            if (hit.transform != hitTrs)
            {
                offsetY = GetGridPosY(hit);

                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Field"))
                {
                    gridPos = new Vector3(hit.collider.transform.position.x, offsetY, hit.collider.transform.position.z);
                }
            }
            else if(hit.transform == hitTrs && hit.collider.gameObject.layer != LayerMask.NameToLayer("Field"))
            {
                gridPos = GetGridPos(hit.point);
            }

            if (Vector3.Distance(previewObj.transform.position, gridPos) > 0.01f)
            {
                previewObj.transform.position = gridPos;
            }

            hitTrs = hit.transform;
        }
    }

    private Vector3 GetGridPos(Vector3 _originPos)
    {
        float x = Mathf.Round(_originPos.x / gridSize) * gridSize;
        float z = Mathf.Round(_originPos.z / gridSize) * gridSize;

        return new Vector3(x, offsetY, z);
    }

    private float GetGridPosY(RaycastHit _hit)
    {
        float y;

        float pivotY = previewObj.transform.position.y;
        float bottomY = box.bounds.min.y;

        offsetY = pivotY - bottomY;

        if (_hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            float terrainY = terrain.SampleHeight(_hit.point);

            y = terrainY + offsetY;
        }
        else
        {
            BoxCollider box;
            box = _hit.collider.gameObject.GetComponent<BoxCollider>();

            if (box == null)
            {
                box = _hit.collider.gameObject.GetComponentInChildren<BoxCollider>();
            }

            y = box.bounds.max.y + offsetY;
        }

        return y;
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
            if (colls[i].gameObject != previewObj)
            {
                return false;
            }

            if (Physics.Raycast
                (box.bounds.center, Vector3.down, out RaycastHit hit, box.bounds.extents.y + 0.1f))
            {
                if (hit.collider.gameObject.layer != LayerMask.NameToLayer("Ground") &&
                    hit.collider.gameObject.layer != LayerMask.NameToLayer("BluePrint"))
                {
                    return false;
                }
            }
        }

        return true;
    }

    private void UpdatePreviewColor()
    {
        Color color = CanBuild() ? new Color(0,1,0,0.5f) : new Color(1,0,0,0.5f);

        for (int i = 0; i < rends.Length; i++)
        {
            rends[i].material.color = color;
        }
    }

    protected virtual void Build()
    {
        box.isTrigger = false;

        GameObject Obj =
            Instantiate(prefabObj, previewObj.transform.position, Quaternion.identity, parent.transform);

        Destroy(previewObj);

        previewObj = null;

        goldCoin.UseGold(cost);

        if (prefabObj.CompareTag("UnitField"))
        {
            unitFieldData.SetUnitSpawnPointList(Obj.transform);
        }
    }

    public void CancleBuild()
    {
        Destroy(previewObj);
        prefabObj = null;
        previewObj = null;
        id = 0;
        box = null;
        cost = 0;
        isBuilding = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TurretBuild : BuildBase
{
    Collider floorColl;

    protected override bool CheckCanBuild(Vector3 _position)
    {
        Vector3 size = box.size;
        Vector3 scale = prefabObj.transform.lossyScale;
        Vector3 halfExtents = Vector3.Scale(size, scale) * 0.49f;

        Collider[] colls = Physics.OverlapBox(box.bounds.center, halfExtents, prefabObj.transform.rotation);

        Debug.DrawRay(box.bounds.center, Vector3.down * (box.bounds.extents.y + 0.1f), Color.red);

        for (int i = 0; i < colls.Length; i++)
        {
            if (colls[i].gameObject != previewObj)
            {
                return false;
            }

            if (Physics.Raycast
                (box.bounds.center, Vector3.down, out RaycastHit hit, box.bounds.extents.y + 0.1f))
            {
                if (hit.collider != null) floorColl = hit.collider;

                if (hit.collider.gameObject.CompareTag("TurretField"))
                {
                    return true;
                }
            }
        }

        return false;
    }

    protected override void Build()
    {
        Transform turretParent = floorColl.transform;

        GameObject Obj =
            Instantiate(prefabObj, previewObj.transform.position, Quaternion.identity, turretParent);

        base.Build();
    }
}

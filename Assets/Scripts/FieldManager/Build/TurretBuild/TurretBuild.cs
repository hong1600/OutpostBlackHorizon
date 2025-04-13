using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TurretBuild : BuildBase
{
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
                if (hit.collider.gameObject.CompareTag("TurretField"))
                {
                    return true;
                }
            }
        }

        return false;
    }
}

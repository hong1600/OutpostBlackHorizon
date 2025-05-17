using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldBuild : BuildBase
{
    [SerializeField] Transform fieldParent;

    protected override bool CheckCanBuild(Vector3 _position)
    {
        return base.CheckCanBuild(_position);
    }

    protected override void Build()
    {
        uiBuild.DecreaseFieldAmount(id, 1);

        GameObject Obj =
            Instantiate(prefabObj, previewObj.transform.position, Quaternion.identity, fieldParent.transform);

        unitFieldData.SetUnitSpawnPointList(Obj.transform);

        base.Build();
    }
}

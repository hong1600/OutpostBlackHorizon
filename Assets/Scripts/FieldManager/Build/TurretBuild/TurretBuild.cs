using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBuild : BuildBase
{
    protected override bool CheckCanBuild(Vector3 position)
    {
        Collider[] colls = Physics.OverlapSphere(position, gridSize / 2);

        for (int i = 0; i < colls.Length; i++)
        {
            if (colls[i].gameObject.layer != LayerMask.NameToLayer("Ground") &&
                colls[i].gameObject.layer != LayerMask.NameToLayer("BluePrint") &&
                colls[i].gameObject.layer != LayerMask.NameToLayer("TurretField")) return false;
        }

        return true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldBuild : BuildBase
{
    protected override bool CheckCanBuild(Vector3 _position)
    {
        return base.CheckCanBuild(_position);
    }

    protected override void Build()
    {
        uiBuild.DecreaseFieldAmount(id, 1);

        base.Build();
    }
}

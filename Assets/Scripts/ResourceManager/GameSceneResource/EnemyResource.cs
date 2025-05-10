using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyResource : SceneResourceBase<EEnemy>
{
    public override void LoadResources()
    {
        base.LoadSceneResources(typeEnums);
    }
}
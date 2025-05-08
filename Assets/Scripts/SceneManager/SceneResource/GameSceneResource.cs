using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneResource : SceneResourceBase<EEnemy>
{
    [SerializeField] EEnemy[] eEnemies;

    protected override void LoadResources()
    {
        base.LoadSceneResources(eEnemies);
    }
}

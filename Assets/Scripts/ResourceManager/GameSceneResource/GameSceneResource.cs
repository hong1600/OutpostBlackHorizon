using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneResource : MonoBehaviour
{
    [SerializeField] EnemyResource enemyResource;

    public void Load()
    {
        enemyResource.LoadResources();
    }

    public void Unload()
    {
        enemyResource.UnloadSceneResources();
    }

    public EnemyResource EnemyResource { get { return enemyResource; } }
}

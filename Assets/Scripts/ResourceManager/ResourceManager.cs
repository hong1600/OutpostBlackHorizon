using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    [SerializeField] GameSceneResource gameSceneResource;

    protected override void Awake()
    {
        base.Awake();
    }

    public GameSceneResource GameSceneResource { get {  return gameSceneResource; } }
}

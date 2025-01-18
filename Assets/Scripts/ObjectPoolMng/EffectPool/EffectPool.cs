using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEffectPool
{
    GameObject FindEffect(EEffect _eEffect);
}

public class EffectPool : ObjectPool<EEffect>, IEffectPool
{
    [SerializeField] List<GameObject> effectList = new List<GameObject>();
    [SerializeField] List<Transform> parentList = new List<Transform>();

    private void Start()
    {
        Init(effectList, parentList, 10);
    }

    public GameObject FindEffect(EEffect _eEffect)
    {
        return base.FindObject(_eEffect);
    }
}

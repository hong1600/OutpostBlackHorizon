using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPool : ObjectPool<EEffect>
{
    [SerializeField] List<GameObject> effectList = new List<GameObject>();
    [SerializeField] List<Transform> parentList = new List<Transform>();

    private void Start()
    {
        Init(effectList, parentList);
    }

    public GameObject FindEffect(EEffect _eEffect)
    {
        return base.FindObject(_eEffect);
    }
}

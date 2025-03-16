using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPool : ObjectPool<EEffect>
{
    private void Start()
    {
        Init();
    }

    public GameObject FindEffect(EEffect _type)
    {
        return FindObject(_type);
    }

    public void ReturnEffect(EEffect _type, GameObject _obj)
    {
        ReturnPool(_type, _obj);
    }
}

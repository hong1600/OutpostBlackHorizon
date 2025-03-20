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

    public GameObject FindEffect(EEffect _type, Vector3 _pos, Quaternion _rot)
    {
        return FindObject(_type, _pos, _rot);
    }

    public void ReturnEffect(EEffect _type, GameObject _obj)
    {
        ReturnPool(_type, _obj);
    }
}

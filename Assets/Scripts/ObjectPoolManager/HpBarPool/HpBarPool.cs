using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarPool : ObjectPoolBase<EHpBar>
{
    private void Start()
    {
        Init();
    }

    public GameObject FindHpbar(EHpBar _type, Vector3 _pos, Quaternion _rot)
    {
        return FindObject(_type, _pos, _rot);
    }

    public void ReturnHpBar(EHpBar _type, GameObject _obj)
    {
        ReturnPool(_type, _obj);
    }
}

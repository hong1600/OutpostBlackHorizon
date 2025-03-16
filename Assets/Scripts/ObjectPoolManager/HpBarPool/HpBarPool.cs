using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarPool : ObjectPool<EHpBar>
{
    private void Start()
    {
        Init();
    }

    public GameObject FindHpbar(EHpBar _type)
    {
        return FindObject(_type);
    }

    public void ReturnHpBar(EHpBar _type, GameObject _obj)
    {
        ReturnPool(_type, _obj);
    }
}

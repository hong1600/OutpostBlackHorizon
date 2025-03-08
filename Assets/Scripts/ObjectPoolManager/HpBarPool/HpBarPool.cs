using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarPool : ObjectPool<EHpBar>
{
    [SerializeField] List<GameObject> hpBarList = new List<GameObject>();
    [SerializeField] List<Transform> parentList = new List<Transform>();

    private void Start()
    {
        Init(hpBarList, parentList);
    }

    public GameObject FindHpBar(EHpBar _eHpBar)
    {
        return base.FindObject(_eHpBar);
    }
}

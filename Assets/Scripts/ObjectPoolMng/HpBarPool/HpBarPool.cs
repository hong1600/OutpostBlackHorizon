using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHpBarPool
{
    GameObject FindHpBar(EHpBar _eHpBar);
}

public class HpBarPool : ObjectPool<EHpBar>, IHpBarPool
{
    [SerializeField] List<GameObject> hpBarList = new List<GameObject>();
    [SerializeField] List<Transform> parentList = new List<Transform>();

    private void Start()
    {
        Init(hpBarList, parentList, 30);
    }

    public GameObject FindHpBar(EHpBar _eHpBar)
    {
        return base.FindObject(_eHpBar);
    }
}

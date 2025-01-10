using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHpBarPool
{
    GameObject FindHpBar(string _key);
    GameObject GetHpBar();
    Transform GetParent();
}

public class HpBarPool : MonoBehaviour, IHpBarPool
{
    [SerializeField] GameObject hpBar;
    [SerializeField] Transform parent;

    private void Start()
    {
        Shared.objectPoolMng.Init(hpBar.name, hpBar, 100, parent);
    }

    public GameObject FindHpBar(string _key)
    {
        return Shared.objectPoolMng.GetObject(hpBar.name, parent);
    }


    public GameObject GetHpBar() { return hpBar; }
    public Transform GetParent() { return parent; }
}

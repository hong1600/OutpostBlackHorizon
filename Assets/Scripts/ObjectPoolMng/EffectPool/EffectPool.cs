using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEffectPool
{
    GameObject FindEffect();
}

public class EffectPool : MonoBehaviour, IEffectPool
{
    [SerializeField] GameObject effect;
    [SerializeField] Transform parent;

    private void Start()
    {
        Shared.objectPoolMng.Init(effect.name, effect, 30, parent);
    }

    public GameObject FindEffect()
    {
        return Shared.objectPoolMng.GetObject(effect.name, parent);
    }
}

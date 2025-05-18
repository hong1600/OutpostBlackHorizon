using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSpark : MonoBehaviour
{
    EffectPool effectPool;

    private void Start()
    {
        effectPool = ObjectPoolManager.instance.EffectPool;
    }

    private void OnEnable()
    {
        Invoke("Return", 0.2f);
    }

    private void Return()
    {
        effectPool.ReturnEffect(EEffect.GUNSPARK, gameObject);
    }
}

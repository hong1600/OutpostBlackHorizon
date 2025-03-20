using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSpark : MonoBehaviour
{
    EffectPool pool;

    private void Start()
    {
        pool = Shared.objectPoolManager.EffectPool;
    }

    private void OnEnable()
    {
        Invoke("Return", 0.2f);
    }

    private void Return()
    {
        pool.ReturnEffect(EEffect.GUNSPARK, gameObject);
    }
}

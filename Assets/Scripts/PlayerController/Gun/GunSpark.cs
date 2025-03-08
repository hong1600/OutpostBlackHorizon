using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSpark : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("Return", 0.2f);
    }

    private void Return()
    {
        Shared.objectPoolManager.ReturnObject(this.gameObject.name, gameObject);
    }
}

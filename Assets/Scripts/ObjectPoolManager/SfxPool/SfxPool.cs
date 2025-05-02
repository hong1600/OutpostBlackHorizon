using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxPool : ObjectPool<ESfxObj>
{
    private void Start()
    {
        Init();
    }

    public GameObject FindSfx(ESfxObj _type, Vector3 _pos, Quaternion _rot)
    {
        return FindObject(_type, _pos, _rot);
    }

    public void ReturnSfx(ESfxObj _type, GameObject _obj)
    {
        ReturnPool(_type, _obj);
    }
}

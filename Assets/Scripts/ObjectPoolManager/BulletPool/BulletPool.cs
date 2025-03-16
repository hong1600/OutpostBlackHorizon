using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : ObjectPool<EBullet>
{
    private void Start()
    {
        Init();
    }

    public GameObject FindBullet(EBullet _type)
    {
        return FindObject(_type);
    }

    public void ReturnBullet(EBullet _type, GameObject _obj)
    {
        ReturnPool(_type, _obj);
    }
}

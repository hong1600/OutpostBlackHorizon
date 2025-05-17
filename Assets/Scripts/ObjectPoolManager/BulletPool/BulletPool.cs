using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : ObjectPoolBase<EBullet>
{
    private void Start()
    {
        Init();
    }

    public GameObject FindBullet(EBullet _type, Vector3 _pos, Quaternion _rot)
    {
        return FindObject(_type, _pos, _rot);
    }

    public void ReturnBullet(EBullet _type, GameObject _obj)
    {
        ReturnPool(_type, _obj);
    }
}

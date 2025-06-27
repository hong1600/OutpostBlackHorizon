using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : ObjectPoolBase<EBullet>, IBulletPool
{
    public GameObject FindBullet(EBullet _type, Vector3 _pos, Quaternion _rot)
    {
        return FindObject(_type, _pos, _rot);
    }

    public GameObject FindRemoteBullet(EBullet _type, Vector3 _pos, Quaternion _rot)
    {
        throw new System.NotImplementedException();
    }

    public void ReturnBullet(EBullet _type, GameObject _obj)
    {
        ReturnPool(_type, _obj);
    }
}

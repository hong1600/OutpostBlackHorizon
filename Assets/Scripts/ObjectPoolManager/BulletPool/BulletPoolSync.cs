using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolSync : ObjectPoolBaseSync<EBullet>, IBulletPool
{
    public GameObject FindBullet(EBullet _type, Vector3 _pos, Quaternion _rot)
    {
        return FindObject(_type, _pos, _rot, "Prefabs/Obj/Projectile/Multi/");
    }

    public void ReturnBullet(EBullet _type, GameObject _obj)
    {
        ReturnPool(_type, _obj);
    }
}

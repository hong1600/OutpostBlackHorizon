using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBulletSync : DirectProjectile
{
    public override void Init(Transform _target, float _dmg, float _speed)
    {
        base.Init(_target, _dmg, _speed);

        time = 3;
    }


    protected override void ReturnPool()
    {
        bulletPool.ReturnBullet(EBullet.LASERBULLET, gameObject);
    }
}

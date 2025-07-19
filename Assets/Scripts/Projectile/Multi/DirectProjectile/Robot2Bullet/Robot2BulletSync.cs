using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot2BulletSync : DirectProjectileSync
{
    public void Init(Transform _target, float _dmg, float _speed, EBulletType _eBulletType)
    {
        base.Init(_target, _dmg, _speed);

        type = _eBulletType;

        time = 3;
    }

    protected override void ReturnPool()
    {
        bulletPool.ReturnBullet(EBullet.ROBOT2BULLET, this.gameObject);
    }
}

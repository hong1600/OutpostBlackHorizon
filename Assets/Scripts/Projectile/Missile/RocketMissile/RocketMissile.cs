using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMissile : Missile
{
    Transform fireTrs;

    public void Init(Transform _target, float _dmg, float _speed, EMissile _eMissile, Transform _fireTrs)
    {
        base.Init(_target, _dmg, _speed);

        fireTrs = _fireTrs;
        eMissile = _eMissile;

        time = 20;
    }

    protected override void RiseUp()
    {
        riseDir = fireTrs.transform.rotation * Vector3.forward;

        base.RiseUp();
    }

    protected override void ReturnPool()
    {
        bulletPool.ReturnBullet(EBullet.ROCKETMISSILE, gameObject);
    }
}

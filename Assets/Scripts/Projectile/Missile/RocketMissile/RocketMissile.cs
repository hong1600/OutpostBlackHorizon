using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMissile : Missile
{
    public override void Init(Transform _target, float _dmg, float _speed)
    {
        target = _target;
        dmg = _dmg;
        speed = _speed;
        time = 10;
    }

    protected override void ReturnPool()
    {
        bulletPool.ReturnBullet(EBullet.ROCKETMISSILE, gameObject);
    }
}

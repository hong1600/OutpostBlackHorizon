using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMissile : Missile
{
    public void Init(Transform _target, float _dmg, float _speed, EMissile _eMissile)
    {
        base.Init(_target, _dmg, _speed);

        eMissile = _eMissile;

        time = 20;
    }

    protected override void ReturnPool()
    {
        bulletPool.ReturnBullet(EBullet.ROCKETMISSILE, gameObject);
    }
}

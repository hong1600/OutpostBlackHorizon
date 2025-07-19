using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class JetMissileSync : ArcProjectileSync
{
    Vector3 randomOffset;

    public override void Init(Transform _target, float _dmg, float _speed)
    {
        base.Init(_target, _dmg, _speed);

        time = 10f;
    }

    protected override void ReturnPool()
    {
        bulletPool.ReturnBullet(EBullet.JETMISSILE, gameObject);
    }
}

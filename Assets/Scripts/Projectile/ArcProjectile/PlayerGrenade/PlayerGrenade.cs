using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrenade : ArcProjectile
{
    public override void Init(Transform _target, float _dmg, float _speed)
    {
        base.Init(_target, _dmg, _speed);

        time = 10f;
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.layer == LayerMask.NameToLayer("EnemySensor") ||
            coll.gameObject.layer == LayerMask.NameToLayer("Bullet") ||
            coll.gameObject.layer == LayerMask.NameToLayer("Effect"))
            return;

        GameObject plasma = effectPool.FindEffect(EEffect.PLASMA, transform.position,
            Quaternion.LookRotation(-transform.forward));
        AudioManager.instance.PlaySfx(ESfx.EXPLOSION, transform.position);

        ReturnPool();
    }

    protected override void ReturnPool()
    {
        bulletPool.ReturnBullet(EBullet.PLAYERGRENADE, gameObject);
    }
}

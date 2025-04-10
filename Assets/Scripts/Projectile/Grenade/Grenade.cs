using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Grenade : Projectile
{
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
}

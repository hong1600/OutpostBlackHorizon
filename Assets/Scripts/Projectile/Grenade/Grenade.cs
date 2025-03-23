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

        Vector3 hitPos = coll.ClosestPoint(transform.position);

        GameObject plasma = effectPool.FindEffect(EEffect.PLASMA, hitPos,
            Quaternion.LookRotation(-transform.forward));
        AudioManager.instance.PlaySfx(ESfx.EXPLOSION, transform.position);

        ReturnPool();
    }
}

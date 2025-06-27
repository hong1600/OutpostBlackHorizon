using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ArcProjectileSync : ProjectileBaseSync
{
    private void FixedUpdate()
    {
        CheckHit();
    }

    private void CheckHit()
    {
        float distance = rigid.velocity.magnitude * Time.fixedDeltaTime;

        if (Physics.SphereCast(rigid.position, sphere.radius * transform.lossyScale.x, rigid.velocity.normalized,
            out RaycastHit hit, distance, ~LayerMask.GetMask("EnemySensor", "Bullet", "Effect")))
        {
            GameObject effect = effectPool.FindEffect
                (EEffect.AIRSTRIKEEXPLOSION, transform.position, transform.rotation);
            Explosion explosion = effect.GetComponent<Explosion>();
            explosion.Init(500, EMissile.PLAYER);

            AudioManager.instance.PlaySfx(ESfx.EXPLOSION, transform.position, null);

            ReturnPool();
        }
    }

}

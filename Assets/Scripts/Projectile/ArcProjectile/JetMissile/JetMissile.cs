using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class JetMissile : ArcProjectile
{
    Vector3 randomOffset;

    public override void Init(Transform _target, float _dmg, float _speed)
    {
        base.Init(_target, _dmg, _speed);

        time = 10f;
    }

    private void FixedUpdate()
    {
        CheckHit();
    }

    protected override void ReturnPool()
    {
        bulletPool.ReturnBullet(EBullet.JETMISSILE, gameObject);
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

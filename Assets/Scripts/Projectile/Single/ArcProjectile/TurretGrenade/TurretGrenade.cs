using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretGrenade : ArcProjectile
{
    public override void Init(Transform _target, float _dmg, float _speed)
    {
        base.Init(_target, _dmg, _speed);

        time = 10f;
    }

    private void FixedUpdate()
    {
        CheckHit();
    }

    private void CheckHit()
    {
        if (Physics.SphereCast(rigid.position, sphere.radius * transform.lossyScale.x, transform.forward,
        out RaycastHit hit, speed * Time.deltaTime, ~LayerMask.GetMask("EnemySensor", "Bullet", "Effect")))
        {
            GameObject effect = effectPool.FindEffect
                (EEffect.GRENADETURRETEXPLOSION, transform.position, transform.rotation);
            Explosion explosion = effect.GetComponent<Explosion>();
            explosion.Init(200, EMissile.PLAYER);

            AudioManager.instance.PlaySfx(ESfx.EXPLOSION, transform.position, null);

            ReturnPool();
        }
    }

    protected override void ReturnPool()
    {
        bulletPool.ReturnBullet(EBullet.PLAYERGRENADE, gameObject);
    }
}

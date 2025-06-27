using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DirectProjectileSync : ProjectileBaseSync
{
    protected EBulletType type;

    private void FixedUpdate()
    {
        CheckMoveBullet();
    }

    private void CheckMoveBullet()
    {
        if (isHit) return;

        RaycastHit hit;

        if (type == EBulletType.PLAYER)
        {
            if (Physics.SphereCast(transform.position, sphere.radius, transform.forward,
                 out hit, speed * Time.fixedDeltaTime, ~LayerMask.GetMask
                 ("EnemySensor","Effect")))
            {
               StartCoroutine(StartHitBullet(hit.point, hit.collider));
                isHit = true;
            }
        }
        else
        {
            if (Physics.SphereCast(transform.position, sphere.radius, transform.forward,
                out hit, speed * Time.fixedDeltaTime, ~LayerMask.GetMask
                ("Effect", "Enemy", "EnemySensor")))
            {
                StartCoroutine(StartHitBullet(hit.point, hit.collider));
                isHit = true;
            }
        }
    }

    protected virtual IEnumerator StartHitBullet(Vector3 _hitPos, Collider _hitObj)
    {
        GameObject spark = effectPool.FindEffect(EEffect.GUNSPARK, _hitPos, Quaternion.LookRotation(-transform.forward));

        ITakeDmg iTakeDmg = _hitObj.GetComponentInParent<ITakeDmg>();

        if (type == EBulletType.PLAYER)
        {
            if (iTakeDmg != null && 
                _hitObj.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                isHead = _hitObj.CompareTag("Head");
                float finalDmg = isHead ? dmg * 1.5f : dmg;
                iTakeDmg.TakeDmg(finalDmg, isHead);
            }
        }
        else
        {
            if (iTakeDmg != null && (_hitObj.gameObject.layer == LayerMask.NameToLayer("Player")
                || _hitObj.gameObject.layer == LayerMask.NameToLayer("Field")
                || _hitObj.gameObject.layer == LayerMask.NameToLayer("Center")))
            {
                iTakeDmg.TakeDmg(dmg, false);
            }
        }

        ReturnPool();
        yield return null;
    }
}

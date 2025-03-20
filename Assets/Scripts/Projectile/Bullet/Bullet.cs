using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : Projectile
{
    private void FixedUpdate()
    {
        CheckMoveBullet();
    }

    private void CheckMoveBullet()
    {
        if (isHit) return;

        RaycastHit hit;
        if (Physics.SphereCast(transform.position, sphere.radius, transform.forward,
            out hit, speed * Time.fixedDeltaTime, ~LayerMask.GetMask("EnemySensor", "Bullet")))
        {
            StartCoroutine(StartHitBullet(hit.point, hit.collider));
            isHit = true;
        }
    }

    protected virtual IEnumerator StartHitBullet(Vector3 _hitPos, Collider _hitObj)
    {
        GameObject spark = effectPool.FindEffect(EEffect.GUNSPARK, _hitPos, Quaternion.LookRotation(-transform.forward));

        ITakeDmg iTakeDmg = _hitObj.GetComponentInParent<ITakeDmg>();

        if (_hitObj.gameObject.layer == LayerMask.NameToLayer("Enemy") && iTakeDmg != null)
        {
            isHead = _hitObj.CompareTag("Head");
            float finalDmg = isHead ? dmg * 1.5f : dmg;
            iTakeDmg.TakeDmg(finalDmg, isHead);
        }

        ReturnPool();
        yield return null;
    }
}

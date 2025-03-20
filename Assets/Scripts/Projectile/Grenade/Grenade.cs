using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Grenade : Projectile
{
    private void FixedUpdate()
    {
        MoveGrenadeBullet();
    }

    private void MoveGrenadeBullet()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, sphere.radius, transform.forward,
            speed * Time.fixedDeltaTime, ~LayerMask.GetMask("EnemySensor", "Bullet"));

        if (hits.Length > 0)
        {
            StartCoroutine(StartGrenadeBullet(hits[0].point, hits[0].collider));
        }
    }

    IEnumerator StartGrenadeBullet(Vector3 _hitPos, Collider _hitObj)
    {
        GameObject plasma = effectPool.FindEffect(EEffect.PLASMA, _hitPos, Quaternion.LookRotation(-transform.forward));
        AudioManager.instance.PlaySfx(ESfx.EXPLOSION, transform.position);

        ITakeDmg iTakeDmg = _hitObj.GetComponent<ITakeDmg>();

        if (_hitObj.gameObject.layer == LayerMask.NameToLayer("Enemy") && iTakeDmg != null)
        {
            iTakeDmg.TakeDmg(dmg, false);
        }

        ReturnPool();
        yield return null;
    }
}

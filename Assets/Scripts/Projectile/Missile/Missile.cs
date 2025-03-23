using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Missile : Projectile
{
    [SerializeField] float rotSpd;

    private void FixedUpdate()
    {
        MoveMissile();
    }

    protected virtual void MoveMissile()
    {
        CheckTarget();

        if (target != null)
        {
            Vector3 predictPos = target.position + target.GetComponentInParent<Rigidbody>().velocity * 1;

            Vector3 dir = (predictPos - transform.position).normalized;

            Quaternion targetRot = Quaternion.LookRotation(dir);

            float targetDistance = Vector3.Distance(transform.position, target.position);
            float dynamicRot = Mathf.Lerp(rotSpd, rotSpd * 2f, targetDistance / 10f);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotSpd * Time.fixedDeltaTime);

            rigid.velocity = transform.forward * speed;

        }
    }

    private void CheckTarget()
    {
        if (!target.gameObject.activeSelf || Vector3.Distance(transform.position, target.transform.position) < 0.1f)
        {
            Explode();
        }
    }

    private void Explode()
    {
        GameObject explosionEffect = effectPool.FindEffect
            (EEffect.ROCKETEXPLOSION, transform.position, Quaternion.identity);
        Explosion explosion = explosionEffect.GetComponent<Explosion>();
        explosion.Init(dmg);
        AudioManager.instance.PlaySfx(ESfx.EXPLOSION, transform.position);

        ReturnPool();
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.layer != LayerMask.NameToLayer("EnemySensor") ||
            coll.gameObject.layer != LayerMask.NameToLayer("Bullet"))
        {
            Explode ();
        }
    }
}

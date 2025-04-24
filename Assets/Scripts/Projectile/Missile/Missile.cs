using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EMissile { PLAYER, ENEMY }

public abstract class Missile : Projectile
{
    protected EMissile eMissile;

    [SerializeField] protected float rotSpd;

    float randomExplotionTime;

    protected virtual void FixedUpdate()
    {
        MoveMissile();
    }

    protected virtual void MoveMissile()
    {
        CheckTarget();

        if (target == null) return;

        Rigidbody targetRigid = target.GetComponentInParent<Rigidbody>();
        Vector3 targetPos = target.position;

        if (targetRigid != null)
        {
            targetPos += targetRigid.velocity * 2;
        }

        Vector3 dir = (targetPos - transform.position).normalized;

        Quaternion targetRot = Quaternion.LookRotation(dir);

        float distance = Vector3.Distance(transform.position, targetPos);
        float dynamicSpeed = Mathf.Lerp(rotSpd, rotSpd * 2f, distance / 10f);

        transform.rotation = Quaternion.RotateTowards
            (transform.rotation, targetRot, dynamicSpeed * Time.fixedDeltaTime);

        rigid.velocity = transform.forward * speed;
    }

    protected void CheckTarget()
    {
        if (target == null || !target.gameObject.activeSelf)
        {
            UpdateRandomExplosion();

            Invoke(nameof(Explode), randomExplotionTime);

            return;
        }

        if (Vector3.Distance(transform.position, target.transform.position) < 0.1f)
        {
            Explode();
        }
    }

    private void UpdateRandomExplosion()
    {
        randomExplotionTime = Random.Range(0, 5);
    }

    private void Explode()
    {
        GameObject explosionEffect = effectPool.FindEffect
            (EEffect.ROCKETEXPLOSION, transform.position, Quaternion.identity);
        Explosion explosion = explosionEffect.GetComponent<Explosion>();
        explosion.Init(dmg, eMissile);
        AudioManager.instance.PlaySfx(ESfx.EXPLOSION, transform.position);

        ReturnPool();
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.layer != LayerMask.NameToLayer("EnemySensor") &&
            coll.gameObject.layer != LayerMask.NameToLayer("Bullet") &&
            coll.gameObject.layer != LayerMask.NameToLayer("Effect"))
        {
            Explode ();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EMissile { PLAYER, ENEMY }

public abstract class HomingProjectile : ProjectileBase
{
    protected EMissile eMissile;

    enum EMissileState { UP, TRACK, LOST }
    EMissileState curFlyState = EMissileState.UP;

    [SerializeField] float upDuration;
    [SerializeField] float upTimer;
    protected Vector3 riseDir;

    [SerializeField] protected float rotSpd;

    protected bool isTargetLost = false;

    protected virtual void FixedUpdate()
    {
        switch (curFlyState)
        {
            case EMissileState.UP:
                RiseUp();
                break;
            case EMissileState.TRACK:
                MoveMissile();
                break;
            case EMissileState.LOST:
                LostMove();
                break;
        }
    }

    protected virtual void RiseUp()
    {
        rigid.velocity = riseDir * speed;

        upTimer += Time.fixedDeltaTime;

        if (upTimer >= upDuration)
        {
            curFlyState = EMissileState.TRACK;
            upTimer = 0f;
        }
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

    protected virtual void LostMove()
    {
        rigid.velocity = transform.forward * speed;
    }

    protected void CheckTarget()
    {
        if (target == null || !target.gameObject.activeSelf)
        {
            LostTarget();
            return;
        }

        if (Vector3.Distance(transform.position, target.transform.position) < 0.1f)
        {
            Explode();
        }
    }

    protected virtual void LostTarget()
    {
        if (!isTargetLost)
        {
            isTargetLost = true;
            curFlyState = EMissileState.LOST;
        }
    }

    protected void Explode()
    {
        GameObject explosionEffect = effectPool.FindEffect
            (EEffect.ROCKETEXPLOSION, transform.position, Quaternion.identity);

        Explosion explosion = explosionEffect.GetComponent<Explosion>();

        explosion.Init(dmg, eMissile);

        rigid.velocity = Vector3.zero;
        isTargetLost = false;
        curFlyState = EMissileState.UP;

        ReturnPool();
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.layer != LayerMask.NameToLayer("EnemySensor") &&
            coll.gameObject.layer != LayerMask.NameToLayer("Effect"))
        {
            Explode ();
        }
    }
}

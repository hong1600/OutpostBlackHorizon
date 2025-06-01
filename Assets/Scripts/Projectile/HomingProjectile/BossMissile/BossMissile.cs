using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMissile : HomingProjectile
{
    Vector3 randomOffset;

    public void Init(Transform _target, float _dmg, float _speed, EMissile _eMissile)
    {
        base.Init(_target, _dmg, _speed);

        eMissile = _eMissile;

        randomOffset = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f));
    }

    protected override void RiseUp()
    {
        riseDir = (Vector3.up + transform.forward * 0.3f).normalized;
        base.RiseUp();
    }

    protected override void MoveMissile()
    {
        CheckTarget();

        if (target == null) return;

        Rigidbody targetRigid = target.GetComponentInParent<Rigidbody>();

        Vector3 targetPos = target.position;

        if (targetRigid != null)
        {
            targetPos += targetRigid.velocity * 2;
        }

        targetPos += randomOffset;

        Vector3 dir = (targetPos - transform.position).normalized;

        Quaternion targetRot = Quaternion.LookRotation(dir);

        float distance = Vector3.Distance(transform.position, targetPos);
        float dynamicSpeed = Mathf.Lerp(rotSpd, rotSpd * 2f, distance / 10f);

        transform.rotation = Quaternion.RotateTowards
            (transform.rotation, targetRot, dynamicSpeed * Time.fixedDeltaTime);

        rigid.velocity = transform.forward * speed;
    }

    protected override void ReturnPool()
    {
        bulletPool.ReturnBullet(EBullet.BOSSMISSILE, gameObject);
    }
}

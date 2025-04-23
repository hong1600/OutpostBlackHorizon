using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMissile : Missile
{
    enum EMissileState { UP, TRACK }
    EMissileState curState = EMissileState.UP;

    float upDuration = 3f;
    float upTimer = 0f;

    Vector3 randomOffset;

    protected override void FixedUpdate()
    {
        switch (curState) 
        {
            case EMissileState.UP:
                RiseUp();
                break;
            case EMissileState.TRACK:
                MoveMissile();
                break;
        }
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

    private void RiseUp()
    {
        Vector3 riseDir = (Vector3.up + transform.forward * 0.3f).normalized;

        rigid.velocity = riseDir * speed;

        upTimer += Time.fixedDeltaTime;

        if(upTimer >= upDuration) 
        {
            curState = EMissileState.TRACK;
        }
    }

    public void Init(Transform _target, float _dmg, float _speed, EMissile _eMissile)
    {
        base.Init(_target, _dmg, _speed);

        eMissile = _eMissile;

        randomOffset = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f));
    }

    protected override void ReturnPool()
    {
        bulletPool.ReturnBullet(EBullet.BOSSMISSILE, gameObject);
    }
}

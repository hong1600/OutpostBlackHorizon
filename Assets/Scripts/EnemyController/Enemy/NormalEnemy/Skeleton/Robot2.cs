using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot2 : NormalEnemy
{
    [SerializeField] Transform fireTrs;
    [SerializeField] float bulletSpeed = 500f;

    protected override void Awake()
    {
        base.Awake();
        base.InitEnemyData(DataManager.instance.TableEnemy.Get(202), EEnemy.ROBOT2);
    }

    protected override IEnumerator StartAttack()
    {
        isAttack = true;

        GameObject bulletObj = bulletPool.FindBullet(EBullet.ROBOT2BULLET, fireTrs.position, fireTrs.rotation);
        Bullet bullet = bulletObj.GetComponent<Bullet>();
        bullet.Init(myTarget, attackDmg, bulletSpeed);

        yield return new WaitForSeconds(1f);

        isAttack = false;
        attackCoroutine = null;
    }
}

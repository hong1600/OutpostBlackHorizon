using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot2 : NormalEnemy
{
    [SerializeField] Transform firePos;
    [SerializeField] float bulletSpeed = 500f;

    private void Start()
    {
        base.InitEnemyData(DataManager.instance.TableEnemy.Get(202));
    }

    protected override IEnumerator StartAttack()
    {
        isAttack = true;

        GameObject bulletObj = bulletPool.FindBullet(EBullet.BULLET);
        Bullet bullet = bulletObj.GetComponent<Bullet>();
        bullet.InitBullet(myTarget, attackDmg, bulletSpeed, EBullet.BULLET);

        yield return new WaitForSeconds(1f);

        isAttack = false;
        attackCoroutine = null;
    }
}

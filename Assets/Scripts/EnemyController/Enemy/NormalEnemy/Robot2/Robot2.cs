using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot2 : NormalEnemy
{
    TableEnemy.Info info;

    [SerializeField] Transform fireTrs;
    [SerializeField] float bulletSpeed = 500f;

    private void Awake()
    {
        info = DataManager.instance.TableEnemy.Get(202);

        base.InitEnemyData(info.Name, info.MaxHp, info.Speed, info.AttackRange, info.AttackDmg, EEnemy.ROBOT2);
    }

    protected override IEnumerator StartAttack()
    {
        isAttack = true;

        GameObject bulletObj = bulletPool.FindBullet(EBullet.ROBOT2BULLET, fireTrs.position, fireTrs.rotation);
        Robot2Bullet bullet = bulletObj.GetComponent<Robot2Bullet>();
        bullet.Init(myTarget, attackDmg, bulletSpeed);

        yield return new WaitForSeconds(1f);

        isAttack = false;
        attackCoroutine = null;
    }
}

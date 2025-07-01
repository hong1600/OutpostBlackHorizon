using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot2 : NormalEnemy
{
    [SerializeField] Transform fireTrs;
    [SerializeField] float bulletSpeed = 500f;

    public override void Init(string _name, float _maxHp, float _spd, float _range, float _dmg, EEnemy _eEnemy, int _id)
    {
        base.Init(_name, _maxHp, _spd, _range, _dmg, _eEnemy, _id);
    }

    protected override IEnumerator StartAttack()
    {
        isAttack = true;

        GameObject bulletObj = bulletPool.FindBullet(EBullet.ROBOT2BULLET, fireTrs.position, fireTrs.rotation);
        AudioManager.instance.PlaySfx(ESfx.GUNSHOT, transform.position, null);
        Robot2Bullet bullet = bulletObj.GetComponent<Robot2Bullet>();
        bullet.Init(myTarget, attackDmg, bulletSpeed, EBulletType.ENEMY);

        yield return new WaitForSeconds(1f);

        isAttack = false;
        attackCoroutine = null;
    }
}

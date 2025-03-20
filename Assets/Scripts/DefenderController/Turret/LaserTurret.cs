using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurret : Defender
{
    TableTurret.Info info;

    [SerializeField] float bulletSpd;
    [SerializeField] Transform fireTrs;

    private void Start()
    {
        info = DataManager.instance.TableTurret.GetTurretData(403);
        base.Init(info.ID, info.Name, info.Damage, info.AttackSpeed, info.AttackRange, info.ImgPath, true);
    }

    protected override IEnumerator OnDamageEvent(Enemy _enemy, int _dmg)
    {
        GameObject bulletObj = bulletPool.FindBullet(EBullet.LASERBULLET, fireTrs.position, fireTrs.rotation);
        LaserBullet bullet = bulletObj.GetComponent<LaserBullet>();
        bullet.Init(null, attackDamage, bulletSpd);

        yield return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineTurret : Defender
{
    TableTurret.Info info;

    [SerializeField] Transform fireTrs;
    [SerializeField] float bulletSpd;

    private void Start()
    {
        info = DataManager.instance.TableTurret.GetTurretData(401);
        base.Init(info.ID, info.Name, info.Damage, info.AttackSpeed, info.AttackRange, info.ImgPath, true);
    }

    protected override IEnumerator OnDamageEvent(Enemy _enemy, int _dmg)
    {
        GameObject bulletObj = bulletPool.FindBullet(EBullet.MACHINEBULLET, fireTrs.position, fireTrs.rotation);
        MachineBullet bullet = bulletObj.GetComponent<MachineBullet>();
        bullet.Init(null, attackDamage, bulletSpd);
        yield return null;
    }
}

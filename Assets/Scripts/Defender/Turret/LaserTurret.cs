using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurret : DefenderBase
{
    TableTurret.Info info;

    [SerializeField] float bulletSpd;
    [SerializeField] Transform[] fireTrs;

    private void Start()
    {
        info = DataManager.instance.TableTurret.GetTurretData(403);
        base.Init(info.ID, info.Name, info.Damage, info.AttackSpeed, info.AttackRange, info.ImgName, true);
    }

    protected override IEnumerator OnDamageEvent(EnemyBase _enemy, int _dmg)
    {
        for (int i = 0; i < fireTrs.Length; i++)
        {
            GameObject bulletObj = bulletPool.FindBullet
                (EBullet.LASERBULLET, fireTrs[i].position, fireTrs[i].rotation);
            LaserBullet bullet = bulletObj.GetComponent<LaserBullet>();
            bullet.Init(null, attackDamage, bulletSpd);

            audioManager.PlaySfx(ESfx.LASER, transform.position);

            yield return new WaitForSeconds(0.2f);
        }

        yield return null;
    }
}

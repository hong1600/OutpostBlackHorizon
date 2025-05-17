using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineTurret : DefenderBase
{
    TableTurret.Info info;

    [SerializeField] Transform[] fireTrs;
    [SerializeField] float bulletSpd;

    private void Start()
    {
        info = DataManager.instance.TableTurret.GetTurretData(401);
        base.Init(info.ID, info.Name, info.Damage, info.AttackSpeed, info.AttackRange, info.ImgName, true);
    }

    protected override IEnumerator OnDamageEvent(EnemyBase _enemy, int _dmg)
    {
        for (int i = 0; i < fireTrs.Length; i++)
        {
            GameObject bulletObj = bulletPool.FindBullet(EBullet.MACHINEBULLET, fireTrs[i].position, fireTrs[i].rotation);
            MachineBullet bullet = bulletObj.GetComponent<MachineBullet>();
            bullet.Init(null, attackDamage, bulletSpd);
            audioManager.PlaySfx(ESfx.MACHINEGUN, transform.position);

            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }
}

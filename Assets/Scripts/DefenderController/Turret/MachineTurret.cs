using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineTurret : Defender
{
    TableTurret.Info info;

    [SerializeField] GameObject gunLine;
    [SerializeField] Transform fireTrs;

    private void Awake()
    {
        info = DataManager.instance.TableTurret.GetTurretData(401);
        base.Init(info.ID, info.Name, info.Damage, info.AttackSpeed, info.AttackRange, info.ImgPath, true);
    }

    protected override IEnumerator OnDamageEvent(Enemy _enemy, int _dmg)
    {
        GameObject effect = effectPool.FindEffect(EEffect.LASER);
        LaserEffect laserEffect = effect.GetComponent<LaserEffect>();
        laserEffect.Init(fireTrs, Color.yellow);
        laserEffect.Fire(target.transform.position);

        return base.OnDamageEvent(_enemy, _dmg);
    }
}

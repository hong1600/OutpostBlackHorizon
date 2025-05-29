using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : UnitBase
{
    TableUnit.Info info;

    private void Awake()
    {
        info = DataManager.instance.TableUnit.GetUnitData(102);

        base.Init(info.ID, info.Name, info.Damage, info.AttackSpeed, info.AttackRange, info.SpriteName, false);
        base.UnitInit(info.Grade);
    }

    protected override IEnumerator OnDamageEvent(EnemyBase enemy, int _dmg)
    {
        int rand = Random.Range(0, 100);

        yield return base.OnDamageEvent(enemy, _dmg);

        if (rand < 12)
        {
            enemy.StayEnemy(1f);

            EEffect eEffect = (EEffect)EEffect.SHIELD;
            GameObject effect = effectPool.FindEffect(eEffect, 
                enemy.transform.position + (Vector3.up * 2), Quaternion.identity);

            yield return new WaitForSeconds(1f);

            effectPool.ReturnEffect(eEffect, effect);
        }

        yield return null;
    }
}

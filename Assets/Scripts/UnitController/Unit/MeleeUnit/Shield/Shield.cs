using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MeleeUnit
{
    private void Awake()
    {
        Init(DataMng.instance.TableUnit.GetUnitData(102));
    }
    protected override IEnumerator OnDamageEvent(Enemy enemy, int _dmg)
    {
        int rand = Random.Range(0, 100);

        yield return base.OnDamageEvent(enemy, _dmg);

        if (rand < 12)
        {
            enemy.StayEnemy(1f);

            EEffect eEffect = (EEffect)EEffect.SHIELD;
            GameObject effect = Shared.objectPoolMng.iEffectPool.FindEffect(eEffect);
            effect.transform.position = enemy.transform.position + (Vector3.up * 2);

            yield return new WaitForSeconds(1f);

            Shared.objectPoolMng.ReturnObject(effect.name, effect);
        }

        yield return null;
    }
}

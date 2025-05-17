using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MeleeUnit
{
    TableUnit.Info info;

    [SerializeField] GameObject effect;

    private void Awake()
    {
        info = DataManager.instance.TableUnit.GetUnitData(101);

        base.Init(info.ID, info.Name, info.Damage, info.AttackSpeed, info.AttackRange, info.SpriteName, false);
        base.UnitInit(info.Grade);
    }

    protected override IEnumerator OnDamageEvent(EnemyBase _enemy, int _dmg)
    {
        int rand = Random.Range(0, 100);

        yield return base.OnDamageEvent(_enemy, _dmg);

        if (rand < 5)
        {
            GameManager.instance.GoldCoin.SetGold(10);

            EEffect eEffect = (EEffect)EEffect.SWORD;
            GameObject effect = effectPool.FindEffect(eEffect, gameObject.transform.position, Quaternion.identity);

            yield return new WaitForSeconds(0.75f);

            effectPool.ReturnEffect(eEffect, effect.gameObject);
        }
    }
}

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

        base.Init(info.ID, info.Name, info.Damage, info.AttackSpeed, info.AttackRange, info.ImgPath);
        base.UnitInit(info.Grade);
    }

    protected override IEnumerator OnDamageEvent(Enemy _enemy, int _dmg)
    {
        int rand = Random.Range(0, 100);

        yield return base.OnDamageEvent(_enemy, _dmg);

        if (rand < 5)
        {
            Shared.gameManager.GoldCoin.SetGold(10);

            EEffect eEffect = (EEffect)EEffect.SWORD;
            GameObject effect = Shared.objectPoolManager.EffectPool.FindEffect(eEffect);
            effect.transform.position = this.gameObject.transform.position;

            yield return new WaitForSeconds(0.75f);

            Shared.objectPoolManager.ReturnObject(effect.name, effect.gameObject);
        }
    }
}

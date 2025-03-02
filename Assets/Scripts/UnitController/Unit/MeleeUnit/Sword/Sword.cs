using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MeleeUnit
{
    [SerializeField] GameObject effect;

    private void Awake()
    {
        Init(DataMng.instance.TableUnit.GetUnitData(101));
    }

    protected override IEnumerator OnDamageEvent(Enemy enemy, int _dmg)
    {
        int rand = Random.Range(0, 100);

        yield return base.OnDamageEvent(enemy, _dmg);

        if (rand < 5)
        {
            Shared.gameMng.GoldCoin.SetGold(10);

            EEffect eEffect = (EEffect)EEffect.SWORD;
            GameObject effect = Shared.objectPoolMng.iEffectPool.FindEffect(eEffect);
            effect.transform.position = this.gameObject.transform.position;

            yield return new WaitForSeconds(0.75f);

            Shared.objectPoolMng.ReturnObject(effect.name, effect.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MeleeUnit
{
    [SerializeField] GameObject effect;

    private void Awake()
    {
        Init(DataManager.instance.TableUnit.GetUnitData(101));
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

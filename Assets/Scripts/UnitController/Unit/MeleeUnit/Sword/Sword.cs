using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MeleeUnit
{
    [SerializeField] UnitData unitData;
    [SerializeField] GameObject effect;

    private void Awake()
    {
        Init(unitData);
    }

    protected override IEnumerator OnDamageEvent(Enemy enemy)
    {
        int rand = Random.Range(0, 100);

        if (rand < 5)
        {
            Shared.gameMng.iGoldCoin.SetGold(10);

            EEffect eEffect = (EEffect)EEffect.SWORD;

            GameObject effect = Instantiate(Shared.objectPoolMng.iEffectPool.FindEffect(eEffect),
                this.transform.position, Quaternion.identity);

            yield return new WaitForSeconds(0.5f);

            Shared.objectPoolMng.ReturnObject(effect.name, effect.gameObject);
        }
    }
}

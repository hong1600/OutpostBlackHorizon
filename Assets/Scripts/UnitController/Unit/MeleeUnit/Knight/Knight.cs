using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Unit
{

    private void Awake()
    {
        Init(DataMng.instance.TableUnit.GetUnitData(103));
    }

    protected override IEnumerator StartAttack()
    {
        int rand = Random.Range(0, 100);

        if (rand < 20 && skillCouroutine == null)
        {
            isSkill = true;
            StartCoroutine(StartSkill());
        }
        else if (rand >= 20 && attackCoroutine == null)
        {
            yield return base.StartAttack();
        }
    }

    protected override IEnumerator StartSkill()
    {
        Enemy enemy = target.GetComponent<Enemy>();

        yield return new WaitForSeconds(0.85f);

        EEffect eEffect = (EEffect)EEffect.KNIGHT;
        GameObject effect = Shared.objectPoolMng.iEffectPool.FindEffect(eEffect);
        yield return base.OnDamageEvent(enemy, skillDamage);
        effect.transform.position = enemy.transform.position;
        skillCouroutine = null;

        yield return new WaitForSeconds(0.85f);

        isSkill = false;
        Shared.objectPoolMng.ReturnObject(effect.name, effect);
    }
}

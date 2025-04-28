using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Unit
{
    TableUnit.Info info;

    private void Awake()
    {
        info = DataManager.instance.TableUnit.GetUnitData(103);

        base.Init(info.ID, info.Name, info.Damage, info.AttackSpeed, info.AttackRange, info.SpriteName, false);
        base.UnitInit(info.Grade);
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
        Enemy enemy = target.GetComponentInParent<Enemy>();

        yield return new WaitForSeconds(0.85f);

        GameObject effect = effectPool.FindEffect(EEffect.KNIGHT, enemy.transform.position, Quaternion.identity);
        yield return base.OnDamageEvent(enemy, skillDamage);
        skillCouroutine = null;

        yield return new WaitForSeconds(0.85f);

        isSkill = false;
        effectPool.ReturnEffect(EEffect.KNIGHT, effect);
    }
}

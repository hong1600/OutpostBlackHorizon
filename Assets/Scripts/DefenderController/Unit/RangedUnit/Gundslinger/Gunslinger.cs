using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunslinger : RangedUnit
{
    TableUnit.Info info;

    [SerializeField] Transform bulletTrs;

    private void Awake()
    {
        info = DataManager.instance.TableUnit.GetUnitData(104);

        base.Init(info.ID, info.Name, info.Damage, info.AttackSpeed, info.AttackRange, info.SpriteName, false);
        base.UnitInit(info.Grade);
    }

    protected override IEnumerator StartAttack()
    {
        if (unitSkillBar.isSkillCast && skillCouroutine == null)
        {
            StartCoroutine(StartSkill());
        }
        else if (!isSkill && attackCoroutine == null)
        {
            yield return base.StartAttack();
        }
    }

    protected override IEnumerator StartSkill()
    {
        isSkill = true;
        Enemy enemy = target.GetComponent<Enemy>();

        yield return new WaitForSeconds(1.3f);

        EEffect eEffect = (EEffect)EEffect.GUNSLINGER;
        GameObject effect = effectPool.FindEffect(eEffect, bulletTrs.position, Quaternion.identity);
        effect.GetComponent<GunslingerEffect>().Init(enemy.gameObject.transform, 100, 150f);
        skillCouroutine = null;
        unitSkillBar.ResetSkillBar();

        yield return new WaitForSeconds(2f);

        isSkill = false;
    }

}

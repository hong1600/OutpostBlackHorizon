using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : RangedUnit
{
    TableUnit.Info info;

    Vector3 yOffset = Vector3.up * 0.1f;

    private void Awake()
    {
        info = DataManager.instance.TableUnit.GetUnitData(105);

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

        yield return new WaitForSeconds(1.5f);

        if (target != null)
        {
            GameObject effect = effectPool.FindEffect
                (EEffect.MAGE, target.transform.position + yOffset, Quaternion.identity);
            unitSkillBar.ResetSkillBar();
        }

        skillCouroutine = null;

        yield return new WaitForSeconds(1f);

        isSkill = false;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : RangedUnit
{
    TableUnit.Info info;

    private void Awake()
    {
        info = DataManager.instance.TableUnit.GetUnitData(105);

        base.Init(info.ID, info.Name, info.Damage, info.AttackSpeed, info.AttackRange, info.ImgPath);
        base.UnitInit(info.Grade);
    }

    protected override IEnumerator StartAttack()
    {
        if (skillBar.GetComponent<UnitSkillBar>().isSkillCast && skillCouroutine == null)
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

        yield return new WaitForSeconds(1.5f);

        EEffect eEffect = (EEffect)EEffect.MAGE;
        GameObject effect = Shared.objectPoolManager.EffectPool.FindEffect(eEffect);
        effect.transform.position = enemy.transform.position + new Vector3(0,0.1f,0);
        skillCouroutine = null;
        skillBar.GetComponent<UnitSkillBar>().ResetSkillBar();

        yield return new WaitForSeconds(1f);

        isSkill = false;
    }

}

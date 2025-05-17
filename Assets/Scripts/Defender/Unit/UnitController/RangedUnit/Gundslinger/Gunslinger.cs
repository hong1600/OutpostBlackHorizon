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

        yield return new WaitForSeconds(1.3f);

        if (target != null)
        {
            GameObject effect = effectPool.FindEffect(EEffect.GUNSLINGER, bulletTrs.position, Quaternion.identity);
            effect.GetComponent<GunslingerEffect>().Init(enemy.gameObject.transform, 50, 150f);
            unitSkillBar.ResetSkillBar();
        }

        skillCouroutine = null;

        yield return new WaitForSeconds(2f);

        isSkill = false;
    }

}

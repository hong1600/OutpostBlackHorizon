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

        yield return new WaitForSeconds(1.3f);

        EEffect eEffect = (EEffect)EEffect.GUNSLINGER;
        GameObject effect = Shared.objectPoolManager.EffectPool.FindEffect(eEffect);
        effect.transform.position = bulletTrs.position;
        effect.GetComponent<GunslingerEffect>().Init(enemy.gameObject.transform, 100, 150f);
        skillCouroutine = null;
        skillBar.GetComponent<UnitSkillBar>().ResetSkillBar();

        yield return new WaitForSeconds(2f);

        isSkill = false;
    }

}

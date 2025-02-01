using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunslinger : RangedUnit
{
    [SerializeField] UnitData unitData;
    [SerializeField] Transform bulletTrs;

    private void Awake()
    {
        Init(unitData);
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
        GameObject effect = Shared.objectPoolMng.iEffectPool.FindEffect(eEffect);
        effect.transform.position = bulletTrs.position;
        effect.GetComponent<GunslingerEffect>().Init(enemy.gameObject.transform, 100, 150f);
        skillCouroutine = null;
        skillBar.GetComponent<UnitSkillBar>().ResetSkillBar();

        yield return new WaitForSeconds(2f);

        isSkill = false;
    }

}

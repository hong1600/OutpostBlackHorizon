using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageSkill : IUnitSkillStrategy
{
    public IEnumerator Excute(SkillContext _ctx)
    {
        _ctx.onStart?.Invoke();

        yield return new WaitForSeconds(1.5f);

        if (_ctx.target != null)
        {
            GameObject effect = _ctx.effectPool.FindEffect
                (EEffect.MAGE, _ctx.target.transform.position + Vector3.up * 0.1f, Quaternion.identity);

            _ctx.unitSkillBar.ResetSkillBar();
        }

        _ctx.onFinish?.Invoke();

        yield return new WaitForSeconds(1f);

        _ctx.onComplete?.Invoke();
    }
}

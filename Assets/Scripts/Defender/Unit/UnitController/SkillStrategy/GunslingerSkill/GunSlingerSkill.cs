using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSlingerSkill : IUnitSkillStrategy
{
    public IEnumerator Excute(SkillContext _ctx)
    {
        _ctx.onStart?.Invoke();

        yield return new WaitForSeconds(1.3f);

        if (_ctx.target != null)
        {
            GameObject effect = 
                _ctx.effectPool.FindEffect(EEffect.GUNSLINGER, _ctx.fireTrs.position, Quaternion.identity);

            effect.GetComponent<GunslingerEffect>().Init(_ctx.target, 50, 150f);

            _ctx.unitSkillBar.ResetSkillBar();
        }

        _ctx.onFinish?.Invoke();

        yield return new WaitForSeconds(2f);

        _ctx.onComplete?.Invoke();

    }
}

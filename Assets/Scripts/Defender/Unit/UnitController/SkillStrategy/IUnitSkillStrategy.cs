using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitSkillStrategy
{
    IEnumerator Excute(SkillContext _context);
}

public class SkillContext
{
    public Transform target;
    public Transform fireTrs;
    public EffectPool effectPool;
    public UnitSkillBar unitSkillBar;
    public Action onStart;
    public Action onFinish;
    public Action onComplete;

    public SkillContext(Transform _target, Transform _fireTrs, EffectPool _effectPool, UnitSkillBar _skillBar,
                        Action _onStart = null, Action _onFinish = null, Action _onComplete = null)
    {
        this.target = _target;
        this.fireTrs = _fireTrs;
        this.effectPool = _effectPool;
        this.unitSkillBar = _skillBar;
        this.onStart = _onStart;
        this.onFinish = _onFinish;
        this.onComplete = _onComplete;
    }
}
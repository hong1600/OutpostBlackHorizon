using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunslinger : UnitBase
{
    TableUnit.Info info;

    [SerializeField] Transform fireTrs;

    private void Awake()
    {
        info = DataManager.instance.TableUnit.GetUnitData(104);

        base.Init(info.ID, info.Name, info.Damage, info.AttackSpeed, info.AttackRange, info.SpriteName, false);
        base.UnitInit(info.Grade);
        skill = new GunSlingerSkill();
    }

    protected override void SetSkill()
    {
        skillContext = new SkillContext(target.transform, fireTrs, effectPool,
            unitSkillBar, OnSkillStart, OnSkillFinish, OnSkillComplete);
    }
}

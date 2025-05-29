using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : UnitBase
{
    TableUnit.Info info;

    Vector3 yOffset = Vector3.up * 0.1f;

    private void Awake()
    {
        info = DataManager.instance.TableUnit.GetUnitData(105);

        base.Init(info.ID, info.Name, info.Damage, info.AttackSpeed, info.AttackRange, info.SpriteName, false);
        base.UnitInit(info.Grade);
        skill = new MageSkill();
    }

    protected override void SetSkill()
    {
        skillContext = new SkillContext(target.transform, null, effectPool,
            unitSkillBar, OnSkillStart, OnSkillFinish, OnSkillComplete);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Unit : DefenderController
{
    public EUnitGrade eUnitGrade;
    public int lastUpgrade;
    public int skillDamage;

    [SerializeField] EUnitAI aiState;
    UnitAI unitAI;
    TableUnit tableUnit;
    Animator anim;
    BoxCollider box;
    public GameObject skillBar;

    [SerializeField] protected internal bool isSkill;
    [SerializeField] protected internal Coroutine skillCouroutine;

    protected virtual void UnitInit(EUnitGrade _eUnitGrade)
    {
        unitAI = new UnitAI();
        unitAI.Init(this);

        anim = this.GetComponent<Animator>();
        box = this.GetComponent<BoxCollider>();

        eUnitGrade = _eUnitGrade;
        skillDamage = 50;


        switch (eUnitGrade)
        {
            case EUnitGrade.C
            : lastUpgrade = Shared.unitManager.UnitUpgrader.GetUpgradeLevel()[0]; 
                break;
            case EUnitGrade.B: 
                lastUpgrade = Shared.unitManager.UnitUpgrader.GetUpgradeLevel()[0]; 
                break;
            case EUnitGrade.A:
                lastUpgrade = Shared.unitManager.UnitUpgrader.GetUpgradeLevel()[1];
                break;
            case EUnitGrade.S: 
                lastUpgrade = Shared.unitManager.UnitUpgrader.GetUpgradeLevel()[2]; 
                break;
            case EUnitGrade.SS: 
                lastUpgrade = Shared.unitManager.UnitUpgrader.GetUpgradeLevel()[2];
                break;
        }

        Shared.unitManager.UnitUpgrader.MissUpgrade(lastUpgrade, this);

        isSkill = false;
        skillCouroutine = null;
    }

    private void Update()
    {
        unitAI.State();
        aiState = unitAI.aiState;
        ChangeAnim(unitAI.aiState);
    }

    protected internal override void Attack()
    {
        if (isSkill) return;
        base.Attack();
    }

    protected internal void ChangeAnim(EUnitAI _curState)
    {
        switch(_curState) 
        {
            case EUnitAI.CREATE:
                anim.SetInteger("unitAnim", (int)EUnitAnim.IDLE);
                break;
            case EUnitAI.SEARCH:
                anim.SetInteger("unitAnim", (int)EUnitAnim.IDLE);
                break;
            case EUnitAI.ATTACK:
                anim.SetInteger("unitAnim", (int)EUnitAnim.ATTACK);
                break;
            case EUnitAI.SKILL:
                anim.SetInteger("unitAnim", (int)EUnitAnim.SKILL);
                anim.Play("Skill1");
                break;
            case EUnitAI.RESET:
                break;
        }
    }
}

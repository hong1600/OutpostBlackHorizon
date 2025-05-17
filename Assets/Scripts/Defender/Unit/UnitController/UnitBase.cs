using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class UnitBase : DefenderBase
{
    public EUnitGrade eUnitGrade;
    public int lastUpgrade;
    public int skillDamage;

    [SerializeField] EDefenderAI aiState;
    UnitAI unitAI;
    TableUnit tableUnit;
    Animator anim;
    BoxCollider box;

    [SerializeField] GameObject skillBar;
    Transform skillBarParent;
    protected UnitSkillBar unitSkillBar;

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

        skillBarParent = GameUI.instance.SkillBarParent;

        UpdateUpgrade();

        UnitManager.instance.UnitUpgrader.MissUpgrade(lastUpgrade, this);

        isSkill = false;
        skillCouroutine = null;
    }

    private void OnEnable()
    {
        InitSkillBar();
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

    protected virtual IEnumerator StartSkill()
    {
        yield return null;
    }

    protected internal override void LookTarget()
    {
        if (target != null)
        {
            targetColls = target.GetComponentsInChildren<Collider>();

            for (int i = 0; i < targetColls.Length; i++)
            {
                if (targetColls[i].gameObject.CompareTag("Body"))
                {
                    targetHeight = targetColls[i].bounds.center.y;
                    break;
                }
            }

            Vector3 targetVelocity = enemy.GetComponent<Rigidbody>().velocity;

            Vector3 predictionPos = new Vector3
                (target.transform.position.x, target.transform.position.y + 1, target.transform.position.z) + targetVelocity;

            Vector3 dir = (predictionPos - transform.position).normalized;
            dir.y = 0f;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }

    private void UpdateUpgrade()
    {
        switch (eUnitGrade)
        {
            case EUnitGrade.C:
                lastUpgrade = UnitManager.instance.UnitUpgrader.GetUpgradeLevel()[0];
                break;
            case EUnitGrade.B:
                lastUpgrade = UnitManager.instance.UnitUpgrader.GetUpgradeLevel()[0];
                break;
            case EUnitGrade.A:
                lastUpgrade = UnitManager.instance.UnitUpgrader.GetUpgradeLevel()[1];
                break;
            case EUnitGrade.S:
                lastUpgrade = UnitManager.instance.UnitUpgrader.GetUpgradeLevel()[2];
                break;
            case EUnitGrade.SS:
                lastUpgrade = UnitManager.instance.UnitUpgrader.GetUpgradeLevel()[2];
                break;
        }
    }

    private void InitSkillBar()
    {
        if (skillBar == null) return;

        if (eUnitGrade == EUnitGrade.SS || eUnitGrade == EUnitGrade.S)
        {
            skillBar = Instantiate(skillBar, transform.position,
                Quaternion.identity, skillBarParent);

            unitSkillBar = skillBar.GetComponent<UnitSkillBar>();

            unitSkillBar.Init(this);
        }
    }

    protected internal void ChangeAnim(EDefenderAI _curState)
    {
        switch(_curState) 
        {
            case EDefenderAI.CREATE:
                anim.SetInteger("unitAnim", (int)EUnitAnim.IDLE);
                break;
            case EDefenderAI.SEARCH:
                anim.SetInteger("unitAnim", (int)EUnitAnim.IDLE);
                break;
            case EDefenderAI.ATTACK:
                anim.SetInteger("unitAnim", (int)EUnitAnim.ATTACK);
                break;
            case EDefenderAI.SKILL:
                anim.SetInteger("unitAnim", (int)EUnitAnim.SKILL);
                anim.Play("Skill1");
                break;
            case EDefenderAI.RESET:
                break;
        }
    }
}

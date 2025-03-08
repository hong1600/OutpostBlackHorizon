using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public string unitName;
    public int unitIndex;
    public int attackDamage;
    public int attackSpeed;
    public float attackRange;
    public EUnitGrade eUnitGrade;
    public Sprite UnitImg;
    public int lastUpgrade;
    public int skillDamage;

    [SerializeField] EUnitAI aiState;
    UnitAI unitAI;
    TableUnit tableUnit;
    Animator anim;
    BoxCollider box;
    public GameObject skillBar;

    [SerializeField] protected internal GameObject target;
    protected internal float rotationSpeed;
    [SerializeField] protected internal bool isAttack;
    [SerializeField] protected internal bool isSkill;
    [SerializeField] protected internal Coroutine attackCoroutine;
    [SerializeField] protected internal Coroutine skillCouroutine;

    protected virtual void Init(TableUnit.Info _unitData)
    {
        unitAI = new UnitAI();
        unitAI.Init(this);

        anim = this.GetComponent<Animator>();
        box = this.GetComponent<BoxCollider>();

        unitIndex = _unitData.ID;
        unitName = _unitData.Name;
        attackDamage = _unitData.Damage;
        attackSpeed = _unitData.AttackSpeed;
        attackRange = _unitData.AttackRange;
        eUnitGrade = _unitData.Grade;
        UnitImg = Resources.Load<Sprite>(_unitData.ImgPath);
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

        rotationSpeed = 5f;
        isAttack = false;
        isSkill = false;
        attackCoroutine = null;
        skillCouroutine = null;
    }

    private void Update()
    {
        unitAI.State();
        aiState = unitAI.aiState;
        ChangeAnim(unitAI.aiState);
    }

    protected internal GameObject TargetEnemy()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, attackRange,
             LayerMask.GetMask("Enemy"));

        target = null;

        float minDistance = Mathf.Infinity;

        foreach (Collider coll in colls)
        {
            Enemy enemy = coll.GetComponent<Enemy>();

            if (enemy == null || enemy.isDie == true)
                continue;

            float distance = Vector3.Distance(transform.position, coll.transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                target = coll.gameObject;
            }
        }

        return target;
    }

    protected internal void Attack()
    {
        if (isSkill || isAttack) return;

        if (target != null && attackCoroutine == null)
        {
            attackCoroutine = StartCoroutine(StartAttack());
        }
    }

    protected virtual IEnumerator StartAttack()
    {
        isAttack = true;

        Enemy enemy = target.GetComponent<Enemy>();
        
        StartCoroutine(OnDamageEvent(enemy, attackDamage));
        
        if (enemy.isDie)
        {
            target = null;
            attackCoroutine = null;
            isAttack = false;
            Attack();
        }

        yield return new WaitForSeconds(1 / attackSpeed);

        attackCoroutine = null;
        isAttack = false;
        Attack();

        yield return null;
    }

    protected virtual IEnumerator StartSkill()
    {
        yield return null;
    }

    protected virtual IEnumerator OnDamageEvent(Enemy enemy , int _damage) 
    {
        enemy.TakeDamage(_damage);
        yield return null;
    }

    protected internal void LookEnemy()
    {
        if (target != null)
        {
            Vector3 dir = (target.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));

            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        }
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

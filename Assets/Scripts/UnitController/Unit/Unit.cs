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

    public UnitAI unitAI;
    public UnitSkill unitSkill;
    public Animator anim;
    public BoxCollider box;

    public GameObject target;
    public float rotationSpeed;
    public Coroutine attackCoroutine;

    public virtual void Init(UnitData _unitData)
    {
        unitAI = new UnitAI();
        unitAI.Init(this);

        anim = this.GetComponent<Animator>();
        box = this.GetComponent<BoxCollider>();

        unitName = _unitData.unitName;
        unitIndex = _unitData.index;
        attackDamage = _unitData.unitDamage;
        attackSpeed = _unitData.attackSpeed;
        attackRange = _unitData.attackRange;
        eUnitGrade = _unitData.unitGrade;
        UnitImg = _unitData.unitImg;


        switch (eUnitGrade)
        {
            case EUnitGrade.C
            : lastUpgrade = Shared.unitMng.iUnitUpgrader.GetUpgradeLevel()[0]; 
                break;
            case EUnitGrade.B: 
                lastUpgrade = Shared.unitMng.iUnitUpgrader.GetUpgradeLevel()[0]; 
                break;
            case EUnitGrade.A:
                lastUpgrade = Shared.unitMng.iUnitUpgrader.GetUpgradeLevel()[1];
                break;
            case EUnitGrade.S: 
                lastUpgrade = Shared.unitMng.iUnitUpgrader.GetUpgradeLevel()[2]; 
                break;
            case EUnitGrade.SS: 
                lastUpgrade = Shared.unitMng.iUnitUpgrader.GetUpgradeLevel()[2];
                break;
        }

        Shared.unitMng.iUnitUpgrader.MissUpgrade(lastUpgrade, this);

        rotationSpeed = 5f;
    }

    private void Update()
    {
        unitAI.State();
    }

    public GameObject TargetEnemy()
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

    public void Attack()
    {
        if (target != null)
        {
            if (attackCoroutine == null)
            {
                attackCoroutine = StartCoroutine(StartAttack());
            }
        }
        else
        {
            if (attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine);
                attackCoroutine = null;
            }
        }
    }

    IEnumerator StartAttack()
    {
        while (true) 
        {
            if (target == null)
            {
                yield break;
            }

            Enemy enemy = target.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(attackDamage);
            }

            yield return new WaitForSeconds(1 / attackSpeed);
        }
    }

    public void LookEnemy()
    {
        if (target == null) return;

        Vector3 dir = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    public void ChangeAnim(EUnitAI _curState)
    {
        switch(_curState) 
        {
            case EUnitAI.CREATE:
                anim.SetInteger("isAttack", (int)EUnitAnim.IDLE);
                break;
            case EUnitAI.SEARCH:
                anim.SetInteger("isAttack", (int)EUnitAnim.IDLE);
                break;
            case EUnitAI.ATTACK:
                anim.SetInteger("isAttack", (int)EUnitAnim.ATTACK);
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

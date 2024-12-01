using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public UnitType unitType;

    public UnitUpgrader unitUpgrader;
    public IUnitUpgrader iUnitUpgrader;

    public string unitName;
    public int attackDamage;
    public int attackSpeed;
    public float attackRange;
    public int lastUpgrade;
    public eUnitGrade unitGrade;

    public UnitAI unitAI;
    public Animator anim;

    public GameObject target;
    public Coroutine attackCoroutine;

    public virtual void Init(UnitData unitData)
    {
        unitAI = new UnitAI();
        unitAI.Init(this);

        anim = this.GetComponent<Animator>();

        unitUpgrader = GetComponent<UnitUpgrader>();
        iUnitUpgrader = unitUpgrader;

        unitName = unitData.unitName;
        attackDamage = unitData.unitDamage;
        attackSpeed = unitData.attackSpeed;
        attackRange = unitData.attackRange;
        unitGrade = unitData.unitGrade;

        switch (unitGrade)
        {
            case eUnitGrade.C
            : lastUpgrade = iUnitUpgrader.getUpgradeLevel()[1]; 
                break;
            case eUnitGrade.B: 
                lastUpgrade = iUnitUpgrader.getUpgradeLevel()[1]; 
                break;
            case eUnitGrade.S: 
                lastUpgrade = iUnitUpgrader.getUpgradeLevel()[2]; 
                break;
            case eUnitGrade.SS: 
                lastUpgrade = iUnitUpgrader.getUpgradeLevel()[3];
                break;
        }

        iUnitUpgrader.missUpgrade(lastUpgrade, this);
    }

    private void Update()
    {
        unitAI.State();
        changeAnim(unitAI.AIState);
        attack();
        turn();
        targetEnemy(transform);
    }


    public virtual GameObject targetEnemy(Transform playerPos)
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(playerPos.position, attackRange,
            Vector2.zero, 0, LayerMask.GetMask("Enemy"));

        target = null;
        float minDistance = Mathf.Infinity;

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                Enemy enemy = hit.collider.GetComponent<Enemy>();

                if(enemy == null || enemy.isDie == true)
                    continue;

                float distance = Vector3.Distance(playerPos.position, hit.transform.position);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    target = hit.collider.gameObject;
                }
            }
        }

        return target;
    }

    public virtual void attack()
    {
        if (target != null)
        {
            if (attackCoroutine == null)
            {
                attackCoroutine = StartCoroutine(Attack());
            }
        }
        else
        {
            if (attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine);
                attackCoroutine = null;
                anim.SetBool("isAttack", false);
            }
        }
    }

    IEnumerator Attack()
    {
        anim.SetBool("isAttack", true);
        while (true) 
        {
            if (target == null)
            {
                yield break;
            }

            Enemy enemy = target.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.takeDamage(attackDamage);
            }

            yield return new WaitForSeconds(1 / attackSpeed);
        }
    }

    public void turn()
    {
        if (target == null) return;
    }

    public void changeAnim(eUnitAI curState)
    {
        switch(curState) 
        {
            case eUnitAI.eAI_CREATE:
                anim.SetBool("isAttack", false);
                break;

            case eUnitAI.eAI_SEARCH:
                anim.SetBool("isAttack", false);
                break;
            case eUnitAI.eAI_ATTACK:
                anim.SetBool("isAttack", true);
                break;
            case eUnitAI.eAI_RESET:
                break;
        }
    }
}

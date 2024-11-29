using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType
{
    SS,
    S,
    A,
    B,
    C
}

public abstract class Unit : MonoBehaviour
{
    public UnitType unitType;

    public UnitUpgrader unitUpgrader;
    public IUnitUpgrader iUnitUpgrader;

    public string unitName;
    public int attackDamage;
    public int attackSpeed;
    public float attackRange;
    public float unitGrade;
    public int lastUpgrade;

    public Animator anim;
    public SpriteRenderer sprite;

    public GameObject target;
    public Coroutine attackCoroutine;

    public AIBase aiBase;

    public virtual void Init(UnitData unitData)
    {
        iUnitUpgrader = unitUpgrader;
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        unitName = unitData.unitName;
        attackDamage = unitData.unitDamage;
        attackSpeed = unitData.attackSpeed;
        attackRange = unitData.attackRange;
        unitGrade = unitData.unitGrade;

        switch (unitGrade)
        {
            case 0: lastUpgrade = iUnitUpgrader.getUpgradeLevel()[1]; break;
            case 1: lastUpgrade = iUnitUpgrader.getUpgradeLevel()[1]; break;
            case 2: lastUpgrade = iUnitUpgrader.getUpgradeLevel()[2]; break;
            case 3: lastUpgrade = iUnitUpgrader.getUpgradeLevel()[3]; break;
        }

        iUnitUpgrader.missUpgrade(lastUpgrade, this);
    }

    private void Update()
    {
        attack();
        turn();
        targetEnemy(transform);
    }

    public GameObject targetEnemy(Transform playerPos)
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

    public void attack()
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

        if (target.transform.position.x < transform.position.x)
        {
            sprite.flipX = true;
        }
        else if (target.transform.position.x > transform.position.x)
        {
            sprite.flipX = false;
        }
    }
}

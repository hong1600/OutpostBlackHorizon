using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public UnitType unitType;

    public UnitData unitData;
    public UnitUpgrader unitUpgrader;
    public IUnitUpgrader iUnitUpgrader;

    public string unitName;
    public int attackDamage;
    public int attackSpeed;
    public float attackRange;
    public float unitGrade;

    public Animator anim;
    public SpriteRenderer sprite;

    public GameObject target;

    public Coroutine attackCoroutine;

    public float lastUpgrade;
    public bool upgrade2;
    public bool upgrade3;
    public bool upgrade4;
    public bool upgrade5;
    public bool upgrade6;

    private void Awake()
    {
        iUnitUpgrader = unitUpgrader;
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        unitName = unitData.unitName;
        attackDamage = unitData.unitDamage;
        attackSpeed = unitData.attackSpeed;
        attackRange = unitData.attackRange;
        unitGrade = unitData.unitGrade;

        upgrade2 = false;
        upgrade3 = false;
        upgrade4 = false;
        upgrade5 = false;
        upgrade6 = false;

        switch (unitGrade)
        {
            case 0: lastUpgrade = iUnitUpgrader.getUpgradeLevel()[1]; break;
            case 1: lastUpgrade = iUnitUpgrader.getUpgradeLevel()[1]; break;
            case 2: lastUpgrade = iUnitUpgrader.getUpgradeLevel()[2]; break;
            case 3: lastUpgrade = iUnitUpgrader.getUpgradeLevel()[3]; break;
        }
        missUpgrade(lastUpgrade);
    }

    private void Update()
    {
        attack();
        turn();
        targetEnemy(transform);
    }

    private GameObject targetEnemy(Transform playerPos)
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

    private void attack()
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

    private void turn()
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

    public void upgrade()
    {
        float upgradeLevel = 0;

        switch(unitGrade) 
        {
            case 0: upgradeLevel = iUnitUpgrader.getUpgradeLevel()[0]; break;
            case 1: upgradeLevel = iUnitUpgrader.getUpgradeLevel()[0]; break;
            case 2: upgradeLevel = iUnitUpgrader.getUpgradeLevel()[1]; break;
            case 3: upgradeLevel = iUnitUpgrader.getUpgradeLevel()[2]; break;
        }

        switch (upgradeLevel)
        {
            case 2: if (upgrade2 == false) { attackDamage *= 2; upgrade2 = true; } break;
            case 3: if (upgrade3 == false) { attackDamage *= 2; upgrade3 = true; } break;
            case 4: if (upgrade4 == false) { attackDamage *= 2; upgrade4 = true; } break;
            case 5: if (upgrade5 == false) { attackDamage *= 2; upgrade5 = true; } break;
            case 6: if (upgrade6 == false) { attackDamage *= 2; upgrade6 = true; } break;
        }
    }

    private void missUpgrade(float curUpgradeLevel)
    {
        for(int i = 1; i <= curUpgradeLevel; i++) 
        {
            switch(i) 
            {
                case 2: if (upgrade2 == false) { attackDamage *= 2; upgrade2 = true; } break;
                case 3: if (upgrade3 == false) { attackDamage *= 2; upgrade3 = true; } break;
                case 4: if (upgrade4 == false) { attackDamage *= 2; upgrade4 = true; } break;
                case 5: if (upgrade5 == false) { attackDamage *= 2; upgrade5 = true; } break;
                case 6: if (upgrade6 == false) { attackDamage *= 2; upgrade6 = true; } break;
            }
        }
    }


}

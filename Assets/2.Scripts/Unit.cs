using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] UnitData unitData;

    string unitName;
    public int attackDamage;
    public int attackSpeed;
    public float attackRange;
    public float unitGrade;

    Animator anim;
    SpriteRenderer sprite;

    [SerializeField] GameObject target;

    Coroutine attackCoroutine;

    bool upgrade2 = false;
    bool upgrade3 = false;
    bool upgrade4 = false;
    bool upgrade5 = false;
    bool upgrade6 = false;

    private void Awake()
    {
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

        upgrade();
    }

    private void Update()
    {
        attack();
        turn();
        targetEnemy(transform);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, attackRange);
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
        if (unitGrade == 0)
        {
            switch ((GameManager.Instance.UpgradeLevel1))
            {
                case 2:
                    if (upgrade2 == false)
                    {
                        attackDamage *= 2;
                        upgrade2 = true;
                    }
                    break;
                case 3:
                    if (upgrade3 == false)
                    {
                        attackDamage *= 2;
                        upgrade2 = true;
                    }
                    break;
                case 4:
                    if (upgrade4 == false)
                    {
                        attackDamage *= 2;
                        upgrade2 = true;
                    }
                    break;
                case 5:
                    if (upgrade5 == false)
                    {
                        attackDamage *= 2;
                        upgrade2 = true;
                    }
                    break;
                case 6:
                    if (upgrade6 == false)
                    {
                        attackDamage *= 2;
                        upgrade2 = true;
                    }
                    break;
            }
        }
        if (unitGrade == 1)
        {
            switch ((GameManager.Instance.UpgradeLevel2))
            {
                case 2:
                    if (upgrade2 == false)
                    {
                        attackDamage *= 2;
                        upgrade2 = true;
                    }
                    break;
                case 3:
                    if (upgrade3 == false)
                    {
                        attackDamage *= 2;
                        upgrade2 = true;
                    }
                    break;
                case 4:
                    if (upgrade4 == false)
                    {
                        attackDamage *= 2;
                        upgrade2 = true;
                    }
                    break;
                case 5:
                    if (upgrade5 == false)
                    {
                        attackDamage *= 2;
                        upgrade2 = true;
                    }
                    break;
                case 6:
                    if (upgrade6 == false)
                    {
                        attackDamage *= 2;
                        upgrade2 = true;
                    }
                    break;
            }
        }
        if (unitGrade == 2)
        {
            switch ((GameManager.Instance.UpgradeLevel3))
            {
                case 2:
                    if (upgrade2 == false)
                    {
                        attackDamage *= 2;
                        upgrade2 = true;
                    }
                    break;
                case 3:
                    if (upgrade3 == false)
                    {
                        attackDamage *= 2;
                        upgrade2 = true;
                    }
                    break;
                case 4:
                    if (upgrade4 == false)
                    {
                        attackDamage *= 2;
                        upgrade2 = true;
                    }
                    break;
                case 5:
                    if (upgrade5 == false)
                    {
                        attackDamage *= 2;
                        upgrade2 = true;
                    }
                    break;
                case 6:
                    if (upgrade6 == false)
                    {
                        attackDamage *= 2;
                        upgrade2 = true;
                    }
                    break;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] UnitData unitData;

    string unitName;
    [SerializeField] int attackDamage;
    [SerializeField] int attackSpeed;
    [SerializeField] float attackRange;
    [SerializeField] float unitGrade;

    Animator anim;
    SpriteRenderer sprite;

    [SerializeField] GameObject target;

    Coroutine attackCoroutine;

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
}

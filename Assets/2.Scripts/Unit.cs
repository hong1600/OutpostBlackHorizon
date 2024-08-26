using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] UnitData unitData;

    string unitName;
    int attackDamage;
    int attackSpeed;
    float attackRange;
    UnitGrade unitGrade;

    Animator anim;
    SpriteRenderer sprite;

    [SerializeField] bool attackReady;

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
        checkEnemy();
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

        GameObject target = null;

        float minDistance = Mathf.Infinity;

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
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

    private void checkEnemy()
    {
        if (Physics2D.CircleCast(transform.position, attackRange,
            Vector2.zero, 0, LayerMask.GetMask("Enemy")))
        {
            attackReady = true;
        }
        else { attackReady = false; }
    }

    private void attack()
    {
        if (attackReady == true)
        {
            StartCoroutine(Attack());
            anim.SetBool("isAttack", true);
        }
        else
        {
            StopCoroutine(Attack());
            anim.SetBool("isAttack", false);
        }
    }

    IEnumerator Attack()
    {
        GameObject target = targetEnemy(transform);

        while (true) 
        {
            Enemy enemy = target.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.takeDamage(attackDamage);
            }

            yield return new WaitForSeconds(1.5f / attackSpeed);
        }
    }

    private void turn()
    {
        if (targetEnemy(transform) == null) return;

        if (targetEnemy(transform).transform.position.x < transform.position.x)
        {
            sprite.flipX = true;
        }
        else if (targetEnemy(transform).transform.position.x > transform.position.x)
        {
            sprite.flipX = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] UnitData unitData;

    string unitName;
    float unitDamage;
    int attackSpeed;
    float attackRange;
    UnitGrade unitGrade;

    Animator anim;

    [SerializeField] float distance = 1;
    [SerializeField] bool attackReady;


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        unitName = unitData.unitName;
        unitDamage = unitData.unitDamage;
        attackSpeed = unitData.attackSpeed;
        attackRange = unitData.attackRange;
        unitGrade = unitData.unitGrade;
    }

    private void Update()
    {
        checkEnemy();
        attack();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, attackRange);
    }

    private void checkEnemy()
    {
        if (Physics2D.CircleCast(transform.position, attackRange,
            Vector2.up, distance, LayerMask.GetMask("Enemy")))
        {
            attackReady = true;
        }
        else { attackReady = false; }
    }

    private void attack()
    {
        if (attackReady) 
        {
            anim.SetBool("isAttack", true);
        }
        else
        {
            anim.SetBool("isAttack", false);
        }
    }
}

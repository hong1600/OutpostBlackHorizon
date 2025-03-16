using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class Defender : MonoBehaviour
{
    protected DefenderAI defenderAI;
    protected EffectPool effectPool;

    public EDefenderAI curState;

    public int defenderID;
    public string defenderName;
    public int attackDamage;
    public float attackSpeed;
    public float attackRange;
    public Sprite defenderImg;

    public GameObject target { get; private set; }

    protected float rotationSpeed;
    protected bool isAttack;
    protected Coroutine attackCoroutine;

    protected virtual void Init(int _id, string _name, int _dmg, 
        float _spd, float _range, string _imgPath, bool _isAI)
    {
        if (_isAI)
        {
            defenderAI = new DefenderAI();
            defenderAI.Init(this);
        }

        defenderID = _id;
        defenderName = _name;
        attackDamage = _dmg;
        attackSpeed = _spd;
        attackRange = _range;
        defenderImg = Resources.Load<Sprite>(_imgPath);

        rotationSpeed = 2.5f;
        isAttack = false;
        attackCoroutine = null;

        effectPool = Shared.objectPoolManager.EffectPool;
    }

    private void Update()
    {
        defenderAI.State();
        curState = defenderAI.aiState;
    }

    protected internal GameObject TargetEnemy()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, attackRange,
             LayerMask.GetMask("Enemy"));

        target = null;

        float minDistance = Mathf.Infinity;

        foreach (Collider coll in colls)
        {
            Enemy enemy = coll.GetComponentInParent<Enemy>();

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

    protected internal virtual void Attack()
    {
        if (isAttack) return;

        if (target != null && attackCoroutine == null)
        {
            attackCoroutine = StartCoroutine(StartAttack());
        }
    }

    protected virtual IEnumerator StartAttack()
    {
        isAttack = true;

        Enemy enemy = target.GetComponentInParent<Enemy>();

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

    protected virtual IEnumerator OnDamageEvent(Enemy _enemy, int _dmg)
    {
        ITakeDmg iTakeDmg = target.GetComponentInParent<ITakeDmg>();

        if (iTakeDmg != null)
        {
            iTakeDmg.TakeDmg(_dmg, false);
        }

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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.3f);
        Gizmos.DrawSphere(transform.position, attackRange);
    }
}

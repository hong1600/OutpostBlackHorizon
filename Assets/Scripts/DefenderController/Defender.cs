using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class Defender : MonoBehaviour
{
    EDefenderAI curState;

    protected Enemy enemy;
    protected DefenderAI defenderAI;
    protected BulletPool bulletPool;
    protected EffectPool effectPool;

    public int defenderID;
    public string defenderName;
    public int attackDamage;
    public float attackSpeed;
    public float attackRange;
    public Sprite defenderSprite;
    protected float rotationSpeed;
    protected bool isAttack;
    protected Coroutine attackCoroutine;

    public GameObject target { get; private set; }
    protected Collider[] targetColls;
    protected float targetHeight;

    protected virtual void Init(int _id, string _name, int _dmg, 
        float _spd, float _range, string _sprite, bool _isAI)
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
        defenderSprite = SpriteManager.instance.GetSprite(_sprite);

        rotationSpeed = 2.5f;
        isAttack = false;
        attackCoroutine = null;

        bulletPool = ObjectPoolManager.instance.BulletPool;
        effectPool = ObjectPoolManager.instance.EffectPool;
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
        targetColls = null;

        float minDistance = Mathf.Infinity;

        foreach (Collider coll in colls)
        {
            enemy = coll.GetComponentInParent<Enemy>();

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

        yield return StartCoroutine(OnDamageEvent(enemy, attackDamage));

        if (enemy.isDie)
        {
            target = null;
            attackCoroutine = null;
            isAttack = false;
            Attack();
        }

        yield return new WaitForSeconds(1 * attackSpeed);

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

    protected internal virtual void LookTarget()
    {
        if (target != null)
        {
            if (targetColls == null)
            {
                targetColls = target.GetComponentsInChildren<Collider>();

                for (int i = 0; i < targetColls.Length; i++)
                {
                    if (targetColls[i].gameObject.CompareTag("Body"))
                    {
                        targetHeight = targetColls[i].bounds.center.y;
                        break;
                    }
                }
            }

            Vector3 targetVelocity = enemy.GetComponent<Rigidbody>().velocity;

            Vector3 predictionPos = new Vector3
                (target.transform.position.x, targetHeight, target.transform.position.z) + targetVelocity * 2f;

            Vector3 dir = (predictionPos - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.3f);
        Gizmos.DrawSphere(transform.position, attackRange);
    }
}

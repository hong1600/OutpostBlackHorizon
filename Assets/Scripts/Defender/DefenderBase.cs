using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class DefenderBase : MonoBehaviour
{
    protected DefenderAIState curState;

    protected AudioManager audioManager;

    protected EnemyBase enemy;
    protected BulletPool bulletPool;
    protected EffectPool effectPool;

    public int defenderID;
    public string defenderName;
    public int attackDamage;
    public float attackSpeed;
    public float attackRange;
    public Sprite defenderSprite;
    protected float rotationSpeed;
    public bool isAttack { get; private set; }
    protected Coroutine attackCoroutine;

    public GameObject target { get; private set; }
    protected Collider[] targetColls;
    protected float targetHeight;

    protected virtual void Init(int _id, string _name, int _dmg, 
        float _spd, float _range, string _sprite, bool _isAI)
    {
        audioManager = AudioManager.instance;

        if (_isAI)
        {
            SetState(new DefenderCreateState(this));
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
        if (curState != null)
        {
            curState.Execute();
        }
    }

    protected internal GameObject TargetEnemy()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, attackRange,
             LayerMask.GetMask("Enemy"));

        target = null;
        targetColls = null;

        float minDistance = Mathf.Infinity;

        for (int i = 0; i < colls.Length; i++)
        {
            EnemyBase enemy = colls[i].GetComponentInParent<EnemyBase>();

            if (enemy == null || enemy.isDie == true)
                continue;

            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                target = enemy.gameObject.transform.parent.gameObject;
            }
        }

        if (target == null)
        {
            isAttack = false;
        }

        return target;
    }

    protected internal virtual void Attack()
    {
        if (isAttack || target == null) return;

        attackCoroutine = StartCoroutine(StartAttack());
    }

    protected virtual IEnumerator StartAttack()
    {
        isAttack = true;

        yield return StartCoroutine(OnDamageEvent(enemy, attackDamage));

        yield return new WaitForSeconds(1 * attackSpeed);

        isAttack = false;
        attackCoroutine = null;
    }

    protected virtual IEnumerator OnDamageEvent(EnemyBase _enemy, int _dmg)
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
            targetColls = target.GetComponentsInChildren<Collider>();

            for (int i = 0; i < targetColls.Length; i++)
            {
                if (targetColls[i].gameObject.CompareTag("Body"))
                {
                    targetHeight = targetColls[i].bounds.center.y;
                    break;
                }
            }

            Vector3 targetVelocity = enemy.GetComponent<Rigidbody>().velocity;

            Vector3 predictionPos = new Vector3
                (target.transform.position.x, target.transform.position.y + 1, target.transform.position.z) + targetVelocity;

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

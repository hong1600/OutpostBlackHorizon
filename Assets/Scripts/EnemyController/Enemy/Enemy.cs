using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Enemy : MonoBehaviour, ITakeDmg
{
    public event Action onTakeDamage;

    [SerializeField] protected EEnemyAI aiState;
    protected EEnemy eEnemy;

    [SerializeField] protected SphereCollider sensor;
    [SerializeField] protected BoxCollider box;
    [SerializeField] protected Material hitMat;
    [SerializeField] protected Renderer render;
    protected Material originMat;
    protected Rigidbody rigid;
    protected Animator anim;
    SkinnedMeshRenderer skinRender;

    protected EnemyAI enemyAI;
    protected EnemyHpBar enemyHpBar;
    protected EnemyPool enemyPool;
    protected HpBarPool hpBarPool;
    protected EffectPool effectPool;
    protected Rewarder rewarder;
    protected EnemySpawner enemySpawner;
    protected GoldCoin goldCoin;
    protected Round round;
    protected Timer timer;
    protected BulletPool bulletPool;

    public string enemyName { get; private set; }
    public float maxHp { get; private set; }
    public float curhp;
    public float enemySpeed { get; private set; }
    public float rotationSpeed { get; private set; }
    public float attackRange { get; private set; }
    public float attackDmg { get; private set; }

    [SerializeField] protected Transform myTarget;
    Transform centerPoint;
    protected Vector3 targetPointDir;

    public bool isStay { get; private set; }
    public bool attackReady { get; private set; }
    public bool isDie { get; protected set; }
    bool isOneShot = true;
    protected bool isAttack;
    bool isReset;

    protected Coroutine attackCoroutine;
    Vector3 hpBarPos;

    public virtual void Init(string _name, float _maxHp, float _spd, float _range, float _dmg, EEnemy _eEnemy)
    {
        enemyAI = new EnemyAI();
        enemyAI.Init(this);

        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        if (render != null)
        {
            originMat = render.material;
        }

        skinRender = GetComponentInChildren<SkinnedMeshRenderer>();

        enemySpawner = EnemyManager.instance.EnemySpawner;
        goldCoin = GameManager.instance.GoldCoin;
        round = GameManager.instance.Round;
        timer = GameManager.instance.Timer;
        rewarder = GameManager.instance.Rewarder;
        bulletPool = ObjectPoolManager.instance.BulletPool;
        hpBarPool = ObjectPoolManager.instance.HpBarPool;
        enemyPool = ObjectPoolManager.instance.EnemyPool;
        effectPool = ObjectPoolManager.instance.EffectPool;

        if (skinRender != null)
        {
            GameObject hpBar = hpBarPool.FindHpbar(EHpBar.NORMAL, skinRender.bounds.center +
                new Vector3(0, skinRender.bounds.extents.y + 0.5f, 0), Quaternion.identity);

            enemyHpBar = hpBar.GetComponent<EnemyHpBar>();

            enemyHpBar.Init(this, skinRender);
        }

        enemyName = _name;
        maxHp = _maxHp;
        enemySpeed = _spd;
        attackRange = _range;
        attackDmg = _dmg;
        curhp = maxHp;
        rotationSpeed = 5;
        eEnemy = _eEnemy;
        centerPoint = enemySpawner.GetCenterPoint();

        myTarget = centerPoint;
    }

    private void OnDisable()
    {
        ResetState();
        enemyAI.aiState = EEnemyAI.CREATE;
    }

    private void OnEnable()
    {
        ResetState();

        if (skinRender != null)
        {
            GameObject hpBar = hpBarPool.FindHpbar(EHpBar.NORMAL, skinRender.bounds.center +
                new Vector3(0, skinRender.bounds.extents.y + 0.5f, 0), Quaternion.identity);

            enemyHpBar = hpBar.GetComponent<EnemyHpBar>();

            enemyHpBar.Init(this, skinRender);
        }
    }

    private void ResetState()
    {
        isReset = false;
        myTarget = centerPoint;
        curhp = maxHp;
        isAttack = false;
        isDie = false;
        isStay = false;
        isOneShot = true;
        attackReady = false;
        attackCoroutine = null;
        StopAllCoroutines();
        isReset = true;
    }

    protected virtual void Update()
    {
        if (enemyAI == null && isReset) return;

        aiState = enemyAI.aiState;

        if (enemyAI != null)
        {
            enemyAI.State();
            ChangeAnim(enemyAI.aiState);
        }
    }

    protected void Move()
    {
        if (myTarget == null) return;

        targetPointDir = (myTarget.transform.position - transform.position).normalized;

        rigid.MovePosition(transform.position + targetPointDir * enemySpeed * Time.fixedDeltaTime);
    }

    protected void Turn()
    {
        Quaternion rotation = Quaternion.LookRotation(new Vector3(targetPointDir.x, 0, targetPointDir.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation,
            rotationSpeed * Time.deltaTime);
    }

    protected virtual void CheckTarget()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position,
            sensor.radius * transform.localScale.x, LayerMask.GetMask("Player", "Field"));

        if(colls.Length > 0) 
        {
            int highPriority = int.MinValue;
            Transform highTargetTrs = null;

            for(int i = 0; i < colls.Length; i++) 
            {
                int priority = GetTargetPriority(colls[i]);

                if (priority > highPriority)
                {
                    highPriority = priority;
                    highTargetTrs = colls[i].transform;
                }
            }

            if (highTargetTrs != null)
            {
                myTarget = highTargetTrs;
            }
        }
    }

    protected virtual int GetTargetPriority(Collider _target)
    {
        if (_target.gameObject.layer == LayerMask.NameToLayer("Field")) return 2;
        if (_target.gameObject.layer == LayerMask.NameToLayer("Player")) return 1;
        return 0;
    }

    protected void ReadyAttack()
    {
        if(myTarget == null) return;

        Collider targetColl = myTarget.GetComponent<Collider>();
        if(targetColl == null) return;

        Vector3 closePoint = targetColl.ClosestPoint(transform.position);
        float distance = Vector3.Distance(closePoint, transform.position);

        if (distance < attackRange)
        {
            attackReady = true;
        }
        else
        {
            attackReady = false;
        }
    }

    protected virtual void Attack()
    {
        if (attackCoroutine == null && !isAttack)
        {
            attackCoroutine = StartCoroutine(StartAttack());
        }
    }

    protected virtual IEnumerator StartAttack()
    {
        isAttack = true;

        if (myTarget != null)
        {
            ITakeDmg iTakeDmg = myTarget.gameObject.GetComponent<ITakeDmg>();

            if (iTakeDmg != null)
            {
                iTakeDmg.TakeDmg(attackDmg, false);
            }
        }

        yield return new WaitForSeconds(2);

        isAttack = false;
        attackCoroutine = null;
    }

    public virtual void TakeDmg(float _dmg, bool _isHead)
    {
        if (isDie) return;

        curhp -= _dmg;

        if(hitMat != null && render != null) 
        {
            render.material = hitMat;

            Invoke(nameof(UpdateMat), 0.2f);
        }

        onTakeDamage?.Invoke();

        if (curhp <= 0)
        {
            isDie = true;
        }
    }

    protected void TakeDmgEvent()
    {
        onTakeDamage?.Invoke();
    }

    private void UpdateMat()
    {
        render.material = originMat;
    }

    public void StayEnemy(float _time)
    {
        StartCoroutine(StartStay(_time));
    }

    IEnumerator StartStay(float _time)
    {
        isStay = true;
        enemyAI.aiState = EEnemyAI.STAY;

        yield return new WaitForSeconds(_time);

        isStay = false;
    }

    protected virtual void Die()
    {
        StartCoroutine(StartDie());
    }

    protected virtual IEnumerator StartDie()
    {
        GameObject explosionObj = effectPool.FindEffect(EEffect.ENEMYEXPLOSION, box.bounds.center, Quaternion.identity);

        rewarder.SetReward(EReward.GOLD, 50);
        rewarder.SetReward(EReward.GEM, 10);
        rewarder.SetReward(EReward.PAPER, 20);
        rewarder.SetReward(EReward.EXP, 1);

        if (enemyHpBar != null)
        {
            hpBarPool.ReturnHpBar(EHpBar.NORMAL, enemyHpBar.gameObject);
        }

        yield return new WaitForSeconds(1f);

        effectPool.ReturnEffect(EEffect.ENEMYEXPLOSION, explosionObj);
        enemyPool.ReturnEnemy(eEnemy, this.gameObject);
    }

    protected virtual void ChangeAnim(EEnemyAI _curState)
    {
        switch (_curState)
        {
            case EEnemyAI.CREATE:
                anim.SetInteger("EnemyAnim", (int)EEnemyAnim.IDLE);
                break;
            case EEnemyAI.MOVE:
                anim.SetInteger("EnemyAnim", (int)EEnemyAnim.WALK);
                break;
            case EEnemyAI.ATTACK:
                anim.SetInteger("EnemyAnim", (int)EEnemyAnim.ATTACK);
                break;
            case EEnemyAI.STAY:
                anim.SetInteger("EnemyAnim", (int)EEnemyAnim.IDLE);
                break;
            case EEnemyAI.DIE:
                anim.SetInteger("EnemyAnim", (int)EEnemyAnim.DIE);
                break;
        }
    }

    public void MoveAI()
    {
        if (myTarget == null)
        {
            attackReady = false;
            myTarget = centerPoint;
        }

        CheckTarget();
        ReadyAttack();

        if (isDie)
            return;

        Move();
        Turn();
    }

    public void AttackAI()
    {
        ReadyAttack();

        if (myTarget == null || !myTarget.gameObject.activeInHierarchy || !attackReady)
        {
            enemyAI.aiState = EEnemyAI.MOVE;
            return;
        }

        Attack();
        Turn();
    }

    public void DieAI()
    {
        if (isDie && isOneShot)
        {
            Die();
            isOneShot = false;
        }
    }
}

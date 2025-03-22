using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Enemy : MonoBehaviour, ITakeDmg
{
    public event Action onTakeDamage;

    EEnemyAI aiState;
    EEnemy eEnemy;

    Rigidbody rigid;
    Animator anim;
    [SerializeField] Renderer render;
    [SerializeField] Material hitMat;
    Material originMat;

    EnemyAI enemyAI;
    EnemyHpBar enemyHpBar;
    EnemyPool enemyPool;
    HpBarPool hpBarPool;
    EffectPool effectPool;
    Rewarder rewarder;

    [SerializeField] SphereCollider sensor;
    [SerializeField] BoxCollider box;
    SkinnedMeshRenderer skinRender;

    protected EnemySpawner enemySpawner;
    protected GoldCoin goldCoin;
    protected Round round;
    protected SpawnTimer spawnTimer;
    protected BulletPool bulletPool;

    public string enemyName { get; private set; }
    public float enemyHp { get; private set; }
    public float curhp { get; private set; }
    public float enemySpeed { get; private set; }
    public float rotationSpeed { get; private set; }
    public float attackRange { get; private set; }
    public float attackDmg { get; private set; }

    protected Transform[] targetPoints;
    protected Transform myTarget;
    protected Vector3 targetPointDir;
    Vector3 hpBarPos;

    internal bool isDie { get; private set; }
    internal bool isStay { get; private set; }
    protected internal bool attackReady { get; private set; }
    protected bool isAttack;

    protected Coroutine attackCoroutine;

    protected virtual void InitEnemyData(string _name, float _maxHp, float _spd,
        float _range, float _dmg, EEnemy _eEnemy)
    {
        enemyAI = new EnemyAI();
        enemyAI.Init(this);

        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        originMat = render.material;
        skinRender = GetComponentInChildren<SkinnedMeshRenderer>();

        enemySpawner = Shared.enemyManager.EnemySpawner;
        goldCoin = Shared.gameManager.GoldCoin;
        round = Shared.gameManager.Round;
        spawnTimer = Shared.gameManager.SpawnTimer;
        bulletPool = Shared.objectPoolManager.BulletPool;
        hpBarPool = Shared.objectPoolManager.HpBarPool;
        enemyPool = Shared.objectPoolManager.EnemyPool;
        effectPool = Shared.objectPoolManager.EffectPool;
        rewarder = Shared.gameManager.Rewarder;


        enemyName = _name;
        enemyHp = _maxHp;
        enemySpeed = _spd;
        attackRange = _range;
        attackDmg = _dmg;
        curhp = enemyHp;
        rotationSpeed = 5;

        targetPoints = enemySpawner.GetTargetPoint();
        int rand = Random.Range(0, targetPoints.Length);
        myTarget = targetPoints[rand];
    }

    private void OnEnable()
    {
        curhp = enemyHp;
        enemyAI.isDie = false;

        GameObject hpBar = hpBarPool.FindHpbar(EHpBar.NORMAL, skinRender.bounds.center + 
            new Vector3(0, skinRender.bounds.extents.y + 0.5f, 0), Quaternion.identity);
        enemyHpBar = hpBar.GetComponent<EnemyHpBar>();
        enemyHpBar.Init(this, skinRender);

        attackReady = false;
        isAttack = false;
        isDie = false;
        isStay = false;
    }

    private void Update()
    {
        if (enemyAI == null) return;

        aiState = enemyAI.aiState;

        if (enemyAI != null)
        {
            enemyAI.State();
            ChangeAnim(enemyAI.aiState);
        }
    }

    protected internal void Move()
    {
        targetPointDir = (myTarget.transform.position - transform.position).normalized;

        rigid.MovePosition(transform.position + targetPointDir * enemySpeed * Time.fixedDeltaTime);
    }

    protected internal void Turn()
    {
        Quaternion rotation = Quaternion.LookRotation(new Vector3(targetPointDir.x, 0, targetPointDir.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation,
            rotationSpeed * Time.deltaTime);
    }

    protected internal virtual void CheckTarget()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position,
            sensor.radius, LayerMask.GetMask("Player", "Field"));

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

    protected internal void ReadyAttack()
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

    protected internal virtual void Attack()
    {
        if (attackCoroutine == null && !isAttack)
        {
            attackCoroutine = StartCoroutine(StartAttack());
        }
    }

    protected abstract IEnumerator StartAttack();

    public void TakeDmg(float _dmg, bool _isHead)
    {
        curhp -= _dmg;
        render.material = hitMat;
        Invoke(nameof(UpdateMat), 0.2f);
        onTakeDamage?.Invoke();

        if (curhp <= 0)
        {
            isDie = true;
        }
    }

    private void UpdateMat()
    {
        render.material = originMat;
    }

    protected internal void StayEnemy(float _time)
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

    protected internal virtual void Die()
    {
        StartCoroutine(StartDie());
    }

    IEnumerator StartDie()
    {
        GameObject explosionObj = effectPool.FindEffect(EEffect.ENEMYEXPLOSION, box.bounds.center, Quaternion.identity);

        rewarder.SetReward(EReward.GOLD, 50);
        rewarder.SetReward(EReward.GEM, 10);
        rewarder.SetReward(EReward.PAPER, 20);
        rewarder.SetReward(EReward.EXP, 1);

        yield return new WaitForSeconds(1f);

        effectPool.ReturnEffect(EEffect.ENEMYEXPLOSION, explosionObj);
        hpBarPool.ReturnHpBar(EHpBar.NORMAL, enemyHpBar.gameObject);
        enemyPool.ReturnEnemy(eEnemy, this.gameObject);
    }

    protected void ChangeAnim(EEnemyAI _curState)
    {
        _curState = aiState;

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
}

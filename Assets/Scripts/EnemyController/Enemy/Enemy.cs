using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public abstract class Enemy : MonoBehaviour, ITakeDmg
{
    public event Action onTakeDamage;

    EEnemyAI aiState;

    EnemyAI enemyAI;
    EnemyHpBar enemyHpBar;
    protected EnemySpawner enemySpawner;
    protected GoldCoin goldCoin;
    protected Round round;
    protected SpawnTimer spawnTimer;
    protected BulletPool bulletPool;

    Rigidbody rigid;
    [SerializeField] SphereCollider sensor;
    [SerializeField] BoxCollider box;
    Animator anim;


    public string enemyName { get; private set; }
    public float enemyHp { get; private set; }
    public float curhp { get; private set; }
    public float enemySpeed { get; private set; }
    public float rotationSpeed { get; private set; }
    public float attackRange { get; private set; }
    public int attackDmg { get; private set; }

    GameObject hitAim;
    Image hitAimImg;
    Coroutine hitAimCoroutine;

    protected Transform[] targetPoints;
    protected Transform myTarget;
    protected Vector3 targetPointDir;

    internal bool isDie { get; private set; }
    internal bool isStay { get; private set; }
    protected internal bool attackReady { get; private set; }
    protected bool isAttack;

    protected Coroutine attackCoroutine;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = this.GetComponent<Animator>();

        enemyAI = new EnemyAI();
        enemyAI.Init(this);
    }

    public void InitEnemyData(TableEnemy.Info _enemyData)
    {
        enemySpawner = Shared.enemyManager.EnemySpawner;
        goldCoin = Shared.gameManager.GoldCoin;
        round = Shared.gameManager.Round;
        spawnTimer = Shared.gameManager.SpawnTimer;
        bulletPool = Shared.objectPoolManager.BulletPool;

        enemyName = _enemyData.Name;
        enemyHp = _enemyData.MaxHp;
        enemySpeed = _enemyData.Speed;
        attackRange = _enemyData.AttackRange;
        attackDmg = _enemyData.AttackDmg;
        curhp = enemyHp;
        rotationSpeed = 5;

        targetPoints = enemySpawner.GetTargetPoint();
        int rand = Random.Range(0, targetPoints.Length);
        myTarget = targetPoints[rand];

        hitAim = Shared.gameUI.HitAim;
        hitAimImg = hitAim.GetComponent<Image>();
    }

    private void OnEnable()
    {
        curhp = enemyHp;

        GameObject hpBar = Shared.objectPoolManager.HpBarPool.FindHpBar(EHpBar.NORMAL);
        enemyHpBar = hpBar.GetComponent<EnemyHpBar>();
        enemyHpBar.Init(this);

        attackReady = false;
        isAttack = false;
        isDie = false;
        isStay = false;
    }

    private void Update()
    {
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

        hitAimImg.color = _isHead ? Color.red : Color.white;
        hitAim.SetActive(true);
        Invoke(nameof(HideHitAim), 0.3f);

        onTakeDamage?.Invoke();

        if (curhp <= 0)
        {
            isDie = true;
        }
    }

    private void HideHitAim()
    {
        hitAim.SetActive(false);
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
        Shared.gameManager.Rewarder.SetReward(EReward.GOLD, 50);
        Shared.gameManager.Rewarder.SetReward(EReward.GEM, 10);
        Shared.gameManager.Rewarder.SetReward(EReward.PAPER, 20);
        Shared.gameManager.Rewarder.SetReward(EReward.EXP, 1);

        yield return new WaitForSeconds(1f);

        Shared.objectPoolManager.ReturnObject(enemyHpBar.gameObject.name, enemyHpBar.gameObject);
        Shared.objectPoolManager.ReturnObject(this.gameObject.name, this.gameObject);
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

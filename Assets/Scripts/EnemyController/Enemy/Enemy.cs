using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Enemy : MonoBehaviour
{
    public event Action onTakeDamage;

    EnemyAI enemyAI;
    EnemyHpBar enemyHpBar;

    Rigidbody rigid;
    [SerializeField] SphereCollider sensor;
    BoxCollider box;
    Animator anim;

    public EEnemyAI aiState;

    public string enemyName;
    public float enemyHp;
    public float curhp;
    public float enemySpeed;
    public float rotationSpeed;
    public float attackRange;
    public int attackDmg;

    [SerializeField] protected Transform[] targetPoints;
    [SerializeField] protected Transform myTarget;
    protected Vector3 targetPointDir;
    internal bool isDie { get; private set; }
    internal bool isStay { get; private set; }
    [SerializeField] internal bool attackReady { get; private set; }
    [SerializeField] bool isAttack;

    Coroutine attackCoroutine;

    Vector3 tagetTrs;

    public void InitEnemyData(EnemyData _enemyData)
    {
        rigid = GetComponent<Rigidbody>();
        box = this.GetComponent<BoxCollider>();
        anim = this.GetComponent<Animator>();

        enemyName = _enemyData.enemyName;
        enemyHp = _enemyData.enemyHp;
        curhp = enemyHp;
        enemySpeed = _enemyData.enemySpeed;
        rotationSpeed = 5;
        attackRange = 1;
        attackDmg = 10;

        targetPoints = Shared.enemyMng.iEnemySpawner.GetTargetPoint();
        int rand = Random.Range(0, targetPoints.Length);
        myTarget = targetPoints[rand];

        enemyAI = new EnemyAI();
        enemyAI.Init(this);
    }

    private void OnEnable()
    {
        curhp = enemyHp;

        GameObject hpBar = Shared.objectPoolMng.iHpBarPool.FindHpBar(EHpBar.NORMAL);
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
            sensor.radius, LayerMask.GetMask("Player", "UnitField"));

        if(colls.Length > 0) 
        {
            myTarget = colls[0].transform;
        }
    }

    protected internal void ReadyAttack()
    {
        if (Vector3.Distance(transform.position,
            new Vector3(myTarget.position.x, myTarget.position.y, myTarget.position.z)) 
            < attackRange)
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

    IEnumerator StartAttack()
    {
        isAttack = true;

        if (myTarget.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Shared.playerMng.playerStat.TakeDmg(attackDmg);
        }

        yield return new WaitForSeconds(1f);

        isAttack = false;
        attackCoroutine = null;
    }


    public void TakeDamage(int _damage)
    {
        curhp -= _damage;

        onTakeDamage?.Invoke();

        if (curhp <= 0)
        {
            isDie = true;
        }
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
        Shared.gameMng.iRewarder.SetReward(EReward.GOLD, 50);
        Shared.gameMng.iRewarder.SetReward(EReward.GEM, 10);
        Shared.gameMng.iRewarder.SetReward(EReward.PAPER, 20);
        Shared.gameMng.iRewarder.SetReward(EReward.EXP, 1);

        yield return new WaitForSeconds(1f);

        Shared.objectPoolMng.ReturnObject(enemyHpBar.gameObject.name, enemyHpBar.gameObject);
        Shared.objectPoolMng.ReturnObject(this.gameObject.name, this.gameObject);
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

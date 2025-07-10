using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class EnemyBase : MonoBehaviour, ITakeDmg
{
    public event Action onTakeDamage;

    [Header("AI")]
    protected EnemyAI enemyAI;
    protected EEnemyAI aiState;
    protected EEnemy eEnemy;

    [Header("Internal Components")]
    [SerializeField] protected SphereCollider sensor;

    [Header("External Components")]
    protected Rigidbody rigid;
    protected Rewarder rewarder;
    protected EnemySpawner enemySpawner;
    protected GoldCoin goldCoin;
    protected Round round;
    protected Timer timer;
    GameState gameState;

    [Header("View")]
    [SerializeField] protected EnemyView enemyView;

    [Header("Pool")]
    protected IEnemyPool enemyPool;
    protected EffectPool effectPool;
    protected IBulletPool bulletPool;

    [Header("Runtime Value")]
    protected Transform myTarget;
    protected Vector3 targetPointDir;
    Transform centerPoint;
    protected bool isAttack;
    bool isOneShot = true;
    bool isReset;
    protected Coroutine attackCoroutine;

    RaiseEventOptions options = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
    SendOptions sendOptions = new SendOptions { Reliability = false };

    public int ID { get; private set; }
    public string enemyName { get; private set; }
    public float maxHp { get; private set; }
    public float curhp;
    public float enemySpeed { get; private set; }
    public float rotationSpeed { get; private set; }
    public float attackRange { get; private set; }
    public float attackDmg { get; private set; }

    public bool isStay { get; private set; }
    public bool attackReady { get; private set; }
    public bool isDie { get; protected set; }

    public virtual void Init(string _name, float _maxHp, float _spd, float _range, float _dmg, EEnemy _eEnemy, int _id)
    {
        rigid = GetComponent<Rigidbody>();

        enemyAI = new EnemyAI();
        enemyAI.Init(this);

        enemySpawner = EnemyManager.instance.EnemySpawner;
        goldCoin = GameManager.instance.GoldCoin;
        round = GameManager.instance.Round;
        timer = GameManager.instance.Timer;
        rewarder = GameManager.instance.Rewarder;
        bulletPool = Shared.Instance.poolManager.BulletPool;
        enemyPool = Shared.Instance.poolManager.EnemyPool;
        effectPool = ObjectPoolManager.instance.EffectPool;
        gameState = GameManager.instance.GameState;

        enemyView.CreateHpBar(this);

        ID = _id;
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
    private void OnEnable()
    {
        ResetState();

        enemyView.InitHpBar(this);

        StartCoroutine(StartSendSyncData());
    }

    private void OnDisable()
    {
        ResetState();
        enemyAI.aiState = EEnemyAI.CREATE;
        this.enabled = false;

        StopAllCoroutines();
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
        if (gameState.GetGameState() != EGameState.PLAYING)
        {
            enemyView.ChangeAnim(EEnemyAI.CREATE);
            return;
        }

        if (enemyAI == null && isReset) return;

        aiState = enemyAI.aiState;

        if (enemyAI != null)
        {
            enemyAI.State();
            enemyView.ChangeAnim(enemyAI.aiState);
        }
    }

    IEnumerator StartSendSyncData()
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f);

        while (true)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                EnemySyncData data = new EnemySyncData()
                {
                    id = ID,
                    pos = transform.position,
                    rot = transform.rotation,
                };

                PhotonNetwork.RaiseEvent(PhotonEventCode.ENEMY_SYNC_EVENT, data, options, sendOptions);
            }

            yield return wait;
        }
    }

    protected virtual void Move()
    {
        if (myTarget == null) return;

        targetPointDir = (myTarget.transform.position - transform.position).normalized;

        rigid.MovePosition(transform.position + targetPointDir * enemySpeed * Time.fixedDeltaTime);
    }

    protected virtual void Turn()
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

    protected virtual void ReadyAttack()
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

        enemyView.ChangeHitMat();
        Invoke(nameof(enemyView.ResetMat), 0.2f);

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
        int rand = Random.Range(0, 10);

        GameObject dieEffect = enemyView.PlayDieEffect();

        if (rand <= 3)
        {
            GameObject ammoPack =
                bulletPool.FindBullet(EBullet.AMMOPACK, transform.position, Quaternion.identity);
        }

        rewarder.SetReward(EReward.GOLD, 50);
        rewarder.SetReward(EReward.GEM, 10);
        rewarder.SetReward(EReward.PAPER, 20);
        rewarder.SetReward(EReward.EXP, 1);

        enemyView.ReturnHpBar();

        yield return new WaitForSeconds(1f);

        effectPool.ReturnEffect(EEffect.ENEMYEXPLOSION, dieEffect);
        enemyPool.ReturnEnemy(eEnemy, this.gameObject);
    }

    public void ChangeState()
    {
        object[] data = new object[] { (int)aiState, ID};

        PhotonNetwork.RaiseEvent(PhotonEventCode.ENEMY_STATE_EVENT, data, options, sendOptions);
    }

    public virtual void MoveAI()
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

    public virtual void AttackAI()
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

    public virtual void DieAI()
    {
        if (isDie && isOneShot)
        {
            Die();
            isOneShot = false;
        }
    }
}

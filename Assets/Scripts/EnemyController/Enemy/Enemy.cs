using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    public EnemyData enemyData;
    protected EnemyAI enemyAI;
    protected EnemyHpBar enemyHpBar;

    protected BoxCollider box;
    protected Animator anim;

    public string enemyName;
    public float enemyHp;
    public float curhp;
    public float enemySpeed;
    public float rotationSpeed;

    protected Transform targetPoint;
    protected Vector3 targetPointDir;
    protected internal bool isDie;
    protected internal bool isStay;

    public void InitEnemyData(EnemyData _enemyData)
    {
        box = this.GetComponent<BoxCollider>();
        anim = this.GetComponent<Animator>();

        enemyName = _enemyData.enemyName;
        enemyHp = _enemyData.enemyHp;
        curhp = enemyHp;
        enemySpeed = _enemyData.enemySpeed;
        rotationSpeed = 5;

        targetPoint = Shared.enemyMng.iEnemySpawner.GetTargetPoint();

        GameObject hpBar = Shared.objectPoolMng.iHpBarPool.FindHpBar(EHpBar.NORMAL);
        enemyHpBar = hpBar.GetComponent<EnemyHpBar>();
        enemyHpBar.Init(this);
 
        enemyAI = new EnemyAI();
        enemyAI.Init(this);

        isDie = false;
        isStay = false;
    }

    private void Update()
    {
        if (enemyAI != null)
        {
            enemyAI.State();
            ChangeAnim(enemyAI.aiState);
        }
    }

    protected internal void Move()
    {
        targetPointDir = (targetPoint.transform.position - transform.position).normalized;

        transform.Translate(targetPointDir * enemySpeed * Time.deltaTime, Space.World);
    }

    protected internal void Turn()
    {
        Quaternion rotation = Quaternion.LookRotation(new Vector3(targetPointDir.x, 0, targetPointDir.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation,
            rotationSpeed * Time.deltaTime);
    }

    public void TakeDamage(int _damage)
    {
        curhp -= _damage;

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

        yield return new WaitForSeconds(0.75f);

        Shared.objectPoolMng.ReturnObject(enemyHpBar.gameObject.name, enemyHpBar.gameObject);
        Shared.objectPoolMng.ReturnObject(this.gameObject.name, this.gameObject);
    }

    protected void ChangeAnim(EEnemyAI _curState)
    {
        switch (_curState)
        {
            case EEnemyAI.CREATE:
                anim.SetInteger("enemyAnim", (int)EEnemyAnim.IDLE);
                break;
            case EEnemyAI.MOVE:
                anim.SetInteger("enemyAnim", (int)EEnemyAnim.WALK);
                break;
            case EEnemyAI.ATTACK:
                anim.SetInteger("enemyAnim", (int)EEnemyAnim.ATTACK);
                break;
            case EEnemyAI.STAY:
                anim.SetInteger("enemyAnim", (int)EEnemyAnim.IDLE);
                break;
            case EEnemyAI.DIE:
                anim.SetInteger("enemyAnim", (int)EEnemyAnim.DIE);
                break;

        }
    }
}

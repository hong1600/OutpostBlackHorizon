using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    public EnemyAI enemyAI;
    public EnemyHpBar enemyHpBar;

    public BoxCollider box;
    public Animator anim;

    public string enemyName;
    public float enemyHp;
    public float curhp;
    public float enemySpeed;
    public Transform[] wayPoints;
    public Vector3 wayPointDir;
    public int wayPointIndex;
    public Transform target;
    public float rotationSpeed;
    public bool isDie;
    public bool isStay;

    public void InitEnemyData(EnemyData _enemyData)
    {
        box = this.GetComponent<BoxCollider>();
        anim = this.GetComponent<Animator>();

        enemyName = _enemyData.enemyName;
        enemyHp = _enemyData.enemyHp;
        curhp = enemyHp;
        enemySpeed = _enemyData.enemySpeed;
        rotationSpeed = 5;

        wayPoints = Shared.enemyMng.iEnemySpawner.GetWayPoint();
        wayPointIndex = 1;
        target = wayPoints[wayPointIndex];

        GameObject hpBar = Shared.objectPoolMng.iHpBarPool.FindHpBar
            (Shared.objectPoolMng.iHpBarPool.GetHpBar().name);
        enemyHpBar = hpBar.GetComponent<EnemyHpBar>();
        enemyHpBar.Init(this);

        enemyAI = new EnemyAI();
        enemyAI.Init(this);
    }

    private void Update()
    {
        if (enemyAI != null)
        {
            enemyAI.State();
        }
    }

    public void Move()
    {
        wayPointDir = (target.transform.position - transform.position).normalized;

        transform.Translate(wayPointDir * enemySpeed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.1f)
        {
            NextMove();
        }
    }

    public void NextMove()
    {
        if (wayPointIndex >= wayPoints.Length -1)
        {
            wayPointIndex = 0;
        }
        else
        {
            wayPointIndex++;
        }

        target = wayPoints[wayPointIndex];
    }

    public void Turn()
    {
        Quaternion rotation = Quaternion.LookRotation(new Vector3(wayPointDir.x, 0, wayPointDir.z));
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

    public virtual void Die()
    {

        StartCoroutine(StartDie());
    }

    IEnumerator StartDie()
    {
        ChangeAnim(EEnemyAI.DIE);

        Shared.gameMng.iRewarder.AddRewardGold(50);
        Shared.gameMng.iRewarder.AddRewardGem(10);
        Shared.gameMng.iRewarder.AddRewardPaper(20);
        Shared.gameMng.iRewarder.AddRewardExp(1);

        yield return new WaitForSeconds(0.75f);

        Shared.objectPoolMng.ReturnObject(this.gameObject.name, this.gameObject);
        Shared.objectPoolMng.ReturnObject(enemyHpBar.gameObject.name, enemyHpBar.gameObject);
    }

    public void ChangeAnim(EEnemyAI _curState)
    {
        switch (_curState)
        {
            case EEnemyAI.CREATE:
                anim.SetInteger("enemyAnim", (int)EEnemyAnim.RUN);
                break;
            case EEnemyAI.MOVE:
                anim.SetInteger("enemyAnim", (int)EEnemyAnim.RUN);
                break;
            case EEnemyAI.DIE:
                anim.SetInteger("enemyAnim", (int)EEnemyAnim.DIE);
                break;

        }
    }
}

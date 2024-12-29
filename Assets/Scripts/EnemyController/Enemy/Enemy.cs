using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    public EnemyAI enemyAI;

    public IEnemySpawner iEnemySpawner;
    public IRewarder iRewarder;
    public IGoldCoin iGoldCoin;
    public ITimer iTimer;
    public IRound iRound;
    public IWaveBossSpawner iWaveBossSpawner;
    public IBossSpawner iBossSpawner;

    public BoxCollider box;
    public Animator anim;

    public string enemyName;
    public float enemyHp;
    public float curhp;
    public float enemySpeed;
    public Transform[] wayPoint;
    public Vector3 wayPointDir;
    public int wayPointIndex;
    public Transform target;
    public float rotationSpeed;
    public bool isDie;
    public bool isStay;

    public void initEnemyData(EnemyData enemyData)
    {
        if(iBossSpawner != null) 
        {
            box = this.GetComponent<BoxCollider>();
            anim = this.GetComponent<Animator>();

            enemyName = enemyData.enemyName;
            enemyHp = enemyData.enemyHp;
            curhp = enemyHp;
            enemySpeed = enemyData.enemySpeed;
            rotationSpeed = 5;

            wayPoint = iEnemySpawner.getWayPoint();
            wayPointIndex = 1;
            target = wayPoint[wayPointIndex];

            //if (enemyData.hpBar != null)
            //{
            //    Instantiate(enemyData.hpBar, new Vector3(transform.position.x, transform.position.y, transform.position.z),
            //        Quaternion.identity, transform);
            //}

            enemyAI = new EnemyAI();
            enemyAI.init(this);
        }
    }

    private void Update()
    {
        if (enemyAI != null)
        {
            enemyAI.State();
        }
    }

    public void move()
    {
        wayPointDir = (target.transform.position - transform.position).normalized;

        transform.Translate(wayPointDir * enemySpeed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.1f)
        {
            nextMove();
        }
    }

    public void nextMove()
    {
        if (wayPointIndex >= wayPoint.Length -1)
        {
            wayPointIndex = 0;
        }
        else
        {
            wayPointIndex++;
        }

        target = wayPoint[wayPointIndex];
    }

    public void turn()
    {
        Quaternion rotation = Quaternion.LookRotation(new Vector3(wayPointDir.x, 0, wayPointDir.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation,
            rotationSpeed * Time.deltaTime);
    }

    public void takeDamage(int damage)
    {
        curhp -= damage;

        if (curhp <= 0)
        {
            isDie = true;
        }
    }

    public virtual void die()
    {
        changeAnim(eEnemyAI.DIE);

        iRewarder.addRewardGold(50);
        iRewarder.addRewardGem(10);
        iRewarder.addRewardPaper(20);
        iRewarder.addRewardExp(1);

        Destroy(this.gameObject, 0.75f);
    }

    public void changeAnim(eEnemyAI curState)
    {
        switch (curState)
        {
            case eEnemyAI.CREATE:
                anim.SetInteger("enemyAnim", (int)EEnemyAnim.RUN);
                break;
            case eEnemyAI.MOVE:
                anim.SetInteger("enemyAnim", (int)EEnemyAnim.RUN);
                break;
            case eEnemyAI.DIE:
                anim.SetInteger("enemyAnim", (int)EEnemyAnim.DIE);
                break;

        }
    }
}

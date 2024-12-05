using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyType { Nomal, WaveBoss, boss };

public class Enemy : MonoBehaviour
{
    public EnemyType enemyType;

    public EnemyAI enemyAI;
    public EnemyHpBar enemyHpBar;

    public EnemySpawner enemySpawner;
    public IEnemySpawner iEnemySpawner;
    public WaveBossSpawner waveBossSpawner;
    public IWaveBossSpawner iWaveBossSpawner;
    public Rewarder rewarder;
    public IRewarder iRewarder;
    public GoldCoin goldCoin;
    public IGoldCoin iGoldCoin;
    public Timer timer;
    public ITimer iTimer;
    public Round round;
    public IRound iRound;

    [Header("Enemy")]
    public BoxCollider box;
    public Animator anim;
    public float enemyHp;
    public float curhp;
    public string enemyName;
    public float enemySpeed;
    public Transform[] wayPoint;
    public Transform target;
    public int wayPointIndex;
    public bool isDie;
    public Vector3 wayPointdir;
    public float rotationSpeed;
    public bool isStay;

    [Header("Boss")]
    public TextMeshProUGUI bossTimeText;
    public float bosstime;

    private void Awake()
    {
        bosstime = 60f;
    }

    public void init(EnemyData enemyData)
    {
        enemyAI = new EnemyAI();
        enemyAI.init(this);

        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        iEnemySpawner = enemySpawner;

        waveBossSpawner = GameObject.Find("WaveBossSpawner").GetComponent<WaveBossSpawner>();
        iWaveBossSpawner = waveBossSpawner;

        rewarder = GameObject.Find("Rewarder").GetComponent<Rewarder>();
        iRewarder = rewarder;

        goldCoin = GameObject.Find("GoldCoin").GetComponent<GoldCoin>();
        iGoldCoin = goldCoin;

        timer = GameObject.Find("Timer").GetComponent<Timer>();
        iTimer = timer;

        round = GameObject.Find("Round").GetComponent<Round>();
        iRound = round;

        box = this.GetComponent<BoxCollider>();
        anim = this.GetComponent<Animator>();

        enemyName = enemyData.enemyName;
        enemyHp = enemyData.enemyHp;
        enemySpeed = enemyData.enemySpeed;
        curhp = enemyHp;

        wayPoint = iEnemySpawner.getWayPoint();
        wayPointIndex = 1;
        target = wayPoint[wayPointIndex];

        rotationSpeed = 5;
    }

    private void Update()
    {
        enemyAI.State();
    }

    public void move()
    {
        wayPointdir = (target.transform.position - transform.position).normalized;

        transform.Translate(wayPointdir * enemySpeed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.1f)
        {
            nextMove();
        }
    }

    public void turn()
    {
        Quaternion rotation = Quaternion.LookRotation(new Vector3(wayPointdir.x, 0, wayPointdir.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation,
            rotationSpeed * Time.deltaTime);
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

    public void takeDamage(int damage)
    {
        curhp -= damage;

        if (curhp <= 0)
        {
            isDie = true;
            die();
        }
    }

    public void die()
    {
        switch (enemyType) 
        {
            case EnemyType.Nomal:
                iGoldCoin.setGold(1);
                break;
            case EnemyType.WaveBoss:
                iGoldCoin.setCoin(2);
                iWaveBossSpawner.setWaveBossDelay(25f);
                break;
            case EnemyType.boss:
                iGoldCoin.setGold(300);
                iGoldCoin.setCoin(4);
                iRound.setBossRound(false);
                iTimer.setSec(15f);
                iEnemySpawner.setEnemySpawnDelay(0.85f);
                break;
        }

        Destroy(gameObject, 0.5f);
        rewarder.rewardGold += 50;
        rewarder.rewardGem += 10;
        rewarder.rewardPaper += 20;
        rewarder.rewardExp += 1;
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
        }
    }


    //private void waveBossTimer()
    //{
    //    bosstime -= Time.deltaTime;
    //    bossTimeText.text = bosstime.ToString("F1")+"s";

    //    if (bosstime <= 0)
    //    {
    //        Destroy(this.gameObject);
    //        waveBossSpawner.wavebossDelay = 25;
    //    }
    //}

    //private void bossTimer()
    //{
    //    bosstime -= Time.deltaTime;
    //    bossTimeText.text = bosstime.ToString("F1") + "s";

    //    if (bosstime <= 0)
    //    {
    //        gameStateCheck.gameOver = true;
    //    }
    //}
}

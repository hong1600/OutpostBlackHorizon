using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyType { Nomal, WaveBoss, boss };

public class Enemy : MonoBehaviour
{
    public EnemyType enemyType;

    [Header("Enemy")]
    [SerializeField] EnemyData enemyData;
    BoxCollider2D box;
    Animator anim;
    [SerializeField] float enemyHp;
    [SerializeField] float curhp;
    string enemyName;
    float enemySpeed;
    [SerializeField] Transform[] wayPoint;
    GameObject wayPointTrs;
    Transform target;
    int wayPointIndex = 0;
    [SerializeField] GameObject healthBarBack;
    [SerializeField] Image healthBarFill;
    public bool isDie;

    [Header("Boss")]
    [SerializeField] TextMeshProUGUI bossTimeText;
    [SerializeField] float bosstime;

    public float EnemyHp 
    { get { return enemyHp; } set { value = enemyHp; } }

    private void Awake()
    {
        box = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        wayPointTrs = GameObject.Find("WayPoints");
    }

    private void Start()
    {
        enemyName = enemyData.enemyName;
        enemyHp = enemyData.enemyHp;
        enemySpeed = enemyData.enemySpeed;
        curhp = enemyHp;
        bosstime = 60f;

        healthBarFill.fillAmount = 1;

        wayPoint = new Transform[wayPointTrs.transform.childCount];
        for (int i = 0; i < wayPoint.Length; i++)
        {
            wayPoint[i] = wayPointTrs.transform.GetChild(i);
        }

        target = wayPoint[wayPointIndex];
    }

    private void Update()
    {
        if (isDie) return;
        move();
        hpBar();
        if (enemyType == EnemyType.WaveBoss)
        {
            waveBossTimer();
        }
        if (enemyType == EnemyType.boss)
        {
            bossTimer();
        }
    }

    private void move()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemySpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) <= 0.05f)
        {
            nextMove();
        }
    }

    private void hpBar()
    {
        healthBarFill.fillAmount = curhp / enemyHp;
    }

    private void nextMove()
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
            die();
        }
    }

    private void die()
    {
        switch (enemyType) 
        {
            case EnemyType.Nomal:
                GameManager.Instance.Gold += 1;
                break;
            case EnemyType.WaveBoss:
                GameManager.Instance.Coin += 2;
                GameManager.Instance.wavebossDelay = 25f;
                break;
            case EnemyType.boss:
                GameManager.Instance.Gold += 300f;
                GameManager.Instance.Coin += 4;
                GameManager.Instance.BossRound = false;
                GameManager.Instance.Sec = 15f;
                GameManager.Instance.EnemySpawnDelay = 0.85f;
                break;
        }

        anim.SetBool("isDie", true);
        isDie = true;
        Destroy(gameObject, 0.5f);
        GameManager.Instance.RewardGold += 50;
        GameManager.Instance.RewardGem += 10;
        GameManager.Instance.RewardPaper += 20;
        GameManager.Instance.RewardExp += 1;
    }

    private void waveBossTimer()
    {
        bosstime -= Time.deltaTime;
        bossTimeText.text = bosstime.ToString("F1")+"s";

        if (bosstime <= 0)
        {
            Destroy(this.gameObject);
            GameManager.Instance.waveBossTime = 25f;
        }
    }

    private void bossTimer()
    {
        bosstime -= Time.deltaTime;
        bossTimeText.text = bosstime.ToString("F1") + "s";

        if (bosstime <= 0)
        {
            GameManager.Instance.bGameOver = true;
        }
    }
}

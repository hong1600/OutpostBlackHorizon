using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] EnemyData enemyData;

    BoxCollider2D box;
    Animator anim;

    GameObject wayPointTrs;
    [SerializeField] Transform[] wayPoint;
    Transform target;
    int wayPointIndex = 0;

    [SerializeField] GameObject healthBarBack;
    [SerializeField] Image healthBarFill;

    public bool isDie;

    string enemyName;
    [SerializeField] float enemyHp;
    [SerializeField] float curhp;
    float enemySpeed;

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
        GameManager.Instance.Gold += 1;
        anim.SetBool("isDie", true);
        isDie = true;
        Destroy(gameObject, 0.5f);
        GameManager.Instance.RewardGold += 50;
        GameManager.Instance.RewardGem += 10;
        GameManager.Instance.RewardPaper += 20;
        GameManager.Instance.RewardExp += 1;
    }
}

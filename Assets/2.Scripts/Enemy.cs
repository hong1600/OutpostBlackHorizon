using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] EnemyData enemyData;

    BoxCollider2D box;

    GameObject wayPointTrs;
    [SerializeField] Transform[] wayPoint;
    Transform target;
    int wayPointIndex = 0;

    string enemyName;
    int enemyHp;
    float enemySpeed;

    private void Awake()
    {
        box = GetComponent<BoxCollider2D>();
        wayPointTrs = GameObject.Find("WayPoints");
    }

    private void Start()
    {
        enemyName = enemyData.enemyName;
        enemyHp = enemyData.enemyHp;
        enemySpeed = enemyData.enemySpeed;

        wayPoint = new Transform[wayPointTrs.transform.childCount];
        for (int i = 0; i < wayPoint.Length; i++)
        {
            wayPoint[i] = wayPointTrs.transform.GetChild(i);
        }

        target = wayPoint[wayPointIndex];
    }

    private void Update()
    {
        move();
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
}

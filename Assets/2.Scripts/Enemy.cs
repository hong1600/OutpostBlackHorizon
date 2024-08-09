using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] EnemyData EnemyData;

    BoxCollider2D box;

    [SerializeField] Transform[] wayPoint;
    [SerializeField] Transform target;
    [SerializeField] int wayPointIndex = 0;

    int id;
    int hp;
    float speed;

    private void Awake()
    {
        box = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        id = EnemyData.enemyId;
        hp = EnemyData.enemyHp;
        speed = EnemyData.enemySpeed;

        target = wayPoint[wayPointIndex];
    }

    private void Update()
    {
        move();
    }

    private void move()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime);

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

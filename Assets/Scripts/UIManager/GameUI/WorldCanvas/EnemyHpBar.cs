using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpBar : MonoBehaviour
{
    public Enemy enemy;

    public GameObject hpBarBack;
    public Image hpBarFill;
    public Vector3 offset;

    private void Awake()
    {
        hpBarFill.fillAmount = 1;
    }

    private void Update()
    {
        hpBar();
    }

    public void Init(Enemy _enemy)
    {
        enemy = _enemy;

        offset = new Vector3(0f, 3f, 0f);
    }

    public void hpBar()
    {
        hpBarFill.fillAmount = enemy.curhp / enemy.enemyHp;

        this.gameObject.transform.position = enemy.transform.position + offset;

        Quaternion rotation = Quaternion.Euler(60f, 0f, 0f);
        this.gameObject.transform.rotation = rotation;
    }
}

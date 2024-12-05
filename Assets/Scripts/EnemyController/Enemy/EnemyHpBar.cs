using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpBar : MonoBehaviour
{
    public Enemy enemy;

    public GameObject hpBarBack;
    public Image hpBarFill;

    private void Awake()
    {
        hpBarFill.fillAmount = 1;
    }

    public void init(Enemy _enemy)
    {
        enemy = _enemy;
    }

    public void hpBar()
    {
        hpBarFill.fillAmount = enemy.curhp / enemy.enemyHp;

        hpBarBack.transform.LookAt(Camera.main.transform);
        hpBarBack.transform.Rotate(0, 180, 0);
    }
}

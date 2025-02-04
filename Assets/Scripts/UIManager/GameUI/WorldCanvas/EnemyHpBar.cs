using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpBar : MonoBehaviour
{
    [SerializeField] Enemy enemy;

    [SerializeField] GameObject hpBarBack;
    [SerializeField] Image hpBarFill;
    [SerializeField] Vector3 offset;

    private void Update()
    {
        hpBar();
    }

    public void Init(Enemy _enemy)
    {
        enemy = _enemy;

        offset = new Vector3(0f, 3f, 0f);

        hpBarFill.fillAmount = 1;
    }

    private void hpBar()
    {
        hpBarFill.fillAmount = enemy.curhp / enemy.enemyHp;

        this.gameObject.transform.position = enemy.transform.position + offset;

        this.gameObject.transform.LookAt(Camera.main.transform);
<<<<<<< HEAD
=======

        //Quaternion rotation = Quaternion.Euler(60f, 0f, 0f);
        //this.gameObject.transform.rotation = rotation;
>>>>>>> origin/main
    }
}

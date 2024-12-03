using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpBar
{
    public GameObject hpBarBack;
    public Image hpBarFill;

    private void Awake()
    {
        hpBarFill.fillAmount = 1;
    }

    public void hpBar(float curHp, float maxHp)
    {
        hpBarFill.fillAmount = curHp / maxHp;

        hpBarBack.transform.LookAt(Camera.main.transform);
        hpBarBack.transform.Rotate(0, 180, 0);
    }
}

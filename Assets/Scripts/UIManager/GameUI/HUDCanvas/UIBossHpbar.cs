using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBossHpbar : MonoBehaviour
{
    Boss boss;

    [SerializeField] GameObject bossHpBar;

    [SerializeField] Image hpValue;

    private void Start()
    {
        //boss.onTakeDamage += UpdateHpBar;
    }

    public void Init(Boss _boss)
    {
        boss = _boss;
    }

    public void ShowHpBar()
    {
        bossHpBar.SetActive(true);
    }

    private void UpdateHpBar()
    {
        hpValue.fillAmount = boss.curhp / boss.maxHp;
    }
}

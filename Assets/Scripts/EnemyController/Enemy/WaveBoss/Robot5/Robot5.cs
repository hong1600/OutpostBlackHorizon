using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot5 : WaveBoss
{
    TableEnemy.Info info;

    private void Awake()
    {
        info = DataManager.instance.TableEnemy.Get(205);

        base.InitEnemyData(info.Name, info.MaxHp, info.Speed, info.AttackRange, info.AttackDmg, EEnemy.ROBOT5);
    }

    protected override IEnumerator StartAttack()
    {
        isAttack = true;

        ITakeDmg iTakeDmg = myTarget.gameObject.GetComponent<ITakeDmg>();

        iTakeDmg.TakeDmg(attackDmg, false);

        yield return new WaitForSeconds(1f);

        isAttack = false;
        attackCoroutine = null;
    }
}

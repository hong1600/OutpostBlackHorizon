using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot1 : NormalEnemy
{
    protected override void Awake()
    {
        base.Awake();
        base.InitEnemyData(DataManager.instance.TableEnemy.Get(201), EEnemy.ROBOT1);
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

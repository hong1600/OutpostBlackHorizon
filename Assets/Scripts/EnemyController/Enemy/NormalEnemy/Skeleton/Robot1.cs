using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot1 : NormalEnemy
{
    private void Start()
    {
        base.InitEnemyData(DataManager.instance.TableEnemy.Get(201));
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

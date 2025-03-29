using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot6 : Boss
{
    TableEnemy.Info info;

    private void Awake()
    {
        info = DataManager.instance.TableEnemy.Get(206);

        base.InitEnemyData(info.Name, info.MaxHp, info.Speed, info.AttackRange, info.AttackDmg, EEnemy.ROBOT6);
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

    protected override void ChangeAnim(EEnemyAI _curState)
    {
        //_curState = aiState;

        //switch (_curState)
        //{
        //    case EEnemyAI.CREATE:
        //        anim.SetInteger("EnemyAnim", (int)EEnemyAnim.IDLE);
        //        break;
        //    case EEnemyAI.ATTACK:
        //        anim.SetInteger("EnemyAnim", (int)EEnemyAnim.ATTACK);
        //        break;
        //    case EEnemyAI.STAY:
        //        anim.SetInteger("EnemyAnim", (int)EEnemyAnim.IDLE);
        //        break;
        //    case EEnemyAI.DIE:
        //        anim.SetInteger("EnemyAnim", (int)EEnemyAnim.DIE);
        //        break;
        //}
    }
}

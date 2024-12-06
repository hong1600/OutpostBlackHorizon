using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eEnemyAI
{
    CREATE,
    MOVE,
    STAY,
    DIE,
}

public class EnemyAI
{
    public Enemy enemy;

    public eEnemyAI AIState = eEnemyAI.CREATE;

    public void init(Enemy _enemy)
    {
        enemy = _enemy;
    }

    public void State()
    {
        switch (AIState)
        {
            case eEnemyAI.CREATE:
                Create();
                break;
            case eEnemyAI.MOVE:
                Move();
                break;
            case eEnemyAI.STAY:
                Stay();
                break;
            case eEnemyAI.DIE:
                Die();
                break;
        }
    }

    public virtual void Create()
    {
        AIState = eEnemyAI.MOVE;
    }

    public virtual void Move()
    {
        if(enemy.isDie == false) 
        {
            enemy.move();
            enemy.turn();
        }
        else if (enemy.isStay == true)
        {
            AIState = eEnemyAI.STAY;
        }
        else if (enemy.isDie == true) 
        {
            AIState = eEnemyAI.DIE;
        }
    }

    public virtual void Stay()
    {
        if (enemy.isStay == false && enemy.isDie == false)
        {
            AIState = eEnemyAI.MOVE;
        }
        else if (enemy.isStay == false && enemy.isDie == true) 
        {
            AIState = eEnemyAI.DIE;
        }
    }

    public virtual void Die()
    {
    }

}

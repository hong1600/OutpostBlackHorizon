using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eEnemyAI
{
    eAI_CREATE,
    eAI_MOVE,
    eAI_RESET,
}

public class EnemyAI
{
    public Enemy enemy;

    public eEnemyAI AIState = eEnemyAI.eAI_CREATE;

    public void init(Enemy _enemy)
    {
        enemy = _enemy;
    }

    public void State()
    {
        switch (AIState)
        {
            case eEnemyAI.eAI_CREATE:
                Create();
                break;
            case eEnemyAI.eAI_MOVE:
                Move();
                break;
            case eEnemyAI.eAI_RESET:
                Reset();
                break;
        }
    }

    public virtual void Create()
    {
        AIState = eEnemyAI.eAI_MOVE;
    }

    public virtual void Move()
    {
        if(enemy.isDie == false) 
        {
            enemy.move();
            enemy.turn();
            enemy.enemyHpBar.hpBar(enemy.curhp, enemy.curhp);
        }
        else
        {
            AIState = eEnemyAI.eAI_RESET;
        }
    }

    public virtual void Reset()
    {
        enemy.changeAnim(eEnemyAI.eAI_RESET);
    }

}

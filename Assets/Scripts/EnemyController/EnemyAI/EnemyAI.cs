using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI
{
    public Enemy enemy;

    public EEnemyAI aiState = EEnemyAI.CREATE;

    public void Init(Enemy _enemy)
    {
        enemy = _enemy;
    }

    public void State()
    {
        switch (aiState)
        {
            case EEnemyAI.CREATE:
                Create();
                break;
            case EEnemyAI.MOVE:
                Move();
                break;
            case EEnemyAI.STAY:
                Stay();
                break;
            case EEnemyAI.DIE:
                Die();
                break;
        }
    }

    public virtual void Create()
    {
        aiState = EEnemyAI.MOVE;
    }

    public virtual void Move()
    {
        if(enemy.isDie == false) 
        {
            enemy.Move();
            enemy.Turn();
            enemy.enemyHpBar.hpBar();
        }
        else if (enemy.isStay == true)
        {
            aiState = EEnemyAI.STAY;
        }
        else if (enemy.isDie == true) 
        {
            aiState = EEnemyAI.DIE;
        }
    }

    public virtual void Stay()
    {
        if (enemy.isStay == false && enemy.isDie == false)
        {
            aiState = EEnemyAI.MOVE;
            enemy.enemyHpBar.hpBar();
        }
        else if (enemy.isStay == false && enemy.isDie == true) 
        {
            aiState = EEnemyAI.DIE;
        }
    }

    public virtual void Die()
    {
        enemy.Die();
    }
}

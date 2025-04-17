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
            case EEnemyAI.ATTACK:
                Attack();
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
        if (enemy.isDie)
        {
            aiState = EEnemyAI.DIE;
            enemy.MoveAI();
        }

        enemy.MoveAI();

        if (enemy.isStay)
        {
            aiState = EEnemyAI.STAY;
        }
        else if (enemy.attackReady)
        {
            aiState = EEnemyAI.ATTACK;
        }
    }

    public virtual void Stay()
    {
        if (!enemy.isStay && !enemy.isDie)
        {
            aiState = EEnemyAI.MOVE;
        }
        else if (enemy.isStay && enemy.isDie)
        {
            aiState = EEnemyAI.DIE;
        }
    }

    public virtual void Attack()
    {
        enemy.AttackAI();

        if (enemy.isStay)
        {
            aiState = EEnemyAI.STAY;
        }
        if (enemy.isDie)
        {
            aiState = EEnemyAI.DIE;
        }
    }

    public virtual void Die()
    {
        if(enemy.isDie) 
        {
            enemy.DieAI();
        }
        else
        {
            aiState = EEnemyAI.CREATE;
        }
    }
}

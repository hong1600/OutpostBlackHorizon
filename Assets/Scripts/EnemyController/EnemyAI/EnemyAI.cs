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
        if (enemy.isDie == false)
        {
            enemy.Move();
            enemy.Turn();
            enemy.CheckTarget();
            enemy.ReadyAttack();
        }
        else
        {
            aiState = EEnemyAI.DIE;
        }

        if (enemy.isStay == true)
        {
            aiState = EEnemyAI.STAY;
        }

        if (enemy.attackReady == true)
        {
            aiState = EEnemyAI.ATTACK;
        }
    }

    public virtual void Stay()
    {
        if (enemy.isStay == false && enemy.isDie == false)
        {
            aiState = EEnemyAI.MOVE;
        }
        if (enemy.isStay == true && enemy.isDie == true)
        {
            aiState = EEnemyAI.DIE;
        }
    }

    public virtual void Attack()
    {
        if (enemy.attackReady)
        {
            enemy.ReadyAttack();
            enemy.Attack();
        }
        else if(enemy.attackReady == false && enemy.isDie == false) 
        {
            aiState = EEnemyAI.MOVE;
        }
        if (enemy.isStay == true)
        {
            aiState = EEnemyAI.STAY;
        }
        if (enemy.isDie == true)
        {
            aiState = EEnemyAI.DIE;
        }
    }

    public virtual void Die()
    {
        if(enemy.isDie) 
        {
            enemy.Die();
        }
        else
        {
            aiState = EEnemyAI.CREATE;
        }
    }
}

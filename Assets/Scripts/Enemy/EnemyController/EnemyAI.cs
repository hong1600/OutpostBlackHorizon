using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI
{
    public EnemyBase enemy;

    public EEnemyAI aiState = EEnemyAI.CREATE;

    public EEnemyAI lastState;

    public void Init(EnemyBase _enemy)
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
        if(lastState != EEnemyAI.CREATE) 
        {
            lastState = EEnemyAI.CREATE;

            enemy.ChangeState();
        }

        aiState = EEnemyAI.MOVE;
    }

    public virtual void Move()
    {
        if (lastState != EEnemyAI.MOVE)
        {
            lastState = EEnemyAI.MOVE;

            enemy.ChangeState();
        }

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
        if (lastState != EEnemyAI.STAY)
        {
            lastState = EEnemyAI.STAY;

            enemy.ChangeState();
        }

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
        if (lastState != EEnemyAI.ATTACK)
        {
            lastState = EEnemyAI.ATTACK;

            enemy.ChangeState();
        }

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
        if (lastState != EEnemyAI.DIE)
        {
            lastState = EEnemyAI.DIE;

            enemy.ChangeState();
        }

        if (enemy.isDie)
        {
            enemy.DieAI();
        }
    }
}

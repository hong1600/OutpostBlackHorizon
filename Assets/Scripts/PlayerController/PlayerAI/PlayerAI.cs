using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI
{
    public Player player;

    public EPlayer aiState = EPlayer.CREATE;

    public void Init(Player _player)
    {
        player = _player;
    }

    public void State()
    {
        switch (aiState)
        {
            case EPlayer.CREATE:
                Create();
                break;
            case EPlayer.WALK:
                Walk();
                break;
            case EPlayer.RUN:
                Run();
                break;
            case EPlayer.JUMP:
                Jump();
                break;
            case EPlayer.DIE:
                Die();
                break;
        }

        player.ChangeAnim(aiState);
    }

    public virtual void Create()
    {
        aiState = EPlayer.WALK;
    }

    public virtual void Walk()
    {
        if (player.isRun) aiState = EPlayer.RUN;
        if (player.isAttack) aiState = EPlayer.ATTACK;
        if (player.isDie) aiState = EPlayer.DIE;
    }

    public virtual void Run()
    {
        if (!player.isGround) aiState = EPlayer.JUMP;
        if (player.isDie) aiState = EPlayer.DIE;
    }

    public virtual void Jump()
    {
        if (player.isGround) aiState = EPlayer.WALK;
        if (player.isDie) aiState = EPlayer.DIE;
    }

    public virtual void Die()
    {
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI
{
    public PlayerMng playerMng;

    public EPlayer aiState = EPlayer.CREATE;

    public void Init(PlayerMng _playerMng)
    {
        playerMng = _playerMng;
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

        playerMng.ChangeAnim(aiState);
    }

    public virtual void Create()
    {
        aiState = EPlayer.WALK;
    }

    public virtual void Walk()
    {
        if (playerMng.playerMovement.isRun) aiState = EPlayer.RUN;
        if (playerMng.playerCombat.isAttack) aiState = EPlayer.ATTACK;
        if (playerMng.playerStat.isDie) aiState = EPlayer.DIE;
    }

    public virtual void Run()
    {
        if (!playerMng.playerMovement.isGround) aiState = EPlayer.JUMP;
        if (playerMng.playerStat.isDie) aiState = EPlayer.DIE;
    }

    public virtual void Jump()
    {
        if (playerMng.playerMovement.isGround) aiState = EPlayer.WALK;
        if (playerMng.playerStat.isDie) aiState = EPlayer.DIE;
    }

    public virtual void Die()
    {
    }
}

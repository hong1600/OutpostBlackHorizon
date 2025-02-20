using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI
{
    public PlayerMovement playerMovement;
    public PlayerCombat playerCombat;

    public EPlayer aiState = EPlayer.CREATE;

    public void Init(PlayerMovement _playerMovement, PlayerCombat _playerCombat)
    {
        playerMovement = _playerMovement;
        playerCombat = _playerCombat;
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

        playerMovement.ChangeAnim(aiState);
    }

    public virtual void Create()
    {
        aiState = EPlayer.WALK;
    }

    public virtual void Walk()
    {
        if (playerMovement.isRun) aiState = EPlayer.RUN;
        if (playerCombat.isAttack) aiState = EPlayer.ATTACK;
        if (playerMovement.isDie) aiState = EPlayer.DIE;
    }

    public virtual void Run()
    {
        if (!playerMovement.isGround) aiState = EPlayer.JUMP;
        if (playerMovement.isDie) aiState = EPlayer.DIE;
    }

    public virtual void Jump()
    {
        if (playerMovement.isGround) aiState = EPlayer.WALK;
        if (playerMovement.isDie) aiState = EPlayer.DIE;
    }

    public virtual void Die()
    {
    }
}

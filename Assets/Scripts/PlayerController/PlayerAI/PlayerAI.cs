using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI
{
    public PlayerManager playerManager;

    public EPlayer aiState = EPlayer.MOVE;

    public void Init(PlayerManager _playerManager)
    {
        playerManager = _playerManager;
    }

    public void State()
    {
        switch (aiState)
        {
            case EPlayer.MOVE:
                Move();
                break;
            case EPlayer.DIE:
                break;
        }
    }

    public virtual void Move()
    {
        if (playerManager.playerStatus.isDie) aiState = EPlayer.DIE;
    }
}

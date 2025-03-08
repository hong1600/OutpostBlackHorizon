using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerManager : MonoBehaviour
{
    public PlayerAI playerAI { get; private set; }
    public CapsuleCollider cap { get; private set; }
    public Animator anim { get; private set; }
    public PlayerMovement playerMovement { get; private set; }
    public PlayerCombat playerCombat { get; private set; }
    public PlayerStat playerStat { get; private set; }

    private void Awake()
    {
        Shared.playerManager = this;
        cap = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();

        playerMovement = GetComponent<PlayerMovement>();
        playerCombat = GetComponent<PlayerCombat>();
        playerStat = GetComponent<PlayerStat>();

        playerAI = new PlayerAI();
        playerAI.Init(this);
    }

    internal void ChangeAnim(EPlayer _ePlayer)
    {
        _ePlayer = playerAI.aiState;

        switch (_ePlayer)
        {
            case EPlayer.WALK:
                anim.SetInteger("PlayerAnim", (int)EPlayerAnim.WALK);
                break;
            case EPlayer.RUN:
                anim.SetInteger("PlayerAnim", (int)EPlayerAnim.RUN);
                break;
            case EPlayer.JUMP:
                anim.SetInteger("PlayerAnim", (int)EPlayerAnim.JUMP);
                break;
            case EPlayer.LAND:
                anim.SetInteger("PlayerAnim", (int)EPlayerAnim.LAND);
                break;
            case EPlayer.ATTACK:
                anim.SetInteger("PlayerAnim", (int)EPlayerAnim.ATTACK);
                break;
            case EPlayer.DIE:
                anim.SetInteger("PlayerAnim", (int)EPlayerAnim.DIE);
                break;
        }
    }

}

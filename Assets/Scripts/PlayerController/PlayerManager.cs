using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerManager : Singleton<PlayerManager>
{
    EPlayer curPlayerState;

    [SerializeField] GameObject rifle;
    [SerializeField] Transform eyeTrs;
    [SerializeField] float checkDistance;

    ViewState viewState;
    UIInteraction uiInteraction;

    bool isInteraction;

    public PlayerAI playerAI { get; private set; }
    public CapsuleCollider cap { get; private set; }
    public Animator anim { get; private set; }
    public PlayerMovement playerMovement { get; private set; }
    public PlayerCombat playerCombat { get; private set; }
    public PlayerStatus playerStatus { get; private set; }
    public GameObject Rifle { get { return rifle; } }

    protected override void Awake()
    {
        base.Awake();

        cap = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();

        viewState = GameManager.instance.ViewState;

        playerMovement = GetComponent<PlayerMovement>();
        playerCombat = GetComponent<PlayerCombat>();
        playerStatus = GetComponent<PlayerStatus>();

        playerAI = new PlayerAI();
        playerAI.Init(this);
    }

    private void Update()
    {
        playerAI.State();
        ChangeAnim(playerAI.aiState);
        //CheckObject();
    }

    //private void CheckObject()
    //{
    //    if (viewState.CurViewState == EViewState.FPS &&
    //        Physics.Raycast(eyeTrs.position, Camera.main.transform.forward, 
    //        out RaycastHit hit, checkDistance, LayerMask.GetMask("DropBullet")))
    //    {
    //        if (!isInteraction)
    //        {
    //            uiInteraction.OpenPanel();

    //            DropBullet dropBullet = hit.collider.gameObject.GetComponent<DropBullet>();

    //            isInteraction = true;
    //        }
    //    }
    //    else
    //    {
    //        uiInteraction.ClosePanel();

    //        isInteraction = false;
    //    }
    //}


    private void ChangeAnim(EPlayer _ePlayer)
    {
        if (curPlayerState == EPlayer.DIE) return;

        curPlayerState = _ePlayer;

        switch (_ePlayer)
        {
            case EPlayer.MOVE:
                anim.SetInteger("PlayerAnim", (int)EPlayerAnim.MOVE);
                break;
            case EPlayer.DIE:
                anim.SetInteger("PlayerAnim", (int)EPlayerAnim.DIE);
                break;
        }
    }

}

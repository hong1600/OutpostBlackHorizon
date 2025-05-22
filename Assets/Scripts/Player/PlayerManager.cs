using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    EPlayer curPlayerState;

    [SerializeField] GameObject rifle;
    [SerializeField] Transform eyeTrs;
    [SerializeField] float checkDistance;

    ViewState viewState;
    UIInteraction uiInteraction;
    GunManager gunManager;

    GameObject lastHitObj;
    bool isInteraction;
    public bool isInTurret = false;

    public PlayerAI playerAI { get; private set; }
    public CapsuleCollider cap { get; private set; }
    public Animator anim { get; private set; }
    public PlayerMovement playerMovement { get; private set; }
    public PlayerCombat playerCombat { get; private set; }
    public PlayerStatus playerStatus { get; private set; }
    public AirStrike airStrike { get; private set; }
    public GuideMissile guideMissile { get; private set; }
    public GameObject Rifle { get { return rifle; } }

    protected override void Awake()
    {
        base.Awake();

        cap = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();

        playerMovement = GetComponent<PlayerMovement>();
        playerCombat = GetComponent<PlayerCombat>();
        playerStatus = GetComponent<PlayerStatus>();
        airStrike = GetComponent<AirStrike>();
        guideMissile = GetComponent<GuideMissile>();

        playerAI = new PlayerAI();
        playerAI.Init(this);
    }

    private void Start()
    {
        viewState = GameManager.instance.ViewState;
        gunManager = GunManager.instance;
        uiInteraction = GameUI.instance.UIInteraction;
    }

    private void Update()
    {
        playerAI.State();
        ChangeAnim(playerAI.aiState);
        CheckObject();
    }

    private void CheckObject()
    {
        if (viewState.CurViewState == EViewState.FPS &&
            Physics.Raycast(eyeTrs.position, Camera.main.transform.forward,
            out RaycastHit hit, checkDistance, LayerMask.GetMask("Object", "Turret")))
        {
            DropBullet dropBullet = hit.collider.GetComponent<DropBullet>();

            if(dropBullet != null)
            {
                if(hit.collider.gameObject != lastHitObj) 
                {
                    uiInteraction.OpenPanel(EObject.BULLET);
                    lastHitObj = hit.collider.gameObject;
                    isInteraction = true;
                }

                if (Input.GetKeyDown(KeyCode.F))
                {
                    gunManager.FillBullet(dropBullet.BulletAmount);
                    Destroy(dropBullet.gameObject);
                    uiInteraction.ClosePanel();
                    isInteraction = false;
                    lastHitObj = null;
                }
                return;
            }
            else
            {
                GrenadeTurret turret = hit.collider.GetComponent<GrenadeTurret>();

                if(turret != null) 
                {
                    if (hit.collider.gameObject != lastHitObj)
                    {
                        uiInteraction.OpenPanel(EObject.TURRET);
                        lastHitObj = hit.collider.gameObject;
                        isInteraction = true;
                    }

                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        turret.enabled = true;
                        turret.ChangeTurretView();
                        isInTurret = true;

                        uiInteraction.ClosePanel();
                        isInteraction = false;
                        lastHitObj = null;
                    }
                    return;
                }
            }
        }

        if (isInteraction)
        {
            uiInteraction.ClosePanel();
            isInteraction = false;
            lastHitObj = null;
        }
    }


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

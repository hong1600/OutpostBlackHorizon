using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;
using UnityEngine.Animations.Rigging;

public class PlayerMng : MonoBehaviour
{
    public PlayerAI playerAI { get; private set; }
    public CapsuleCollider cap { get; private set; }
    public Animator anim { get; private set; }

    public PlayerMovement playerMovement;
    public PlayerCombat playerCombat;

    public event Action onTakeDmg;

    public float maxHp { get; private set; } = 100f;
    public float curHp { get; private set; } = 100f;

    private void Awake()
    {
        Shared.playerMng = this;
        cap = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();

        playerAI = new PlayerAI();
        playerAI.Init(playerMovement, playerCombat);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T)) 
        {
            TakeDmg(10);
        }
    }

    public void TakeDmg(int _dmg)
    {
        if (curHp > 0)
        {
            curHp -= _dmg;
            curHp = Mathf.Clamp(curHp, 0, maxHp);
            onTakeDmg?.Invoke();
        }
        if (curHp <= 0)
        {
            playerMovement.isDie = true;
            StartCoroutine(playerMovement.StartDie());
        }
    }
}

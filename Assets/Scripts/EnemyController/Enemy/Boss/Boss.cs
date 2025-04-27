using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class Boss : Enemy
{
    UIBossHpbar hpbar;

    protected virtual void Start()
    {
        hpbar = GameUI.instance.UIBossHpbar;

        hpbar.Init(this);
    }

    protected override IEnumerator StartDie()
    {
        yield return null;
    }

    protected override void Die()
    {
        GameManager.instance.GameState.SetGameState(EGameState.GAMECLEAR);
        base.Die();
    }
}

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
        StartCoroutine(GameUI.instance.StartBlackout(0.5f));
        GameManager.instance.ViewState.SwitchNone();
        CameraManager.instance.CutScene.PlayCutScene(ECutScene.FINISH);
        StartCoroutine(StartDie());
        base.Die();
    }

    IEnumerator StartBossDie()
    {

        yield return null;
    }
}

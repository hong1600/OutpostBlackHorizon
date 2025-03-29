using DG.Tweening.Core.Easing;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewState : MonoBehaviour
{
    public event Action<EViewState> onViewStateChange;

    [Header("FpsComponent")]
    PlayerMovement playerMovement;
    PlayerCombat playerCombat;
    [SerializeField] GunManager gunManager;
    [SerializeField] GunMovement gunMovement;

    [Header("TopComponent")]
    FieldBuild fieldBuild;

    [SerializeField] EViewState curViewState;

    [SerializeField] List<GameObject> fpsObj;
    [SerializeField] List<GameObject> topObj;

    private void Awake()
    {
        curViewState = EViewState.FPS;
    }

    private void Start()
    {
        playerMovement = Shared.playerManager.playerMovement;
        playerCombat = Shared.playerManager.playerCombat;
        fieldBuild = Shared.fieldManager.FieldBuild;

        UpdateState(curViewState);
        onViewStateChange?.Invoke(curViewState);
    }

    public void SetViewState(EViewState _state)
    {
        if (curViewState == _state) return;

        curViewState = _state;
        UpdateState(_state);
        onViewStateChange?.Invoke(_state);
    }

    public EViewState GetViewState()
    {
        return curViewState;
    }

    private void UpdateState(EViewState _state)
    {
        switch (_state) 
        {
            case EViewState.FPS:
                SwitchFps();
                break;
            case EViewState.TOP:
                SwitchTop();
                break;
            case EViewState.NONE:
                SwitchNone();
                break;
        }
    }

    private void SwitchFps()
    {
        playerMovement.enabled = true;
        playerCombat.enabled = true;
        gunMovement.enabled = true;
        gunManager.enabled = true;
        fieldBuild.enabled = false;

        for(int i = 0; i < topObj.Count; i++) 
        {
            topObj[i].SetActive(false);
        }
        for (int i = 0; i < topObj.Count; i++)
        {
            topObj[i].SetActive(true);
        }
        Shared.gameUI.SwitchFps();
    }

    private void SwitchTop()
    {
        playerMovement.enabled = false;
        playerCombat.enabled = false;
        gunMovement.enabled = false;
        gunManager.enabled = false;
        fieldBuild.enabled = true;

        for (int i = 0; i < fpsObj.Count; i++)
        {
            fpsObj[i].SetActive(false);
        }
        for (int i = 0; i < fpsObj.Count; i++)
        {
            fpsObj[i].SetActive(true);
        }
        Shared.gameUI.SwitchTop();
    }

    private void SwitchNone()
    {
        playerMovement.enabled = false;
        playerCombat.enabled = false;
        gunMovement.enabled = false;
        gunManager.enabled = false;
        fieldBuild.enabled = false;

        for (int i = 0; i < fpsObj.Count; i++)
        {
            fpsObj[i].SetActive(false);
        }
        for (int i = 0; i < topObj.Count; i++)
        {
            topObj[i].SetActive(false);
        }
        Shared.gameUI.SwitchNone();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewState : MonoBehaviour
{
    public event Action<EViewState> onViewStateChange;

    PlayerMovement playerMovement;
    PlayerCombat playerCombat;
    FieldBuild fieldBuild;

    [SerializeField] EViewState curViewState;

    [SerializeField] List<GameObject> fpsUI;
    [SerializeField] List<GameObject> TopUI;

    private void Awake()
    {
        curViewState = EViewState.TOP;
    }

    private void Start()
    {
        playerMovement = Shared.playerManager.playerMovement;
        playerCombat = Shared.playerManager.playerCombat;
        fieldBuild = Shared.fieldManager.FieldBuild;

        UpdateState(curViewState);
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
                SwitchFPS();
                break;
            case EViewState.TOP:
                SwitchTOP();
                break;
        }
    }

    private void SwitchFPS()
    {
        playerMovement.enabled = true;
        playerCombat.enabled = true;
        fieldBuild.enabled = false;

        for(int i = 0; i < TopUI.Count; i++) 
        {
            TopUI[i].SetActive(false);
        }
        for (int i = 0; i < fpsUI.Count; i++)
        {
            fpsUI[i].SetActive(true);
        }
    }

    private void SwitchTOP()
    {
        playerMovement.enabled = false;
        playerCombat.enabled = false;
        fieldBuild.enabled = true;

        for (int i = 0; i < fpsUI.Count; i++)
        {
            fpsUI[i].SetActive(false);
        }
        for (int i = 0; i < TopUI.Count; i++)
        {
            TopUI[i].SetActive(true);
        }
    }
}

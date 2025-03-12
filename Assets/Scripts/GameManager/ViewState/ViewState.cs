using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewState : MonoBehaviour
{
    PlayerMovement playerMovement;
    PlayerCombat playerCombat;
    FieldBuild fieldBuild;

    EViewState curViewState;

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
        curViewState = _state;
        UpdateState(_state);
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
    }

    private void SwitchTOP()
    {
        playerMovement.enabled = false;
        playerCombat.enabled = false;
        fieldBuild.enabled = true;
    }
}

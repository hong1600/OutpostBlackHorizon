using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IViewState
{
    void SetViewState(EViewState _state);
    EViewState GetViewState();
}

public class ViewState : MonoBehaviour, IViewState
{
    EViewState curViewState;

    private void Awake()
    {
        curViewState = EViewState.FPS;
    }

    public void SetViewState(EViewState _state)
    {
        curViewState = _state;
    }

    public EViewState GetViewState()
    {
        return curViewState;
    }
}

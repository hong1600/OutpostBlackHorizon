using DG.Tweening.Core.Easing;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewState : MonoBehaviour
{
    public event Action<EViewState> onViewStateChange;

    [SerializeField] EViewState curViewState;

    List<MonoBehaviour> topComponent = new List<MonoBehaviour>();
    List<MonoBehaviour> fpsComponent = new List<MonoBehaviour>();

    private void Awake()
    {
        curViewState = EViewState.FPS;
    }

    private void Start()
    {
        //fps
        fpsComponent.Add(Shared.playerManager.playerMovement);
        fpsComponent.Add(Shared.playerManager.playerCombat);
        fpsComponent.Add(Shared.gunManager);
        fpsComponent.Add(Shared.gunManager.GunMovement);
        fpsComponent.Add(Shared.cameraManager.CameraFpsMove);
        fpsComponent.Add(Shared.cameraManager.CameraFpsShake);
        fpsComponent.Add(Shared.cameraManager.CameraFpsZoom);

        //top
        topComponent.Add(Shared.fieldManager.FieldBuild);
        topComponent.Add(Shared.cameraManager.CameraTopMove);
        topComponent.Add(Shared.cameraManager.CameraTopZoom);

        onViewStateChange?.Invoke(curViewState);
    }

    public void SetViewState(EViewState _state)
    {
        if (curViewState == _state) return;

        curViewState = _state;
        onViewStateChange?.Invoke(_state);
    }

    public void UpdateState(EViewState _state)
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
        for(int i = 0; i < topComponent.Count; i++) 
        {
            topComponent[i].enabled = false;
        }
        for (int i = 0; i < fpsComponent.Count; i++)
        {
            fpsComponent[i].enabled = true;
        }
        Shared.gameUI.SwitchFps();
    }

    private void SwitchTop()
    {
        for (int i = 0; i < fpsComponent.Count; i++)
        {
            fpsComponent[i].enabled = false;
        }
        for (int i = 0; i < topComponent.Count; i++)
        {
            topComponent[i].enabled = true;
        }

        Shared.gameUI.SwitchTop();
    }

    private void SwitchNone()
    {
        for (int i = 0; i < topComponent.Count; i++)
        {
            topComponent[i].enabled = false;
        }
        for (int i = 0; i < fpsComponent.Count; i++)
        {
            fpsComponent[i].enabled = false;
        }

        Shared.gameUI.SwitchNone();
    }

    public EViewState CurViewState { get { return curViewState; } }
}

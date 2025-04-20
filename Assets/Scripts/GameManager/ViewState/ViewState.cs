using DG.Tweening.Core.Easing;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewState : MonoBehaviour
{
    public event Action<EViewState> onViewStateChange;

    [SerializeField] EViewState curViewState;

    GameUI gameUI;

    List<MonoBehaviour> topComponent = new List<MonoBehaviour>();
    List<MonoBehaviour> fpsComponent = new List<MonoBehaviour>();

    private void Start()
    {
        fpsComponent.Add(PlayerManager.instance.playerMovement);
        fpsComponent.Add(PlayerManager.instance.playerCombat);
        fpsComponent.Add(GunManager.instance);
        fpsComponent.Add(GunManager.instance.GunMovement);
        fpsComponent.Add(CameraManager.instance.CameraFpsMove);
        fpsComponent.Add(CameraManager.instance.CameraFpsShake);
        fpsComponent.Add(CameraManager.instance.CameraFpsZoom);

        topComponent.Add(FieldManager.instance.FieldBuild);
        topComponent.Add(CameraManager.instance.CameraTopMove);
        topComponent.Add(CameraManager.instance.CameraTopZoom);

        gameUI = GameUI.instance;
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
        gameUI.SwitchFps();
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

        gameUI.SwitchTop();
    }

    public void SwitchNone()
    {
        curViewState = EViewState.NONE;

        for (int i = 0; i < topComponent.Count; i++)
        {
            topComponent[i].enabled = false;
        }
        for (int i = 0; i < fpsComponent.Count; i++)
        {
            fpsComponent[i].enabled = false;
        }

        gameUI.SwitchNone();
    }

    public EViewState CurViewState { get { return curViewState; } }
}

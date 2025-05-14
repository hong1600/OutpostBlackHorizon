using DG.Tweening.Core.Easing;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewState : MonoBehaviour
{
    public event Action<EViewState> onViewStateChange;

    CameraTopToFps cameraTopToFps;
    CameraFpsZoom cameraFpsZoom;

    [SerializeField] EViewState curViewState;

    List<MonoBehaviour> topComponent = new List<MonoBehaviour>();
    List<MonoBehaviour> fpsComponent = new List<MonoBehaviour>();
    List<MonoBehaviour> turretComponent = new List<MonoBehaviour>();
    GameObject rifle;

    private void Start()
    {
        fpsComponent.Add(PlayerManager.instance.playerMovement);
        fpsComponent.Add(PlayerManager.instance.playerCombat);
        fpsComponent.Add(GunManager.instance);
        fpsComponent.Add(GunManager.instance.GunMovement);
        fpsComponent.Add(CameraManager.instance.CameraFpsMove);
        fpsComponent.Add(CameraManager.instance.CameraFpsShake);
        fpsComponent.Add(CameraManager.instance.CameraFpsZoom);
        rifle = PlayerManager.instance.Rifle;

        topComponent.Add(FieldManager.instance.FieldBuild);
        topComponent.Add(CameraManager.instance.CameraTopMove);
        topComponent.Add(CameraManager.instance.CameraTopZoom);

        turretComponent.Add(CameraManager.instance.CameraTurretMove);

        cameraTopToFps = CameraManager.instance.CameraTopToFps;
        cameraFpsZoom = CameraManager.instance.CameraFpsZoom;
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
            case EViewState.TURRET:
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

        for (int i = 0; i < turretComponent.Count; i++)
        {
            turretComponent[i].enabled = false;
        }

        for (int i = 0; i < fpsComponent.Count; i++)
        {
            fpsComponent[i].enabled = true;
            rifle.SetActive(true);
        }

        GameUI.instance.SwitchFps();
    }

    private void SwitchTop()
    {
        if(cameraFpsZoom.isZoom)
        {
            cameraFpsZoom.ZoomCamera();
        }

        for (int i = 0; i < fpsComponent.Count; i++)
        {
            fpsComponent[i].enabled = false;
        }

        for (int i = 0; i < topComponent.Count; i++)
        {
            topComponent[i].enabled = true;
            rifle.SetActive(false);
        }

        GameUI.instance.SwitchTop();
    }

    public void SwitchNone()
    {
        curViewState = EViewState.NONE;

        if (cameraFpsZoom != null)
        {
            if (cameraFpsZoom.isZoom)
            {
                cameraFpsZoom.ZoomCamera();
            }
        }

        for (int i = 0; i < topComponent.Count; i++)
        {
            topComponent[i].enabled = false;
        }

        for (int i = 0; i < fpsComponent.Count; i++)
        {
            fpsComponent[i].enabled = false;
        }

        for (int i = 0; i < turretComponent.Count; i++)
        {
            turretComponent[i].enabled = false;
        }

        if (rifle == null)
        {
            rifle = PlayerManager.instance.Rifle;
        }
        rifle.SetActive(false);
        GameUI.instance.SwitchNone();
    }

    public IEnumerator SwitchInTurret(Vector3 _pos, Quaternion _rot)
    {
        curViewState = EViewState.TURRET;

        if (cameraFpsZoom.isZoom)
        {
            cameraFpsZoom.ZoomCamera();
        }

        for (int i = 0; i < topComponent.Count; i++)
        {
            topComponent[i].enabled = false;
        }
        for (int i = 0; i < fpsComponent.Count; i++)
        {
            fpsComponent[i].enabled = false;
        }
        for (int i = 0; i < turretComponent.Count; i++)
        {
            turretComponent[i].enabled = true;
        }

        StartCoroutine(GameUI.instance.StartBlackout(0.5f));
        cameraTopToFps.SetInTurretMode(_pos, _rot, EViewState.TURRET);
        StartCoroutine(CameraManager.instance.CameraFpsZoom.StartZoom(50f));

        yield return new WaitForSeconds(0.5f);

        GameUI.instance.SwitchTurret();
    }

    public EViewState CurViewState { get { return curViewState; } }
}

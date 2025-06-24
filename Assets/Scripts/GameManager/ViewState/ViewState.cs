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
    CameraFpsShake cameraFpsShake;

    [SerializeField] EViewState curViewState;

    List<MonoBehaviour> topComponent = new List<MonoBehaviour>();
    List<MonoBehaviour> fpsComponent = new List<MonoBehaviour>();
    List<MonoBehaviour> turretComponent = new List<MonoBehaviour>();
    GameObject rifle;

    private void Start()
    {
        GameManager.instance.PlayerSpawner.onSpawnPlayer += InitPlayer;

        cameraTopToFps = CameraManager.instance.CameraTopToFps;
        cameraFpsZoom = CameraManager.instance.CameraFpsZoom;
        cameraFpsShake = CameraManager.instance.CameraFpsShake;

        fpsComponent.Add(CameraManager.instance.CameraFpsMove);
        fpsComponent.Add(cameraFpsZoom);

        topComponent.Add(FieldManager.instance.FieldBuild);
        topComponent.Add(CameraManager.instance.CameraTopMove);
        topComponent.Add(CameraManager.instance.CameraTopZoom);

        turretComponent.Add(CameraManager.instance.CameraTurretMove);
    }

    private void InitPlayer()
    {
        fpsComponent.Add(GameManager.instance.PlayerSpawner.player.GetComponent<PlayerMovement>());
        fpsComponent.Add(GameManager.instance.PlayerSpawner.player.GetComponent<PlayerCombat>());
        fpsComponent.Add(GameManager.instance.PlayerSpawner.player.GetComponent<PlayerManager>().GunManager);
        fpsComponent.Add(GameManager.instance.PlayerSpawner.player.GetComponent<PlayerManager>().GunManager.GunMovement);
        rifle = GameManager.instance.PlayerSpawner.player.GetComponent<PlayerManager>().Rifle;
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
            rifle = GameManager.instance.PlayerSpawner.player.GetComponent<PlayerManager>().Rifle;
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

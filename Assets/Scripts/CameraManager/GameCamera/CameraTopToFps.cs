using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;

public class CameraTopToFps : MonoBehaviour
{
    Camera mainCam;

    [SerializeField] GameObject fpsMove;
    [SerializeField] GameObject fpsShake;
    [SerializeField] GameObject fpsZoom;

    [SerializeField] GameObject playerObj;
    [SerializeField] Transform playerEyeTrs;
    [SerializeField] GameObject rifle;
    [SerializeField] Transform topTrs;

    public bool isArrive { get; private set; }

    Quaternion playerRot;

    private void Awake()
    {
        mainCam = Camera.main;
        Shared.gameManager.ViewState.onViewStateChange += SetCameraMode;
    }

    private void Start()
    {
        isArrive = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isArrive)
        {
            if (Shared.gameManager.ViewState.CurViewState == EViewState.FPS)
            {
                Shared.gameManager.ViewState.SetViewState(EViewState.TOP);
            }
            else
            {
                Shared.gameManager.ViewState.SetViewState(EViewState.FPS);
            }
        }
    }

    private void SetCameraMode(EViewState _eGameState)
    {
        isArrive = false;

        if (_eGameState == EViewState.FPS)
        {
            mainCam.transform.SetParent(playerObj.transform);
            MoveCamera(_eGameState);
        }
        else if( _eGameState == EViewState.TOP)
        {
            rifle.transform.SetParent(playerObj.transform);
            MoveCamera(_eGameState);
        }
        else
        {
            return;
        }
    }

    private void MoveCamera(EViewState _eGameState)
    {
        isArrive = false;

        Vector3 targetTrs = (Shared.gameManager.ViewState.CurViewState == EViewState.FPS) ?
            playerEyeTrs.position : topTrs.position;
        Quaternion targetRot = (Shared.gameManager.ViewState.CurViewState == EViewState.FPS) ?
            playerObj.transform.rotation : topTrs.rotation;

        StartCoroutine(StartMoveCamera(targetTrs, targetRot, _eGameState));
    }

    private IEnumerator StartMoveCamera(Vector3 _targetTrs, Quaternion _targetRot, EViewState _eGameState)
    {
        mainCam.transform.DOMove(_targetTrs, 1.5f)
        .SetEase(Ease.InOutSine);

        mainCam.transform.DORotateQuaternion(_targetRot, 1.5f)
            .SetEase(Ease.InOutSine);

        yield return new WaitForSeconds(1.51f);

        SwitchMode();

        yield return new WaitForEndOfFrame();

        Shared.gameManager.ViewState.UpdateState(_eGameState);
        isArrive = true;
    }

    private void SwitchMode()
    {
        bool isFPS = Shared.gameManager.ViewState.CurViewState == EViewState.FPS;

        if(isFPS) 
        {
            rifle.transform.SetParent(mainCam.transform, true);
            rifle.transform.localPosition = new Vector3(0.07f, -0.27f, 0.29f);
            rifle.transform.localRotation = Quaternion.identity;
            rifle.GetComponent<GunMovement>().InitPos();
        }
        else
        {
            mainCam.transform.SetParent(null);
        }
    }
}

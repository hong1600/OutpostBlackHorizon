using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;

public class CameraTopToFps : MonoBehaviour
{
    Camera mainCam;

    GameManager gameManager;

    CameraFpsShake cameraShake;

    [SerializeField] GameObject playerObj;
    [SerializeField] Transform playerEyeTrs;
    [SerializeField] GameObject rifle;
    [SerializeField] Transform topTrs;

    public bool isArrive { get; private set; }

    Quaternion playerRot;

    private void Awake()
    {
        mainCam = Camera.main;
        isArrive = true;
    }

    private void Start()
    {
        isArrive = true;

        gameManager = GameManager.instance;
        gameManager.ViewState.onViewStateChange += SetCameraMode;
        cameraShake = CameraManager.instance.CameraFpsShake;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && isArrive)
        {
            if (gameManager.ViewState.CurViewState == EViewState.FPS)
            {
                gameManager.ViewState.SetViewState(EViewState.TOP);
            }
            else if (gameManager.ViewState.CurViewState == EViewState.TOP)
            {
                gameManager.ViewState.SetViewState(EViewState.FPS);
            }
        }
    }

    private void SetCameraMode(EViewState _eViewState)
    {
        isArrive = false;

        if (_eViewState == EViewState.FPS)
        {
            mainCam.transform.SetParent(playerObj.transform);
            StartCoroutine(StartMoveCamera(playerEyeTrs.position, playerObj.transform.rotation, _eViewState, 1.5f));
        }
        else if(_eViewState == EViewState.TOP)
        {
            rifle.transform.SetParent(playerObj.transform);
            StartCoroutine(StartMoveCamera(topTrs.position, topTrs.transform.rotation, _eViewState, 1.5f));
        }
        else
        {
            return;
        }
    }

    public void SetInTurretMode(Vector3 _pos, Quaternion _rot, EViewState _eViewState)
    {
        rifle.transform.SetParent(playerObj.transform);

        StartCoroutine(StartMoveCamera(_pos, _rot, _eViewState, 0.4f));
    }

    private IEnumerator StartMoveCamera(Vector3 _targetTrs, Quaternion _targetRot, EViewState _eViewState, float _time)
    {
        isArrive = false;

        mainCam.transform.DOMove(_targetTrs, _time)
        .SetEase(Ease.InOutSine);

        mainCam.transform.DORotateQuaternion(_targetRot, _time)
            .SetEase(Ease.InOutSine);

        yield return new WaitForSeconds(_time + 0.01f);

        if(_eViewState != EViewState.TURRET) 
        {
            SwitchMode();
        }
        else
        {
            mainCam.transform.SetParent(CameraManager.instance.CameraTurretMove.turretObj.transform);
        }

        yield return new WaitForEndOfFrame();

        gameManager.ViewState.UpdateState(_eViewState);
        isArrive = true;
        cameraShake.Init();
    }

    private void SwitchMode()
    {
        bool isFPS = gameManager.ViewState.CurViewState == EViewState.FPS;

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

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFpsMove : MonoBehaviour
{
    Camera mainCam;

    InputManager inputManager;
    ViewState viewState;
    CameraTopToFps topToFps;

    GameObject playerObj;
    [SerializeField] float mouseSpeed = 2f;
    float verticalRotation = 0f;

    float walkFOV = 60f;
    float runFOV = 70f;
    float duration = 0.12f;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void Start()
    {
        inputManager = InputManager.instance;
        viewState = GameManager.instance.ViewState;
        topToFps = CameraManager.instance.CameraTopToFps;
        playerObj = GameManager.instance.PlayerSpawner.player;
    }

    private void LateUpdate()
    {
        if (inputManager.isInputLock) return;

        LookMouse();
    }

    private void LookMouse()
    {
        if (viewState.CurViewState == EViewState.FPS && topToFps.isArrive)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            playerObj.transform.Rotate(Vector3.up * mouseX * mouseSpeed);

            verticalRotation -= mouseY * mouseSpeed;
            verticalRotation = Mathf.Clamp(verticalRotation, -60f, 60f);
            mainCam.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        }
    }

    public void SetRunFOV(bool _isRun)
    {
        if (_isRun)
        {
            mainCam.DOFieldOfView(runFOV, duration).SetEase(Ease.OutSine);
        }
        else
        {
            mainCam.DOFieldOfView(walkFOV, duration).SetEase(Ease.OutSine);
        }
    }
}

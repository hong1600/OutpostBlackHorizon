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

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void Start()
    {
        inputManager = InputManager.instance;
        viewState = GameManager.instance.ViewState;
        topToFps = CameraManager.instance.CameraTopToFps;
        playerObj = PlayerManager.instance.gameObject;
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
}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;

public class CameraTopToFps : MonoBehaviour
{
    EViewState curViewState;

    Camera mainCam;
    PlayerMovement playerMovement;
    PlayerCombat playerCombat;

    [SerializeField] GameObject fpsShake;
    [SerializeField] GameObject fpsZoom;

    [SerializeField] GameObject playerObj;
    [SerializeField] Transform playerEyeTrs;
    [SerializeField] GameObject rifle;
    [SerializeField] Transform topTrs;
    [SerializeField] GameObject aimMouse;
    [SerializeField] GameObject functionUI;
    [SerializeField] GameObject customMouse;
    [SerializeField] GameObject centerLine;

    public bool isArrive { get; private set; }

    Quaternion playerRot;

    [SerializeField] float mouseSpeed = 2f;
    float verticalRotation = 0f;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void Start()
    {
        curViewState = Shared.gameManager.ViewState.GetViewState();

        isArrive = true;

        playerMovement = playerObj.GetComponent<PlayerMovement>();
        playerCombat = playerObj.GetComponent<PlayerCombat>();
        SetCameraMode(Shared.gameManager.ViewState.GetViewState());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isArrive)
        {
            if (curViewState == EViewState.FPS)
            {
                Shared.gameManager.ViewState.SetViewState(EViewState.TOP);
            }
            else
            {
                Shared.gameManager.ViewState.SetViewState(EViewState.FPS);
            }

            curViewState = Shared.gameManager.ViewState.GetViewState();

            SetCameraMode(curViewState);
        }
    }

    private void LateUpdate()
    {
        LookMouse();
    }

    private void LookMouse()
    {
        if (curViewState == EViewState.FPS && isArrive)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            playerObj.transform.Rotate(Vector3.up * mouseX * mouseSpeed);

            verticalRotation -= mouseY * mouseSpeed;
            verticalRotation = Mathf.Clamp(verticalRotation, -60f, 60f);
            mainCam.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        }
    }

    private void SetCameraMode(EViewState _eGameState)
    {
        isArrive = false;

        if (_eGameState == EViewState.FPS)
        {
            mainCam.transform.SetParent(playerObj.transform);
            customMouse.SetActive(false);
            aimMouse.SetActive(true);
            functionUI.SetActive(false);
            centerLine.SetActive(false);
            MoveCamera();
        }
        else
        {
            playerMovement.enabled = false;
            playerCombat.enabled = false;
            rifle.transform.SetParent(playerObj.transform);
            MoveCamera();
        }
    }

    private void MoveCamera()
    {
        isArrive = false;

        Vector3 targetTrs = (curViewState == EViewState.FPS) ? playerEyeTrs.position : topTrs.position;
        Quaternion targetRot = (curViewState == EViewState.FPS) ? playerObj.transform.rotation : topTrs.rotation;

        StartCoroutine(StartMoveCamera(targetTrs, targetRot));
    }

    private IEnumerator StartMoveCamera(Vector3 _targetTrs, Quaternion _targetRot)
    {
        mainCam.transform.DOMove(_targetTrs, 1.5f)
        .SetEase(Ease.InOutSine);

        mainCam.transform.DORotateQuaternion(_targetRot, 1.5f)
            .SetEase(Ease.InOutSine);

        yield return new WaitForSeconds(1.51f);

        if (curViewState == EViewState.FPS)
        {
            rifle.transform.SetParent(mainCam.transform, true);
            rifle.transform.localPosition = new Vector3(0.07f, -0.27f, 0.29f);
            rifle.transform.localRotation = Quaternion.Euler(180, 0, 180);
            rifle.GetComponent<GunMovement>().InitPos();
            playerMovement.enabled = true;
            playerCombat.enabled = true;
            fpsShake.SetActive(true);
            fpsZoom.SetActive(true);
        }
        else if (curViewState == EViewState.TOP)
        {
            mainCam.transform.SetParent(null);
            customMouse.SetActive(true);
            aimMouse.SetActive(false);
            functionUI.SetActive(true);
            fpsShake.SetActive(false);
            fpsZoom.SetActive(false);
            centerLine.SetActive(true);
        }

        isArrive = true;
    }

    private void SwitchFPS()
    {
    }

    private void SwitchTOP()
    {
    }
}

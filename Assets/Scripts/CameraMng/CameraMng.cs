using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading;

public class CameraMng : MonoBehaviour
{
    enum ECamMode { FPS, TOP}
    ECamMode curCamMode = ECamMode.TOP;

    Camera mainCam;
    NormalPlayer normalPlayer;

    [SerializeField] GameObject player;
    [SerializeField] GameObject rifle;
    [SerializeField] Transform topTrs;
    [SerializeField] GameObject customMouse;
    [SerializeField] GameObject aimMouse;
    [SerializeField] GameObject functionUI;
    [SerializeField] bool isArrive;

    Vector3 playerEye;

    float mouseX;
    float mouseY;
    [SerializeField] float mouseSpeed = 3f;
    float verticalRotation = 0f;

    private void Awake()
    {
        mainCam = Camera.main;
        normalPlayer = player.GetComponent<NormalPlayer>();
        normalPlayer.enabled = false;
        Shared.cameraMng = this;
    }

    private void Start()
    {
        playerEye = player.transform.position + new Vector3 (0.03f, 1.47f, 0.15f);
        isArrive = true;
        SetCameraMode(ECamMode.TOP);
        mainCam.transform.position = topTrs.position;
        mainCam.transform.rotation = topTrs.rotation;
    }

    private void Update()
    {
        CursorLock();

        if(curCamMode == ECamMode.FPS) 
        {
            CheckInput();
            LookMouse();
        }

        if (Input.GetKeyDown(KeyCode.F) && isArrive)
        {
            if(curCamMode == ECamMode.FPS) 
            {
                SetCameraMode(ECamMode.TOP);
            }
            else
            {
                SetCameraMode(ECamMode.FPS);
            }
        }
    }

    private void CheckInput()
    {
        mouseX = Input.GetAxisRaw("Mouse X") * mouseSpeed;
        mouseY = Input.GetAxisRaw("Mouse Y") * mouseSpeed;
    }

    private void LookMouse()
    {
        player.transform.Rotate(Vector3.up * mouseX);

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -60f, 60f);
        transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }

    private void SetCameraMode(ECamMode _eCamMode)
    {
        curCamMode = _eCamMode;

        isArrive = false;

        if(curCamMode == ECamMode.FPS) 
        {
            mainCam.transform.SetParent(player.transform);
            customMouse.SetActive(false);
            aimMouse.SetActive(true);
            functionUI.SetActive(false);
            normalPlayer.enabled = true;
            MoveCamera();
        }
        else
        {
            mainCam.transform.SetParent(null);
            rifle.transform.SetParent(player.transform);
            customMouse.SetActive(true);
            aimMouse.SetActive(false);
            functionUI.SetActive(true);
            normalPlayer.enabled = false;
            MoveCamera();
        }
    }

    private void MoveCamera()
    {
        isArrive = false;

        Vector3 targetTrs = (curCamMode == ECamMode.FPS) ? playerEye : topTrs.position;
        Quaternion targetRot = (curCamMode == ECamMode.FPS) ? player.transform.rotation : topTrs.rotation;

        StartCoroutine(StartMoveCamera(targetTrs, targetRot));
    }

    private IEnumerator StartMoveCamera(Vector3 _targetTrs, Quaternion _targetRot)
    {
        mainCam.transform.DOMove(_targetTrs, 1.25f)
        .SetEase(Ease.InOutSine);

        mainCam.transform.DORotateQuaternion(_targetRot, 1.25f)
            .SetEase(Ease.InOutSine)
            .OnComplete(OnIsArrive);

        yield return new WaitForSeconds(1.25f);

        if (curCamMode == ECamMode.FPS)
        {
            //rifle.transform.SetParent(mainCam.transform, true);
        }
    }

    private void OnIsArrive()
    {
        isArrive = true;
    }

    private void CursorLock()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Cursor.lockState == CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading;
using Unity.VisualScripting;

public class CameraMng : MonoBehaviour
{
    EViewState curViewState;

    Camera mainCam;

    [SerializeField] GameObject playerObj;
    [SerializeField] GameObject rifle;
    [SerializeField] Transform topTrs;
    [SerializeField] GameObject customMouse;
    [SerializeField] GameObject aimMouse;
    [SerializeField] GameObject functionUI;
    public bool isArrive { get; private set; }

    Vector3 playerTrs;
    Quaternion playerRot;
    [SerializeField] float lerpSpeed;

    [SerializeField] float mouseSpeed = 2f;
    float verticalRotation = 0f;

    private void Awake()
    {
        mainCam = Camera.main;
        Shared.cameraMng = this;
    }

    private void Start()
    {
        curViewState = Shared.gameMng.iViewState.GetViewState();

        isArrive = true;

        mainCam.transform.position = topTrs.position;
        mainCam.transform.rotation = topTrs.rotation;

        Player player = playerObj.GetComponent<Player>();
        player.enabled = false;
        mainCam.transform.SetParent(null);
        customMouse.SetActive(true);
        aimMouse.SetActive(false);
        functionUI.SetActive(true);
    }

    private void Update()
    {
        CursorLock();
        LookMouse(Shared.inputMng.mouseDelta);

        if (Input.GetKeyDown(KeyCode.F) && isArrive)
        {
            if (curViewState == EViewState.FPS)
            {
                Shared.gameMng.iViewState.SetViewState(EViewState.TOP);
            }
            else
            {
                Shared.gameMng.iViewState.SetViewState(EViewState.FPS);
            }

            curViewState = Shared.gameMng.iViewState.GetViewState();

            SetCameraMode(curViewState);
        }
    }

    private void LookMouse(Vector2 _mouseDelta)
    {
        if (curViewState == EViewState.FPS && isArrive)
        {
            playerObj.transform.Rotate(Vector3.up * _mouseDelta.x * mouseSpeed);

            verticalRotation -= _mouseDelta.y * mouseSpeed;
            verticalRotation = Mathf.Clamp(verticalRotation, -60f, 60f);
            transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        }
    }

    private void SetCameraMode(EViewState _eGameState)
    {
        isArrive = false;

        if(_eGameState == EViewState.FPS)
        {
            playerTrs = playerObj.transform.position + new Vector3(0f, 1.56f, 0.3f);
            mainCam.transform.SetParent(playerObj.transform);
            customMouse.SetActive(false);
            aimMouse.SetActive(true);
            functionUI.SetActive(false);
            MoveCamera();
        }
        else
        {
            Player player = playerObj.GetComponent<Player>();
            player.enabled = false;
            rifle.transform.SetParent(playerObj.transform);
            MoveCamera();
        }
    }

    private void MoveCamera()
    {
        isArrive = false;

        Vector3 targetTrs = (curViewState == EViewState.FPS) ? playerTrs : topTrs.position;
        Quaternion targetRot = (curViewState == EViewState.FPS) ? playerObj.transform.rotation : topTrs.rotation;

        StartCoroutine(StartMoveCamera(targetTrs, targetRot));
    }

    private IEnumerator StartMoveCamera(Vector3 _targetTrs, Quaternion _targetRot)
    {
        //while (Vector3.Distance(mainCam.transform.position, _targetTrs) > 0.01f ||
        //    Quaternion.Angle(mainCam.transform.rotation, _targetRot) > 0.1f)
        //{
        //    mainCam.transform.position = Vector3.Lerp(transform.position, _targetTrs, lerpSpeed * Time.deltaTime);
        //    mainCam.transform.rotation = Quaternion.Lerp(transform.rotation, _targetRot, lerpSpeed * Time.deltaTime);
        //    yield return null;
        //}

        //mainCam.transform.position = _targetTrs;
        //mainCam.transform.rotation = _targetRot;

        mainCam.transform.DOMove(_targetTrs, 1.25f)
        .SetEase(Ease.InOutSine);

        mainCam.transform.DORotateQuaternion(_targetRot, 1.25f)
            .SetEase(Ease.InOutSine);

        yield return new WaitForSeconds(1.26f);

        if (curViewState == EViewState.FPS)
        {
            rifle.transform.SetParent(mainCam.transform, true);
            rifle.GetComponent<GunMovement>().InitPos();
            Player player = playerObj.GetComponent<Player>();
            player.enabled = true;
        }
        else if (curViewState == EViewState.TOP)
        {
            mainCam.transform.SetParent(null);
            customMouse.SetActive(true);
            aimMouse.SetActive(false);
            functionUI.SetActive(true);
        }
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

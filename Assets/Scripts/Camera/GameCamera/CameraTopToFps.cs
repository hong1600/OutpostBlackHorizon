using DG.Tweening;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class CameraTopToFps : MonoBehaviour
{
    Camera mainCam;

    GameManager gameManager;
    CameraFpsShake cameraShake;
    PlayerCombat playerCombat;

    GameObject playerObj;
    Transform playerEyeTrs;

    [SerializeField] Transform topTrs;

    List<GameObject> weaponList = new List<GameObject>();

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
        cameraShake = CameraManager.instance.CameraFpsShake;

        GameManager.instance.PlayerSpawner.onSpawnPlayer += InitPlayer;
        gameManager.ViewState.onViewStateChange += SetCameraMode;
    }

    private void InitPlayer()
    {
        playerObj = gameManager.PlayerSpawner.player;
        playerEyeTrs = gameManager.PlayerSpawner.player.GetComponent<PlayerManager>().playerEyeTrs;
        playerCombat = playerObj.GetComponent<PlayerCombat>();
        weaponList = playerCombat.weaponList;
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
            for (int i = 0; i < weaponList.Count; i++)
            {
                weaponList[i].transform.SetParent(playerObj.transform);
            }

            StartCoroutine(StartMoveCamera(topTrs.position, topTrs.transform.rotation, _eViewState, 1.5f));
        }
        else
        {
            return;
        }
    }

    public void SetInTurretMode(Vector3 _pos, Quaternion _rot, EViewState _eViewState)
    {
        for (int i = 0; i < weaponList.Count; i++)
        {
            weaponList[i].transform.SetParent(playerObj.transform);
        }

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
            SwitchMode(playerCombat.curWeapon);
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


    private void SwitchMode(GameObject _curWeapon)
    {
        bool isFPS = gameManager.ViewState.CurViewState == EViewState.FPS;

        if(isFPS) 
        {
            _curWeapon.transform.SetParent(mainCam.transform, true);
            _curWeapon.transform.localPosition = new Vector3(0.07f, -0.27f, 0.29f);
            _curWeapon.transform.localRotation = Quaternion.identity;
            _curWeapon.GetComponent<GunMovement>().InitPos();
        }
        else
        {
            mainCam.transform.SetParent(null);
        }
    }
}

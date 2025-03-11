using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartDropShipMove : MonoBehaviour
{
    Camera mainCam;
    Cinemachine.CinemachineVirtualCamera virtualCam;
    Cinemachine.CinemachineTransposer virtualTransposer;

    [Header("Movement Settings")]
    [SerializeField] Transform pos1;
    [SerializeField] Transform pos2;
    [SerializeField] float moveSpeed = 100f;

    [Header("Hatch Settings")]
    [SerializeField] GameObject hatch;
    [SerializeField] float hatchSpeed = 2f;

    [Header("Player Settings")]
    [SerializeField] GameObject player;
    [SerializeField] Transform playerStartPos;
    PlayerMovement playerMovement;
    PlayerCombat playerCombat;

    [Header("Camera Transforms")]
    [SerializeField] Transform hatchCamTrs;
    [SerializeField] Transform playerCamTrs;

    [Header("UI Elements")]
    [SerializeField] GameObject HUDCanvas;
    [SerializeField] GameObject TextPanel;
    [SerializeField] GameObject customMouse;
    [SerializeField] GameObject camMng;

    Vector3 dropShipCamPos = new Vector3(0, 30, -30);

    private void Awake()
    {
        mainCam = Camera.main;
        virtualCam = GetComponentInChildren<CinemachineVirtualCamera>();
        virtualTransposer = virtualCam.GetCinemachineComponent<CinemachineTransposer>();
    }

    private void Start()
    {
        playerMovement = Shared.playerManager.playerMovement;
        playerCombat = Shared.playerManager.playerCombat;
        AudioManager.instance.PlayBgm(EBgm.GAMESTART);
        Init();
        StartCoroutine(StartMove());
    }

    private void Init()
    {
        playerMovement.enabled = false;
        playerCombat.enabled = false;
        player.transform.SetParent(transform);
        player.transform.position = playerStartPos.position;
        hatch.transform.localRotation = Quaternion.identity;
        customMouse.SetActive(false);
        camMng.SetActive(false);
        HUDCanvas.SetActive(false);
        TextPanel.SetActive(true);
        virtualTransposer.m_FollowOffset = dropShipCamPos;
    }

    IEnumerator StartMove()
    {
        yield return StartCoroutine(StartMoveToTarget(pos1));

        moveSpeed = 40f;

        yield return StartCoroutine(StartMoveToTarget(pos2));
    }

    IEnumerator StartMoveToTarget(Transform _target)
    {
        while (Vector3.Distance(transform.position, _target.position) > 0.1f)
        {
            Vector3 dir = (_target.position - transform.position).normalized;

            float distance = Vector3.Distance(transform.position, _target.position);
            float speed = Mathf.Lerp(0, moveSpeed, distance / 30f);

            transform.position = Vector3.MoveTowards
                (transform.position, _target.position, speed * Time.deltaTime);
            player.transform.position = playerStartPos.position;

            yield return null;
        }

        TextPanel.SetActive(false);

        if (Vector3.Distance(transform.position, pos2.position) <= 0.1f)
        {
            virtualCam.Follow = null;
            virtualCam.LookAt = null;

            Vector3[] path = new Vector3[]
            {
            virtualCam.transform.position,
            ((virtualCam.transform.localPosition + hatchCamTrs.localPosition) * 0.5f) + Vector3.back * 20,
            hatchCamTrs.position
            };


            virtualCam.transform.DOPath
                (path, 3f, PathType.CatmullRom).SetEase(Ease.InOutSine);
            virtualCam.transform.DORotateQuaternion
                (hatchCamTrs.rotation, 3f).SetEase(Ease.InOutSine);

            yield return new WaitForSeconds(3);

            StartCoroutine(StartOpenHatch());
        }
    }

    IEnumerator StartOpenHatch()
    {
        Quaternion targetRot = Quaternion.Euler(-45, 0, 0);

        hatch.transform.DOLocalRotate(targetRot.eulerAngles, hatchSpeed).SetEase(Ease.InOutSine);

        yield return new WaitForSeconds(hatchSpeed);

        player.transform.SetParent(null);
        hatch.transform.localRotation = targetRot;

        StartCoroutine(StartClosePlayer());
    }

    IEnumerator StartClosePlayer()
    {
        Vector3[] path = new Vector3[]
        {
            virtualCam.transform.position,
            (virtualCam.transform.position + playerCamTrs.position) * 0.5f + Vector3.left * 3,
            playerCamTrs.position
        };

        virtualCam.transform.DOPath(path, 3f, PathType.CatmullRom)
            .SetEase(Ease.InOutSine);
        virtualCam.transform.DORotateQuaternion
            (playerCamTrs.rotation, 3f).SetEase(Ease.InOutSine);

        yield return new WaitForSeconds(3);

        StartCoroutine(StartChangePlayer());
    }

    IEnumerator StartChangePlayer()
    {
        AudioManager.instance.PlayBgm(EBgm.DESERT);
        virtualCam.enabled = false;
        camMng.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        HUDCanvas.SetActive(true);
        this.enabled = false;
        playerMovement.enabled = true;
        playerCombat.enabled = true;
    }
}

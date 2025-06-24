using Cinemachine;
using DG.Tweening;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDropshipCut : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] GameObject camObj;
    [SerializeField] CinemachineVirtualCamera cam;
    [SerializeField] CinemachineSmoothPath track;
    [SerializeField] AnimationClip clip;

    [Header("Movement Settings")]
    [SerializeField] Transform pos1;
    [SerializeField] Transform pos2;
    [SerializeField] float moveSpeed = 100f;

    [Header("Dropship Settings")]
    [SerializeField] GameObject dropship;
    [SerializeField] GameObject hatch;
    [SerializeField] float hatchSpeed = 2f;

    [SerializeField] GameObject restBtn;

    bool isWaiting = true;
    bool isArrive = false;

    private void Start()
    {
        AudioManager.instance.PlayBgm(EBgm.GAMESTART);
        Init();
        StartCoroutine(StartMove());
    }

    private void Init()
    {
        CinemachineTrackedDolly dolly = cam.GetCinemachineComponent<CinemachineTrackedDolly>();

        dolly.m_Path = track;
        dolly.m_PathPosition = 0f;

        GameManager.instance.enabled = false;
        GameManager.instance.Timer.enabled = false;
        FieldManager.instance.enabled = false;
        EnemyManager.instance.enabled = false;
        restBtn.SetActive(false);
        GameUI.instance.SwitchNone();
        CameraManager.instance.enabled = false;
        hatch.transform.localRotation = Quaternion.identity;

        CameraManager.instance.CutScene.PlayClip(clip);
        cam.enabled = true;
    }

    public void WaitingSpawn()
    {
        isWaiting = false;
    }

    IEnumerator StartMove()
    {
        yield return StartCoroutine(StartMoveToTarget(pos1));

        moveSpeed = 40f;

        yield return StartCoroutine(StartMoveToTarget(pos2));

        isArrive = true;

        yield return new WaitForSeconds(2);

        StartCoroutine(StartOpenHatch());

        PhotonManager.instance.PhotonMaching.ArriveDropship();
    }

    IEnumerator StartMoveToTarget(Transform _target)
    {
        while (Vector3.Distance(dropship.transform.position, _target.position) > 0.1f)
        {
            Vector3 dir = (_target.position - dropship.transform.position).normalized;

            float distance = Vector3.Distance(dropship.transform.position, _target.position);
            float speed = Mathf.Lerp(0, moveSpeed, distance / 30f);

            dropship.transform.position = Vector3.MoveTowards
                (dropship.transform.position, _target.position, speed * Time.deltaTime);

            yield return null;
        }

        if (Vector3.Distance(dropship.transform.position, pos2.position) <= 0.1f && !isArrive)
        {
            yield break;
        }
    }

    IEnumerator StartOpenHatch()
    {
        while (isWaiting) 
        {
            yield return null;
        }

        Quaternion targetRot = Quaternion.Euler(-45, 0, 0);

        hatch.transform.DOLocalRotate(targetRot.eulerAngles, hatchSpeed).SetEase(Ease.InOutSine);

        yield return new WaitForSeconds(hatchSpeed);

        hatch.transform.localRotation = targetRot;

        StartCoroutine(StartChangePlayer());
    }

    IEnumerator StartChangePlayer()
    {
        yield return new WaitForSeconds(1);

        GameManager.instance.enabled = true;
        GameManager.instance.Timer.enabled = true;
        FieldManager.instance.enabled = true;
        EnemyManager.instance.enabled = true;
        CameraManager.instance.enabled = true;

        cam.enabled = false;

        yield return new WaitForSeconds(1f);

        this.enabled = false;
        GameManager.instance.ViewState.SetViewState(EViewState.FPS);
        restBtn.SetActive(true);
        camObj.SetActive(false);
    }
}

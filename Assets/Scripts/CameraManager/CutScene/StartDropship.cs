using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDropship : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] CinemachineVirtualCamera cam;
    [SerializeField] CinemachineSmoothPath track;
    [SerializeField] AnimationClip clip;
    [SerializeField] GameObject camMng;

    [Header("Movement Settings")]
    [SerializeField] Transform pos1;
    [SerializeField] Transform pos2;
    [SerializeField] float moveSpeed = 100f;

    [Header("Dropship Settings")]
    [SerializeField] GameObject dropship;
    [SerializeField] GameObject hatch;
    [SerializeField] float hatchSpeed = 2f;

    [Header("Player Settings")]
    [SerializeField] GameObject player;
    [SerializeField] Transform playerStartPos;
    PlayerMovement playerMovement;
    PlayerCombat playerCombat;

    [Header("Managers")]
    [SerializeField] GameObject gameManager;
    [SerializeField] GameObject enemyManager;
    [SerializeField] GameObject fieldManager;

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
        CinemachineTrackedDolly dolly = cam.GetCinemachineComponent<CinemachineTrackedDolly>();
        CinemachineBasicMultiChannelPerlin noise = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        dolly.m_Path = track;
        dolly.m_PathPosition = 0f;

        noise.m_AmplitudeGain = 0;

        gameManager.SetActive(false);
        fieldManager.SetActive(false);
        enemyManager.SetActive(false);
        playerMovement.enabled = false;
        playerCombat.enabled = false;
        player.transform.SetParent(transform);
        player.transform.position = playerStartPos.position;
        player.transform.rotation = Quaternion.Euler(0, 45, 0);
        hatch.transform.localRotation = Quaternion.identity;
        camMng.SetActive(false);
        Shared.gameManager.ViewState.enabled = false;

        Shared.cameraManager.CutScene.PlayClip(clip);
        cam.enabled = true;
    }

    IEnumerator StartMove()
    {
        yield return StartCoroutine(StartMoveToTarget(pos1));

        moveSpeed = 40f;

        yield return StartCoroutine(StartMoveToTarget(pos2));
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
            player.transform.position = playerStartPos.position;

            yield return null;
        }

        if (Vector3.Distance(dropship.transform.position, pos2.position) <= 0.1f)
        {
            yield return new WaitForSeconds(2);

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

        StartCoroutine(StartChangePlayer());
    }

    IEnumerator StartChangePlayer()
    {
        yield return new WaitForSeconds(1);

        AudioManager.instance.PlayBgm(EBgm.DESERT);
        camMng.SetActive(true);
        gameManager.SetActive(true);
        fieldManager.SetActive(true);
        enemyManager.SetActive(true);

        cam.enabled = false;

        yield return new WaitForSeconds(1f);

        this.enabled = false;
        Shared.gameManager.ViewState.enabled = true;
        playerMovement.enabled = true;
        playerCombat.enabled = true;
    }
}

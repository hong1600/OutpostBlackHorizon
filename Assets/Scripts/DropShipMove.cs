using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DropShipMove : MonoBehaviour
{
    Camera mainCam;
    [SerializeField] Cinemachine.CinemachineVirtualCamera virtualCam;

    [SerializeField] Transform pos1;
    [SerializeField] Transform pos2;
    [SerializeField] float moveSpeed = 10f;

    [SerializeField] GameObject hatch;
    [SerializeField] float hatchSpeed = 2f;

    [SerializeField] GameObject player;
    [SerializeField] Transform playerEye;
    Vector3 playerOffset;

    [SerializeField] Transform hatchCamTrs;
    [SerializeField] Transform playerCamTrs;

    [SerializeField] GameObject HUDCanvas;

    private void Start()
    {
        mainCam = Camera.main;
        playerOffset = player.transform.localPosition;
        StartCoroutine(StartMove());
    }

    IEnumerator StartMove()
    {
        yield return StartCoroutine(StartMovePos(pos1));

        yield return StartCoroutine(StartMovePos(pos2));
    }

    IEnumerator StartMovePos(Transform _target)
    {
        while (Vector3.Distance(transform.position, _target.position) > 0.1f)
        {
            Vector3 dir = (_target.position - transform.position).normalized;

            float distance = Vector3.Distance(transform.position, _target.position);
            float speed = Mathf.Lerp(0, moveSpeed, distance / 30f);

            transform.position = Vector3.MoveTowards
                (transform.position, _target.position, speed * Time.deltaTime);
            player.transform.localPosition = playerOffset;

            yield return null;
        }

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
        virtualCam.enabled = false;
        CinemachineBrain brain = mainCam.GetComponent<CinemachineBrain>();
        brain.enabled = false;
        CameraMng camMng = mainCam.GetComponent<CameraMng>();
        camMng.enabled = true;

        yield return new WaitForSeconds(1.5f);

        HUDCanvas.SetActive(true);
    }
}

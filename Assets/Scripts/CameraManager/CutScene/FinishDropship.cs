using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishDropship : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] CinemachineVirtualCamera cam;
    [SerializeField] CinemachineSmoothPath track;
    [SerializeField] AnimationClip clip;
    [SerializeField] GameObject camMng;

    [Header("Component")]
    BoxCollider box;
    PlayerMovement player;

    [Header("Dropship Setting")]
    [SerializeField] GameObject dropShip;
    [SerializeField] GameObject hatch;
    [SerializeField] GameObject finishBox;
    [SerializeField] float hatchSpeed = 2f;

    [Header("Player Setting")]
    [SerializeField] CameraManager camManager;
    [SerializeField] GameObject playerObj;
    [SerializeField] GameObject rifle;

    [Header("Movement Setting")]
    [SerializeField] List<Transform> movePosList;
    Vector3 dropShipUpOffset = new Vector3(-15, 20, 30);
    Vector3 playerPos;

    private void Awake()
    {
        player = playerObj.GetComponent<PlayerMovement>();
        box = finishBox.GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            CinemachineTrackedDolly dolly = cam.GetComponent<CinemachineTrackedDolly>();

            dolly.m_Path = track;
            dolly.m_PathPosition = 0f;

            camManager.enabled = false;
            cam.enabled = true;
            rifle.transform.SetParent(playerObj.transform);
            playerObj.transform.SetParent(transform);
            player.enabled = false;
            playerPos = playerObj.transform.localPosition;

            StartCoroutine(StartFarPlayer());
        }
    }

    IEnumerator StartFarPlayer()
    {
        CameraManager.instance.CutScene.PlayClip(clip);

        yield return new WaitForSeconds(3f);

        StartCoroutine(StartHatchClose());
    }

    IEnumerator StartHatchClose()
    {
        Quaternion targetRot = Quaternion.Euler(0, 0, 0);

        hatch.transform.DOLocalRotate(targetRot.eulerAngles, hatchSpeed).SetEase(Ease.InOutSine);

        yield return new WaitForSeconds(hatchSpeed);

        hatch.transform.localRotation = targetRot;

        yield return new WaitForSeconds(1);

        yield return StartMovePos();
    }


    IEnumerator StartMovePos()
    {
        float targetRotY = Quaternion.LookRotation(movePosList[1].transform.position).eulerAngles.y;
        Quaternion targetRot = Quaternion.Euler(0, targetRotY, 0);

        dropShip.transform.DOMove(movePosList[0].position, 2).SetEase(Ease.InOutSine);
        dropShip.transform.DORotate(targetRot.eulerAngles, 2).SetEase(Ease.InOutSine);

        yield return new WaitForSeconds(2);

        dropShip.transform.DOMove(movePosList[1].position, 10).SetEase(Ease.InOutSine);

        yield return new WaitForSeconds(3);

        MSceneManager.Instance.ChangeScene(EScene.WAITING, true);
    }
}

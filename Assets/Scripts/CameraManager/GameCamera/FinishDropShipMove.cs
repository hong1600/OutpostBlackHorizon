using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishDropShipMove : MonoBehaviour
{
    Camera mainCam;
    BoxCollider box;
    PlayerMovement player;

    [SerializeField] Cinemachine.CinemachineVirtualCamera virtualCam;
    Cinemachine.CinemachineTransposer virualTransposer;

    [SerializeField] CameraManager camManager;
    [SerializeField] GameObject dropShip;
    [SerializeField] GameObject HUDCanvas;
    [SerializeField] GameObject hatch;
    [SerializeField] GameObject playerObj;
    [SerializeField] GameObject rifle;
    [SerializeField] GameObject finishBox;
    [SerializeField] Transform hatchCamTrs;

    [SerializeField] List<Transform> movePosList;
    [SerializeField] float hatchSpeed = 2f;

    Vector3 dropShipUpOffset = new Vector3(-15, 20, 30);
    Vector3 playerPos;

    private void Awake()
    {
        mainCam = Camera.main;
        player = playerObj.GetComponent<PlayerMovement>();
        box = finishBox.GetComponent<BoxCollider>();
    }

    private void Start()
    {
        virtualCam.Follow = null;
        virtualCam.LookAt = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            camManager.enabled = false;
            virtualCam.transform.position = mainCam.transform.position;
            virtualCam.transform.rotation = mainCam.transform.rotation;
            virtualCam.enabled = true;
            virualTransposer = virtualCam.GetCinemachineComponent<CinemachineTransposer>();
            mainCam.transform.SetParent(null);
            rifle.transform.SetParent(playerObj.transform);
            HUDCanvas.SetActive(false);
            playerObj.transform.SetParent(transform);
            player.enabled = false;
            playerPos = playerObj.transform.localPosition;

            StartCoroutine(StartFarPlayer());
        }
    }

    IEnumerator StartFarPlayer()
    {
        virtualCam.transform.DOMove(hatchCamTrs.position, 3f).SetEase(Ease.InOutSine);
        virtualCam.transform.DORotateQuaternion(hatchCamTrs.rotation, 3f).SetEase(Ease.InOutSine);

        yield return new WaitForSeconds(3f);

        StartCoroutine(StartHatchClose());
    }

    IEnumerator StartHatchClose()
    {
        Quaternion targetRot = Quaternion.Euler(0, 0, 0);

        hatch.transform.DOLocalRotate(targetRot.eulerAngles, hatchSpeed).SetEase(Ease.InOutSine);

        yield return new WaitForSeconds(hatchSpeed);

        hatch.transform.localRotation = targetRot;
        virtualCam.Follow = dropShip.transform;
        virtualCam.LookAt = dropShip.transform;
        virualTransposer.m_FollowOffset = dropShipUpOffset;

        yield return new WaitForSeconds(1);

        yield return StartMovePos();
    }


    IEnumerator StartMovePos()
    {
        float targetRotY = Quaternion.LookRotation(movePosList[1].transform.position).eulerAngles.y;
        Quaternion targetRot = Quaternion.Euler(0, targetRotY, 0);

        transform.DOMove(movePosList[0].position, 2).SetEase(Ease.InOutSine);
        transform.DORotate(targetRot.eulerAngles, 2).SetEase(Ease.InOutSine);

        yield return new WaitForSeconds(2);

        transform.DOMove(movePosList[1].position, 10).SetEase(Ease.InOutSine);

        yield return new WaitForSeconds(3);

        MSceneManager.Instance.ChangeScene(EScene.WAITING, true);
    }
}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFpsDead : MonoBehaviour
{
    Camera mainCam;

    [SerializeField] GameObject player;
    [SerializeField] float backOffset;
    [SerializeField] float moveTime;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    public void MoveCam()
    {
        Vector3 backDir = -player.transform.forward.normalized;

        Vector3 targetPos = player.transform.position + backDir * backOffset;

        targetPos.y = mainCam.transform.position.y;

        mainCam.transform.DOMove(targetPos, moveTime).SetEase(Ease.Linear);
    }
}

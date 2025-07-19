using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMiniMap : MonoBehaviour
{
    Camera mainCam;
    Transform playerTrs;

    [SerializeField] Camera minimapCam;
    [SerializeField] RectTransform miniMapRect;
    [SerializeField] RectTransform iconContainer;
    [SerializeField] RectTransform playerIcon;

    private void Start()
    {
        GameManager.instance.PlayerSpawner.onSpawnPlayer += InitPlayer;

        mainCam = Camera.main;
    }

    private void InitPlayer()
    {
        playerTrs = GameManager.instance.PlayerSpawner.player.transform;
    }

    private void Update()
    {
        if (mainCam != null && playerTrs != null)
        {
            MoveCam();
            UpdatePlayer();
        }
    }

    private void MoveCam()
    {
        transform.position = new Vector3(mainCam.transform.position.x, 30, mainCam.transform.position.z);
    }

    private void UpdatePlayer()
    {
        Vector3 screenPos = minimapCam.WorldToScreenPoint(playerTrs.position);

        if (screenPos.z <= 0)
        {
            playerIcon.gameObject.SetActive(false);
            return;
        }

        playerIcon.gameObject.SetActive(true);

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            iconContainer,
            screenPos,
            minimapCam,
            out localPoint
        );

        playerIcon.anchoredPosition = localPoint;
    }
}

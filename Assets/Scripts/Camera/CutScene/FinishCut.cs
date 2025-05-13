using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class FinishCut : MonoBehaviour, ICutScene
{
    [SerializeField] GameObject camObj;
    CinemachineVirtualCamera virtualCam;

    private void Awake()
    {
        virtualCam = camObj.GetComponent<CinemachineVirtualCamera>();
    }

    public void Play()
    {
        virtualCam.m_LookAt = EnemyManager.instance.BossSpawner.bossObj.transform;
        camObj.SetActive(true);
        GameManager.instance.ViewState.SetViewState(EViewState.NONE);
        StartCoroutine(GameUI.instance.StartBlackout(1));

        StartCoroutine(StartCutScene());
    }

    IEnumerator StartCutScene()
    {
        yield return new WaitForSeconds(8);

        GameManager.instance.GameState.SetGameState(EGameState.GAMECLEAR);
    }
}

using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using DG.Tweening;

public class BossCut : MonoBehaviour, ICutScene
{
    BossSpawner bossSpawner;
    ViewState viewState;

    [SerializeField] GameObject camObj;
    CinemachineVirtualCamera virtualCam;

    [SerializeField] GameObject boss;

    float timer = 0f;
    [SerializeField] float swaySpeed;
    [SerializeField] float swayAmount;

    [SerializeField] float zoomSpeed;
    [SerializeField] float zoomDuration = 2;
    [SerializeField] float endFOV = 40;

    private void Awake()
    {
        virtualCam = camObj.GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        bossSpawner = EnemyManager.instance.BossSpawner;
        viewState = GameManager.instance.ViewState;
    }

    public void Play()
    {
        bossSpawner = EnemyManager.instance.BossSpawner;
        camObj.SetActive(true);
        GameManager.instance.ViewState.SwitchNone();

        StartCoroutine(StartCutScene());
    }

    IEnumerator StartCutScene()
    {
        Quaternion originRot = camObj.transform.rotation;
        float startFOV = virtualCam.m_Lens.FieldOfView;

        yield return StartCoroutine(GameUI.instance.StartBlackout(1));

        yield return new WaitForSeconds(0.5f);

        while (timer <= 2)
        {
            timer += Time.deltaTime;
            swaySpeed += Time.deltaTime;

            float offset = Mathf.Sin(timer * swaySpeed) * swayAmount;

            camObj.transform.rotation = originRot * Quaternion.Euler(0f, offset, 0f);

            yield return null;
        }

        AudioManager.instance.PlayBgm(EBgm.BOSSROUND);

        timer = 0f;

        GameObject boss = bossSpawner.SpawnBoss();
        Transform bossTrs = boss.transform;

        yield return null;

        Quaternion targetRot = Quaternion.LookRotation((bossTrs.position - camObj.transform.position).normalized);
        Quaternion targetRotOffset = Quaternion.Euler(targetRot.x - 18, targetRot.y, targetRot.z);
        
        while (timer <= zoomDuration)
        {
            timer += Time.deltaTime;
        
            float t = Mathf.Clamp01(timer / zoomDuration) * zoomSpeed;
            float fov = Mathf.Lerp(startFOV, endFOV, t);
            camObj.transform.rotation = Quaternion.Lerp(camObj.transform.rotation, targetRotOffset, t);
            virtualCam.m_Lens.FieldOfView = fov;
        
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        StartCoroutine(GameUI.instance.StartBlackout(1));
        virtualCam.m_Lens.FieldOfView = 60f;

        yield return null;

        camObj.SetActive(false);
        viewState.SetViewState(EViewState.FPS);
    }
}

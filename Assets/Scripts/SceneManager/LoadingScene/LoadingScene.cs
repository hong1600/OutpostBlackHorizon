using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    GameSceneResource gameSceneResource;
    GameModeManager gameModeManager;

    static EScene nextScene;
    [SerializeField] Image sliderValue;

    float sceneProgress = 0f;
    float resourceProgress = 0f;

    static bool isReadyToGame = false;

    private void Start()
    {
        gameSceneResource = ResourceManager.instance.GameSceneResource;
        gameModeManager = GameModeManager.instance;

        StartCoroutine(StartLoadScene());
    }

    public static void AllowSceneActivation()
    {
        isReadyToGame = true;
    }

    public static void SetNextScene(EScene _eScene)
    {
        nextScene = _eScene;
    }

    public static void LoadScene(EScene _eScene)
    {
        SceneManager.LoadScene("Loading");

        nextScene = _eScene;
    }

    IEnumerator StartLoadScene()
    {
        yield return null;

        yield return StartCoroutine(LoadResource());

        AsyncOperation sceneOp = SceneManager.LoadSceneAsync((int)nextScene);
        sceneOp.allowSceneActivation = false;

        while (sceneOp.progress < 0.9f)
        {
            sceneProgress = Mathf.Clamp01(sceneOp.progress / 0.9f);
            float totalProgress = (sceneProgress + resourceProgress) / 2f;
            sliderValue.fillAmount = Mathf.MoveTowards(sliderValue.fillAmount, totalProgress, Time.deltaTime * 2f);
            yield return null;
        }

        if(gameModeManager.eGameMode == EGameMode.MULTI) 
        {
            PhotonManager.instance.PhotonMaching.NotifySceneLoaded();
        }

        while (sliderValue.fillAmount <= 0.999f)
        {
            sliderValue.fillAmount = Mathf.MoveTowards(sliderValue.fillAmount, 1f, Time.deltaTime * 2f);
            yield return null;
        }

        while(!isReadyToGame && gameModeManager.eGameMode == EGameMode.MULTI)
            yield return null;

        sceneOp.allowSceneActivation = true;
    }

    IEnumerator LoadResource()
    {
        if (nextScene == EScene.SINGLEGAME || nextScene == EScene.MULTIGAME)
        {
            EEnemy[] gameResources = gameSceneResource.EnemyResource.GetTypeEnums();

            int total = gameResources.Length;
            int loaded = 0;

            for (int i = 0; i < total; i++)
            {
                string address = gameSceneResource.EnemyResource.ConvertEnumToAddress(gameResources[i]);

                var handle = Addressables.LoadAssetAsync<GameObject>(address);

                yield return handle;

                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    gameSceneResource.EnemyResource.addPrefabDic(gameResources[i], handle.Result);
                }

                loaded++;
                resourceProgress = (float)loaded / total;
            }
        }

        resourceProgress = 1f;
    }
}

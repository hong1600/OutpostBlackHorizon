using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MSceneManager : MonoBehaviour
{
    public static MSceneManager Instance;

    EScene scene = EScene.LOGIN;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        LoadSceneResources(EScene.GAME);
    }

    public void ChangeScene(EScene _eScene, bool _loading = false)
    {
        if (scene == _eScene)
            return;

        scene = _eScene;

        if(_loading) 
        {
            LoadingScene.LoadScene(_eScene);
        }
        else
        {
            LoadSceneResources(_eScene);

            switch (_eScene)
            {
                case EScene.LOGIN:
                    SceneManager.LoadScene("Login");
                    break;
                case EScene.LOBBY:
                    SceneManager.LoadScene("Lobby");
                    break;
                case EScene.WAITING:
                    SceneManager.LoadScene("Waiting");
                    break;
                case EScene.MACHING:
                    SceneManager.LoadScene("Maching");
                    break;
                case EScene.LOADING:
                    SceneManager.LoadScene("Loading");
                    break;
                case EScene.GAME:
                    SceneManager.LoadScene("Game");
                    break;
                case EScene.END:
                    SceneManager.LoadScene("End");
                    break;
            }
        }
    }

    private void LoadSceneResources(EScene _eScene)
    {
        ResourceManager.instance.GameSceneResource.Unload();

        switch(_eScene) 
        {
            case EScene.LOGIN:
                break;
            case EScene.LOBBY:
                break;
            case EScene.WAITING:
                break;
            case EScene.GAME:
                ResourceManager.instance.GameSceneResource.Load();
                break;
            default:
                return;
        }
    }

    private EScene GetSceneName(string sceneName)
    {
        switch (sceneName)
        {
            case "Login":
                return EScene.LOGIN;
            case "Lobby":
                return EScene.LOBBY;
            case "Waiting":
                return EScene.WAITING;
            case "Game":
                return EScene.GAME;
            default:
                return 0;
        }
    }
}

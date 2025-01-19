using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMng : MonoBehaviour
{
    EScene scene = EScene.LOGIN;

    private void Awake()
    {
        Shared.sceneMng = this;
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
            switch (_eScene)
            {
                case EScene.LOGIN:
                    SceneManager.LoadScene("Login");
                    break;
                case EScene.LOBBY:
                    SceneManager.LoadScene("Lobby");
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
}

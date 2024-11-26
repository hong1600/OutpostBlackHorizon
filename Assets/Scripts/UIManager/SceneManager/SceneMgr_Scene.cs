using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class SceneMgr : MonoBehaviour
{
    public static SceneMgr Instance;

    eSCENE Scene = eSCENE.eSCENE_LOGIN;

    public string curNameText;

    private void Awake()
    {
        if(Instance == null) 
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void ChangeScene(eSCENE _e, bool _Loading = false)
    {
        if (Scene == _e)
            return;

        Scene = _e;

        switch(_e)
        {
            case eSCENE.eSCENE_LOGIN:
                SceneManager.LoadScene("Login");
                break;
            case eSCENE.eSCENE_LOBBY:
                SceneManager.LoadScene("Lobby");
                break;
            case eSCENE.eSCENE_LOADING:
                SceneManager.LoadScene("Loading");
                break;
            case eSCENE.eSCENE_GAME:
                SceneManager.LoadScene("Game");
                break;
            case eSCENE.eSCENE_END:
                SceneManager.LoadScene("End");
                break;
        }
    }
}

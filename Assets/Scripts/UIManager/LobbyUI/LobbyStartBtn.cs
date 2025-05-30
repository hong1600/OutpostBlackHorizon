using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyStartBtn : MonoBehaviour
{
    public void ClickSingleBtn()
    {
        MSceneManager.Instance.ChangeScene(EScene.WAITING);
    }

    public void ClickMachingBtn()
    {
        MSceneManager.Instance.ChangeScene(EScene.MACHING);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyStartBtn : MonoBehaviour
{
    public void ClickGameStartBtn()
    {
        MSceneManager.Instance.ChangeScene(EScene.WAITING);
    }
}

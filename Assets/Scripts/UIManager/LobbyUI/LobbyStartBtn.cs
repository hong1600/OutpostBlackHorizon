using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyStartBtn : MonoBehaviour
{
    public void ClickGameStartBtn()
    {
        SceneMng.Instance.ChangeScene(EScene.WAITING, true);
    }
}

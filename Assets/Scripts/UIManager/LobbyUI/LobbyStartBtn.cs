using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyStartBtn : MonoBehaviour
{
    [SerializeField] GameObject grapicPanel;

    public void ClickGameStartBtn()
    {
        SceneMng.Instance.ChangeScene(EScene.WAITING);
    }
}

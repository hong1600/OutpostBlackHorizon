using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingStartBtn : MonoBehaviour
{
    public void OnClickStartBtn()
    {
        AudioManager.instance.StopBgm();
        MSceneManager.Instance.ChangeScene(EScene.GAME, true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingStartBtn : MonoBehaviour
{
    public void OnClickStartBtn()
    {
        MSceneManager.Instance.ChangeScene(EScene.GAME, true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingStartBtn : MonoBehaviour
{
    public void OnClickStartBtn()
    {
        SceneMng.Instance.ChangeScene(EScene.GAME, true);
    }
}

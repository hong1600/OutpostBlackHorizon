using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartBtn : MonoBehaviour
{
    public void ClickGameStartBtn()
    {
        Shared.sceneMng.ChangeScene(EScene.GAME, true);
    }
}

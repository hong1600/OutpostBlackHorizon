using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round : MonoBehaviour
{
    public event Action onRoundEvent;

    public bool isBossRound;
    public int curRound;

    private void Awake()
    {
        isBossRound = false;
        curRound = 2;
    }

    public void RoundCheck()
    {
        if(curRound == 1) 
        {
            CameraManager.instance.CutScene.PlayCutScene(ECutScene.ENEMYDROPSHIP);
        }

        if (curRound == 3)
        {
            isBossRound = true;
            CameraManager.instance.CutScene.PlayCutScene(ECutScene.BOSS);
        }
        else
        {
            isBossRound = false;
        }

        onRoundEvent?.Invoke();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round : MonoBehaviour
{
    public event Action onRoundEvent;
    public event Action onBossRound;

    public bool isBossRound;
    public int curRound;

    private void Awake()
    {
        isBossRound = false;
        curRound = 1;
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
            onBossRound?.Invoke();
        }
        else
        {
            isBossRound = false;
        }

        onRoundEvent?.Invoke();
    }
}

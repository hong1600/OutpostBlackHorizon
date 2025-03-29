using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round : MonoBehaviour
{
    public event Action onRoundEvent;
    public event Action onBossRound;

    CutScene cutScene;

    public bool isBossRound;
    public int curRound;

    private void Awake()
    {
        isBossRound = false;
        curRound = 0;
    }

    private void Start()
    {
        cutScene = Shared.cameraManager.CutScene;
    }

    public void RoundCheck()
    {
        if(curRound == 1) 
        {
            Shared.cameraManager.CutScene.PlayCutScene(ECutScene.ENEMYDROPSHIP);
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

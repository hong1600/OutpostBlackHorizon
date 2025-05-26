using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round : MonoBehaviour
{
    public event Action onRoundEvent;

    [SerializeField] bool isBossRound;
    [SerializeField] int curRound;

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
            CameraManager.instance.CutScene.PlayCutScene(ECutScene.BOSS);
        }
        else
        {
            isBossRound = false;
        }

        onRoundEvent?.Invoke();
    }

    public int CurRound { get { return curRound; } }
    public void increaseRound(int _amount)
    {
        curRound += _amount;
    }
    public bool IsBossRound { get { return isBossRound;} }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRound 
{
    bool GetIsBossRound();
    void SetBossRound(bool _value);
    int GetCurRound();
    void SetCurRound(int _value);
    void RoundCheck();
}

public class Round : MonoBehaviour, IRound
{
    public bool isBossRound;
    public int curRound;

    private void Awake()
    {
        isBossRound = false;
        curRound = 0;
    }

    public void RoundCheck()
    {
        if (curRound == 10)
        {
            isBossRound = true;
        }
        else
        {
            isBossRound = false;
        }
    }

    public bool GetIsBossRound() { return isBossRound; }
    public void SetBossRound(bool value) { isBossRound = value;}
    public int GetCurRound() {  return curRound; }
    public void SetCurRound(int _value) { curRound += _value; }
}

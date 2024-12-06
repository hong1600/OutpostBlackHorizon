using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRound 
{
    bool isBossRound();
    void setBossRound(bool value);
    int getCurRound();
    void setCurRound(int value);
    void roundCheck();
}

public class Round : MonoBehaviour, IRound
{
    public bool bossRound;
    public int curRound;

    private void Awake()
    {
        bossRound = false;
        curRound = 0;
    }

    public void roundCheck()
    {
        if (curRound == 10)
        {
            bossRound = true;
        }
        else
        {
            bossRound = false;
        }
    }

    public bool isBossRound() { return bossRound; }
    public void setBossRound(bool value) { bossRound = value;}
    public int getCurRound() {  return curRound; }
    public void setCurRound(int value) { curRound += value; }
}

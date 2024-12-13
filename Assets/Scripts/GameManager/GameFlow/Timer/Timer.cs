using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITimer
{
    void timer();
    float getMin();
    float getSec();
    void setSec(float value);
}

public interface ISpawnTime 
{
    bool isSpawnTime();
    void setSpawnTime(bool value);

}

public class Timer : MonoBehaviour, ITimer, ISpawnTime
{
    public Round round;
    public IRound iRound;
    public UIRoundPanel uiRoundPanel;
    public IUIRoundPanel iUIRoundPanel;
    public UISpawnPointTimerPanel uiSpawnPointTimerPanel;
    public IUISpawnPointTimerPanel iUISpawnPointTimerPanel;

    public bool timerRunning;
    public float min, sec;
    public bool spawnTime;

    private void Awake()
    {
        iRound = round;
        iUIRoundPanel = uiRoundPanel;
        iUISpawnPointTimerPanel = uiSpawnPointTimerPanel;

        spawnTime = false;
        min = 0.0f;
        sec = 4f;
    }

    public void timer()
    {
        if (iRound.isBossRound())
        {
            sec = 0f;
        }
        else
        {
            sec -= Time.deltaTime;

            if(sec < 4) 
            {
                int intSec = (int)sec;

                iUISpawnPointTimerPanel.getSpawnPointTimerPanel().SetActive(true);
                iUISpawnPointTimerPanel.getSpawnPointTimerText().text = intSec.ToString();
            }
        }

        int intsec = (int)sec;

        if (sec < 0f)
        {
            StartCoroutine(spawn());
            uiRoundPanel.roundPanel();

            if (min > 0)
            {
                min -= 1;
            }
        }
    }

    IEnumerator spawn()
    {
        iRound.setCurRound(1);
        iRound.roundCheck();
        sec = 20f;
        spawnTime = true;
        iUISpawnPointTimerPanel.getSpawnPointTimerPanel().SetActive(false);

        yield return new WaitForSeconds(17);

        spawnTime = false;
    }


    public float getSec() { return sec; }
    public void setSec(float value) { sec = value; }
    public bool isSpawnTime() { return spawnTime; }
    public void setSpawnTime(bool value) { spawnTime = value; }
    public float getMin() { return min; }
}

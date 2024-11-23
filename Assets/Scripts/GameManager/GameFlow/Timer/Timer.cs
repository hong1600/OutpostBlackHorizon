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

    public bool timerRunning;
    public float min, sec;
    public bool spawnTime;

    private void Awake()
    {
        iRound = round;
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
        }
        int intsec = (int)sec;

        if (sec < 0f)
        {
            StartCoroutine(spawn());

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
        //gameMainUI.spawnPointTimerPanel.SetActive(false);

        yield return new WaitForSeconds(17);

        spawnTime = false;
    }


    public float getSec() { return sec; }
    public void setSec(float value) { sec = value; }
    public bool isSpawnTime() { return spawnTime; }
    public void setSpawnTime(bool value) { spawnTime = value; }
    public float getMin() { return min; }
}

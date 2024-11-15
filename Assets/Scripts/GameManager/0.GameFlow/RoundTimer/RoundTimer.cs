using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundTimer : MonoBehaviour
{
    public GameFlow gameFlow;
    public GameMainUI gameMainUI;
    public GameStateCheck gameStateCheck;

    public bool bossRound;
    public float min, sec;
    public bool spawnTime;
    public int curRound;
    public bool timerRunning;

    private void Awake()
    {
        bossRound = false;
        spawnTime = false;
        curRound = 0;
    }

    private void Update()
    {
        if (timerRunning)
        {
            timer();
        }
    }

    public void timer()
    {
        if (bossRound)
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
        curRound++;
        roundCheck();
        sec = 20f;
        spawnTime = true;
        gameMainUI.spawnPointTimerPanel.SetActive(false);

        yield return new WaitForSeconds(17);

        spawnTime = false;
    }

    public void roundCheck()
    {
        if (curRound == 5 || curRound == 10)
        {
            bossRound = true;
        }
        else
        {
            bossRound = false;
        }
    }
}

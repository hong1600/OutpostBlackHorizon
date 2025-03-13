using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnTimer : MonoBehaviour
{
    public event Action onTimeEvent;

    float sec;
    float maxSec;
    bool spawnTime;

    private void Start()
    {
        spawnTime = false;
        sec = 4f;
        maxSec = sec;

        StartCoroutine(StartTimerLoop());
    }

    IEnumerator StartTimerLoop()
    {
        while (Shared.gameManager.GameState.GetGameState() == EGameState.PLAYING)
        {
            Timer();
            yield return null;
        }
    }

    private void Timer()
    {
        if (Shared.gameManager.Round.GetIsBossRound())
        {
            sec = 0f;
        }
        else
        {
            sec -= Time.deltaTime;
            sec = Mathf.Max(0f, sec);
            onTimeEvent?.Invoke();

            if (sec < 4)
            {
                int intSec = (int)sec;
            }
        }

        int intsec = (int)sec;

        if (sec <= 0f)
        {
            StartCoroutine(StartSpawnTime());
        }
    }

    IEnumerator StartSpawnTime()
    {
        spawnTime = true;

        Shared.gameManager.Round.SetCurRound(1);
        Shared.gameManager.Round.RoundCheck();
        sec = 20f;
        maxSec = sec;

        yield return new WaitForSeconds(17);

        spawnTime = false;
    }

    public float GetMaxSec() { return maxSec; }
    public float GetSec() { return sec; }
    public void SetSec(float _value) { sec = _value; }
    public bool GetIsSpawnTime() { return spawnTime; }
    public void SetIsSpawnTime(bool _value) { spawnTime = _value; }
}

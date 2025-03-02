using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface ISpawnTimer
{
    void SubTimerEvent(Action _listener);
    void UnTimerEvent(Action _listener);
    float GetMaxSec();
    float GetSec();
    void SetSec(float _value);
    bool GetIsSpawnTime();
    void SetIsSpawnTime(bool _value);

}

public class SpawnTimer : MonoBehaviour, ISpawnTimer
{
    event Action onTimeEvent;

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
        while (Shared.gameMng.GameState.GetGameState() == EGameState.PLAYING)
        {
            Timer();
            yield return null;
        }
    }

    private void Timer()
    {
        if (Shared.gameMng.Round.GetIsBossRound())
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

        Shared.gameMng.Round.SetCurRound(1);
        Shared.gameMng.Round.RoundCheck();
        sec = 20f;
        maxSec = sec;

        yield return new WaitForSeconds(17);

        spawnTime = false;
    }

    public void SubTimerEvent(Action _listener) { onTimeEvent += _listener; }
    public void UnTimerEvent(Action _listener) { onTimeEvent -= _listener; }
    public float GetMaxSec() { return maxSec; }
    public float GetSec() { return sec; }
    public void SetSec(float _value) { sec = _value; }
    public bool GetIsSpawnTime() { return spawnTime; }
    public void SetIsSpawnTime(bool _value) { spawnTime = _value; }
}

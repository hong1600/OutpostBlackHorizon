using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timer : MonoBehaviour
{
    public event Action onTimeEvent;
    public event Action onRestTime;

    protected EnemyManager enemyManager;
    protected GameState gameState;
    protected Round round;
    protected EnemySpawner enemySpawner;
    WaveBossSpawner waveBossSpawner;

    public float sec;
    public float maxSec { get; private set; }
    protected bool isTimerRunning = false;
    public bool isSpawnTime { get; private set; } = false;

    [SerializeField] float spawnTime;
    [SerializeField] float restTime;

    protected virtual void Start()
    {
        enemyManager = EnemyManager.instance;
        gameState = GameManager.instance.GameState;
        round = GameManager.instance.Round;
        enemySpawner = EnemyManager.instance.EnemySpawner;
        waveBossSpawner = EnemyManager.instance.WaveBossSpawner;

        sec = 28f;
        maxSec = sec;

        isTimerRunning = true;
        StartCoroutine(StartTimerLoop());
    }

    protected virtual IEnumerator StartTimerLoop()
    {
        while (gameState.GetGameState() == EGameState.PLAYING)
        {
            if(isTimerRunning) 
            {
                RunTimer();
            }
            yield return null;
        }
    }

    protected virtual void RunTimer()
    {
        if (round.IsBossRound)
        {
            isTimerRunning = false;
        }
        else
        {
            sec -= Time.deltaTime;
            sec = Mathf.Max(0f, sec);
            onTimeEvent?.Invoke();
        }

        if (sec <= 0f)
        {
            enemySpawner.IsSpawn = false;

            if (!isSpawnTime) 
            {
                if (round.CurRound == 0)
                {
                    isTimerRunning = false;
                    ChangeSpawnTime();
                }
                else
                {
                    ChangeSpawnTime();
                }
            }
            else if(isSpawnTime && enemyManager.GetCurEnemy() <= 0)
            {
                ChangeRestTime();
            }
        }
    }

    protected void OnTimeEvent()
    {
        onTimeEvent?.Invoke();
    }

    protected void ChangeSpawnTime()
    {
        isSpawnTime = !isSpawnTime;
        sec = spawnTime;
        maxSec = spawnTime;
        if (round.CurRound != 0)
        {
            enemySpawner.IsSpawn = true;
        }
        round.increaseRound(1);
        round.RoundCheck();
        onRestTime?.Invoke();
    }

    protected void ChangeRestTime()
    {
        isSpawnTime = !isSpawnTime;
        enemySpawner.IsSpawn = false;
        sec = restTime;
        maxSec = restTime;
        onRestTime?.Invoke();
    }

    public void SetTimer(bool _value) { isTimerRunning = _value; }
    public virtual float GetSec() { return sec; }
    public void SetSec(float _value) { sec = _value; }
}

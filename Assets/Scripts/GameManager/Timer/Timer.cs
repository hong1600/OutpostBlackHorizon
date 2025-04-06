using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timer : MonoBehaviour
{
    public event Action onTimeEvent;
    public event Action onRestTime;

    GameState gameState;
    Round round;
    EnemySpawner enemySpawner;
    WaveBossSpawner waveBossSpawner;

    public float sec;
    public float maxSec { get; private set; }
    bool isTimerRunning = false;
    bool isSpawnTime = false;

    [SerializeField] float spawnTime = 30;
    [SerializeField] float restTime = 30;

    private void Start()
    {
        gameState = GameManager.instance.GameState;
        round = GameManager.instance.Round;
        enemySpawner = EnemyManager.instance.EnemySpawner;
        waveBossSpawner = EnemyManager.instance.WaveBossSpawner;

        enemySpawner.IsSpawn = false;
        sec = 120f;
        maxSec = sec;

        isTimerRunning = true;
        StartCoroutine(StartTimerLoop());
    }

    IEnumerator StartTimerLoop()
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
    private void RunTimer()
    {
        if (round.isBossRound)
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
            round.curRound++;
            round.RoundCheck();

            if(isSpawnTime == false) 
            {
                if (round.curRound == 1)
                {
                    Invoke(nameof(ChangeSpawnTime), 8);
                }
                else
                {
                    ChangeSpawnTime();
                }
            }
            else
            {
                ChangeRestTime();
            }

            isSpawnTime = !isSpawnTime;
        }
    }

    private void ChangeSpawnTime()
    {
        enemySpawner.IsSpawn = true;
        sec = spawnTime;
        maxSec = spawnTime;
    }

    private void ChangeRestTime()
    {
        enemySpawner.IsSpawn = false;
        sec = restTime;
        maxSec = restTime;
        onRestTime?.Invoke();
    }

    public void SetTimer(bool _value) { isTimerRunning = _value; }
    public float GetSec() { return sec; }
    public void SetSec(float _value) { sec = _value; }
}

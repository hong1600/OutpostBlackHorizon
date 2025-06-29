using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TimerSync : Timer, IOnEventCallback
{
    private const byte TIMER_UPDATE_EVENT = 7;
    private const byte REST_TIME_EVENT = 8;
    private const byte SPAWN_TIME_EVENT = 9;

    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    protected override void Start()
    {
        base.Start();
        StartCoroutine(SendTimeEvent());
    }

    protected override IEnumerator StartTimerLoop()
    {
        while (gameState.GetGameState() == EGameState.PLAYING)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                if (isTimerRunning)
                {
                    RunTimer();
                }
            }

            yield return null;
        }
    }

    protected override void RunTimer()
    {
        if (round.IsBossRound)
        {
            isTimerRunning = false;
        }
        else
        {
            sec -= Time.deltaTime;
            sec = Mathf.Max(0f, sec);
            OnTimeEvent();
        }

        if (sec <= 0f)
        {
            enemySpawner.IsSpawn = false;

            RaiseEventOptions options = new RaiseEventOptions { Receivers = ReceiverGroup.Others };

            if (!isSpawnTime)
            {
                if (round.CurRound == 0)
                {
                    isTimerRunning = false;
                    ChangeSpawnTime();

                    PhotonNetwork.RaiseEvent(SPAWN_TIME_EVENT, null, options, SendOptions.SendUnreliable);
                }
                else
                {
                    ChangeSpawnTime();

                    PhotonNetwork.RaiseEvent(SPAWN_TIME_EVENT, null, options, SendOptions.SendUnreliable);
                }
            }
            else if (isSpawnTime && enemyManager.GetCurEnemy() <= 0)
            {
                ChangeRestTime();

                PhotonNetwork.RaiseEvent(REST_TIME_EVENT, null, options, SendOptions.SendUnreliable);
            }
        }
    }

    IEnumerator SendTimeEvent()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                object[] content = new object[] { sec };
                RaiseEventOptions options = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
                PhotonNetwork.RaiseEvent(TIMER_UPDATE_EVENT, content, options, SendOptions.SendUnreliable);
            }

            yield return wait;
        }
    }

    public void OnEvent(EventData _photonEvent)
    {
        if (_photonEvent.Code == TIMER_UPDATE_EVENT)
        {
            if (PhotonNetwork.IsMasterClient) return;

            object[] data = (object[])_photonEvent.CustomData;

            sec = (float)data[0];
            OnTimeEvent();
        }
        else if (_photonEvent.Code == SPAWN_TIME_EVENT)
        {
            ChangeSpawnTime();
        }
        else if (_photonEvent.Code == REST_TIME_EVENT)
        {
            ChangeRestTime();
        }
    }
}

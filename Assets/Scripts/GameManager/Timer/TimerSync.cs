using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TimerSync : Timer, IOnEventCallback
{
    float lastSyncSec = 0;
    float lastSyncTime = 0;

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
            OnTimeEvent(sec);
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

                    PhotonNetwork.RaiseEvent(PhotonEventCode.SPAWN_TIME_EVENT, null, options, SendOptions.SendUnreliable);
                }
                else
                {
                    ChangeSpawnTime();

                    PhotonNetwork.RaiseEvent(PhotonEventCode.SPAWN_TIME_EVENT, null, options, SendOptions.SendUnreliable);
                }
            }
            else if (isSpawnTime && enemyManager.GetCurEnemy() <= 0)
            {
                ChangeRestTime();

                PhotonNetwork.RaiseEvent(PhotonEventCode.REST_TIME_EVENT, null, options, SendOptions.SendUnreliable);
            }
        }
    }

    IEnumerator SendTimeEvent()
    {
        WaitForSeconds wait = new WaitForSeconds(0.05f);

        while (true)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                object[] content = new object[] { sec };
                RaiseEventOptions options = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
                PhotonNetwork.RaiseEvent(PhotonEventCode.TIMER_UPDATE_EVENT, content, options, SendOptions.SendUnreliable);
            }

            yield return wait;
        }
    }

    public void OnEvent(EventData _photonEvent)
    {
        if (_photonEvent.Code == PhotonEventCode.TIMER_UPDATE_EVENT)
        {
            if (PhotonNetwork.IsMasterClient) return;

            object[] data = (object[])_photonEvent.CustomData;
            lastSyncSec = (float)data[0];
            lastSyncTime = Time.time;

            OnTimeEvent(lastSyncSec);
        }
        else if (_photonEvent.Code == PhotonEventCode.SPAWN_TIME_EVENT)
        {
            ChangeSpawnTime();
        }
        else if (_photonEvent.Code == PhotonEventCode.REST_TIME_EVENT)
        {
            ChangeRestTime();
        }
    }

    public override float GetSec()
    {
        if (PhotonNetwork.IsMasterClient) return sec;

        float interpolated = lastSyncSec - (Time.time - lastSyncTime);
        return Mathf.Max(0f, interpolated);

    }
}

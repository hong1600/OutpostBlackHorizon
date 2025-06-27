using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerSync : Timer, IOnEventCallback
{
    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
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

                object[] content = new object[] { sec };
                RaiseEventOptions options = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
                PhotonNetwork.RaiseEvent(100, content, options, SendOptions.SendUnreliable);
            }

            yield return null;
        }
    }
    public void OnEvent(EventData _photonEvent)
    {
        if (_photonEvent.Code == 100)
        {
            if (PhotonNetwork.IsMasterClient) return;

            object[] data = (object[])_photonEvent.CustomData;

            sec = (float)data[0];
            OnTimeEvent();
        }
    }
}

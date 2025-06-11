using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonChat : MonoBehaviour, IOnEventCallback
{
    const byte GAME_CHAT_EVENT = 100;

    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public void SendChat(string _sender, string _message)
    {
        object[] content = new object[] { _sender, _message };
        RaiseEventOptions options = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        SendOptions sendOptions = new SendOptions { Reliability = true };

        PhotonNetwork.RaiseEvent(GAME_CHAT_EVENT, content, options, sendOptions);
    }

    public void OnEvent(EventData _photonEvent)
    {
        if (_photonEvent.Code == GAME_CHAT_EVENT)
        {
            object[] data = (object[])_photonEvent.CustomData;
            string sender = (string)data[0];
            string message = (string)data[1];

            GameUI.instance.UIGameChat.DisplayMessage(sender, message);
        }
    }
}

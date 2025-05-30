using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PhotonManager : MonoBehaviourPunCallbacks
{
    public void CreateLobbyRoom(string _Room)
    {
        if (_Room == null)
            return;

        PhotonNetwork.CreateRoom(_Room);
    }

    public void RandomLobbyRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void JoinLobbyRoom(string _Room)
    {
        if(_Room == null)
            return;

        PhotonNetwork.JoinRoom(_Room);
    }

    public void LeaveRoom(bool _Com)
    {
        PhotonNetwork.LeaveRoom(_Com);
    }

    public void LeaveLobby()
    {
        PhotonNetwork.LeaveLobby();
    }

    public void SecretLobbyRoom(string _Room, byte _Secret, byte _MaxPlayer)
    {
        if(_Room == null)
            return;

        bool open = _Secret > 0 ? false : true;

        RoomOptions option = new RoomOptions()
        {
            IsVisible = open,
            MaxPlayers = _MaxPlayer
        };

        if (option == null)
            return;

        PhotonNetwork.JoinOrCreateRoom(_Room, option, null);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);

        foreach (RoomInfo room in roomList)
        {
        }
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
    }

    [PunRPC]
    public void SendRoomEntry()
    {
        PV.RPC("LobbyRoomEntry", RpcTarget.All, true);
    }

    public void LobbyRoomEntry(bool _Entry)
    {
    }

    [PunRPC]
    public void SendRoomReady()
    {
        PV.RPC("LobbyRoomReady", RpcTarget.All);
    }

    [PunRPC]
    public void SendStartInGame()
    {
        PV.RPC("StartInGame", RpcTarget.All);
    }
}

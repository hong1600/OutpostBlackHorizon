using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System;

public class PhotonMatching : MonoBehaviourPunCallbacks, IOnEventCallback
{
    const byte MATCHING_GAME_EVENT = 1;
    const byte LOAD_COMPLETE_EVENT = 2;
    const byte START_GAME_EVENT = 3;
    const byte ARRIVE_DROPSHIP_EVENT = 4;
    const byte SPAWN_PLAYER_EVENT = 5;

    int loadPlayerCount = 0;
    int arrivePlayerCount = 0;

    private void Awake()
    {
        PhotonNetwork.GameVersion = "1.0.0";
        PhotonNetwork.SendRate = 30;
        PhotonNetwork.SerializationRate = 10;

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
        base.OnEnable();
    }

    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
        base.OnDisable();
    }

    //����Ǹ� �κ� ����
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        PhotonNetwork.JoinLobby();
    }

    //�κ� �����Ǹ� üũ
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("�κ�����");
    }

    //��Īť ����
    public void StartMatching()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    //�� ���� �� ����
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        string id = Guid.NewGuid().ToString();

        string roomName = "Room"+ id;

        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 2;

        PhotonNetwork.CreateRoom(roomName, options);

        base.OnJoinRandomFailed(returnCode, message);
    }

    //��Ī ��� �� �κ�� ���ư���
    public void CancleMatching()
    {
        if(PhotonNetwork.InRoom) 
        {
            PhotonNetwork.LeaveRoom();
        }
        else if(PhotonNetwork.IsConnectedAndReady && !PhotonNetwork.InLobby) 
        {
            PhotonNetwork.JoinLobby();
        }
    }

    //�� ���� üũ �� ���� �� ���Ӿ� �̵�
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);

        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            if (PhotonNetwork.IsMasterClient) 
            {
                RaiseEventOptions options = new RaiseEventOptions();
                options.Receivers = ReceiverGroup.All;

                SendOptions sendOptions = new SendOptions();
                sendOptions.Reliability = true;

                PhotonNetwork.RaiseEvent(MATCHING_GAME_EVENT, null, options, sendOptions);
            }
        }
    }

    //���ӽ���
    IEnumerator StartGame()
    {
        MatchingUI.instance.CompleteMatch();
        LoadingScene.SetNextScene(EScene.MULTIGAME);
        GameModeManager.instance.ChangeGameMode(EGameMode.MULTI);

        yield return new WaitForSeconds(1f);

        MSceneManager.Instance.ChangeScene(EScene.LOADING);
    }

    public void OnEvent(EventData _photonEvent)
    {
        if (_photonEvent.Code == MATCHING_GAME_EVENT)
        {
            StartCoroutine(StartGame());
        }
        else if (_photonEvent.Code == LOAD_COMPLETE_EVENT)
        {
            loadPlayerCount++;

            if(PhotonNetwork.IsMasterClient && loadPlayerCount >= PhotonNetwork.CurrentRoom.PlayerCount) 
            {
                RaiseEventOptions options = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                SendOptions sendOptions = new SendOptions { Reliability = true };
                PhotonNetwork.RaiseEvent(START_GAME_EVENT, null, options, sendOptions);
            }
        }
        else if(_photonEvent.Code == START_GAME_EVENT) 
        {
            LoadingScene.AllowSceneActivation();
        }
        else if (_photonEvent.Code == ARRIVE_DROPSHIP_EVENT)
        {
            arrivePlayerCount++;

            Debug.Log(arrivePlayerCount);

            if (PhotonNetwork.IsMasterClient && arrivePlayerCount >= PhotonNetwork.CurrentRoom.PlayerCount)
            {
                StartCoroutine(SendSpawnPlayerEvents());
            }
        }
        else if (_photonEvent.Code == SPAWN_PLAYER_EVENT)
        {
            int actorNum = (int)_photonEvent.CustomData;

            StartCoroutine(DelayedSpawnPlayer(actorNum));
        }
    }

    IEnumerator SendSpawnPlayerEvents()
    {
        yield return new WaitForSeconds(2f);

        SendOptions sendOptions = new SendOptions { Reliability = true };

        for(int i = 0; i < PhotonNetwork.PlayerList.Length; i++) 
        {
            var p = PhotonNetwork.PlayerList[i];
            int actorNum = p.ActorNumber;
            var options = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            PhotonNetwork.RaiseEvent(SPAWN_PLAYER_EVENT, actorNum, options, new SendOptions { Reliability = true });

            yield return new WaitForSeconds(0.1f);
        }
    }

    public void NotifySceneLoaded()
    {
        RaiseEventOptions options = new RaiseEventOptions { Receivers = ReceiverGroup.MasterClient };
        SendOptions sendOptions = new SendOptions { Reliability = true };
        PhotonNetwork.RaiseEvent(LOAD_COMPLETE_EVENT, null, options, sendOptions);
    }

    public void ArriveDropship()
    {
        RaiseEventOptions options = new RaiseEventOptions{Receivers = ReceiverGroup.MasterClient};
        SendOptions sendOptions = new SendOptions { Reliability = true };
        PhotonNetwork.RaiseEvent(ARRIVE_DROPSHIP_EVENT, null, options, sendOptions);
    }

    IEnumerator DelayedSpawnPlayer(int _actorNum)
    {
        while (PhotonNetwork.NetworkClientState != ClientState.Joined)
        {
            yield return null;
        }

        yield return null;

        if (PhotonNetwork.LocalPlayer.ActorNumber == _actorNum)
        {
            GameManager.instance.PlayerSpawner.SpawnPlayer();
        }
    }
}

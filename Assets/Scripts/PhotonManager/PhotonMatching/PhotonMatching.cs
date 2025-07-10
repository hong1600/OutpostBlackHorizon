using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System;

public class PhotonMatching : MonoBehaviourPunCallbacks, IOnEventCallback
{
    int loadPlayerCount = 0;
    int arrivePlayerCount = 0;

    private void Awake()
    {
        PhotonNetwork.GameVersion = "1.0.0";
        PhotonNetwork.SendRate = 30;
        PhotonNetwork.SerializationRate = 10;

        PhotonNetwork.ConnectUsingSettings();

        PhotonPeer.RegisterType(
            typeof(EnemySyncData),
            PhotonCustomTypes.EnemySyncDataCode,
            PhotonCustomTypes.SerializeEnemySyncData,
            PhotonCustomTypes.DeserializeEnemySyncData
            );

        PhotonPeer.RegisterType(
            typeof(BulletSyncData),
            PhotonCustomTypes.BulletSyncDataCode,
            PhotonCustomTypes.SerializeBulletSyncData,
            PhotonCustomTypes.DeserializeBulletSyncData
            );
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

    //연결되면 로비 참가
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        PhotonNetwork.JoinLobby();
    }

    //로비 참가되면 체크
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("로비참가");
    }

    //매칭큐 시작
    public void StartMatching()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    //방 없을 시 생성
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        string id = Guid.NewGuid().ToString();

        string roomName = "Room"+ id;

        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 2;

        PhotonNetwork.CreateRoom(roomName, options);

        base.OnJoinRandomFailed(returnCode, message);
    }

    //매칭 취소 시 로비로 돌아가기
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

    //방 정원 체크 및 정원 시 게임씬 이동
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

                PhotonNetwork.RaiseEvent(PhotonEventCode.MATCHING_GAME_EVENT, null, options, sendOptions);
            }
        }
    }

    //게임시작
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
        if (_photonEvent.Code == PhotonEventCode.MATCHING_GAME_EVENT)
        {
            StartCoroutine(StartGame());
        }
        else if (_photonEvent.Code == PhotonEventCode.LOAD_COMPLETE_EVENT)
        {
            loadPlayerCount++;

            if(PhotonNetwork.IsMasterClient && loadPlayerCount >= PhotonNetwork.CurrentRoom.PlayerCount) 
            {
                RaiseEventOptions options = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                SendOptions sendOptions = new SendOptions { Reliability = true };
                PhotonNetwork.RaiseEvent(PhotonEventCode.START_GAME_EVENT, null, options, sendOptions);
            }
        }
        else if(_photonEvent.Code == PhotonEventCode.START_GAME_EVENT) 
        {
            LoadingScene.AllowSceneActivation();
        }
        else if (_photonEvent.Code == PhotonEventCode.ARRIVE_DROPSHIP_EVENT)
        {
            arrivePlayerCount++;

            Debug.Log(arrivePlayerCount);

            if (PhotonNetwork.IsMasterClient && arrivePlayerCount >= PhotonNetwork.CurrentRoom.PlayerCount)
            {
                StartCoroutine(SendSpawnPlayerEvents());
            }
        }
        else if (_photonEvent.Code == PhotonEventCode.SPAWN_PLAYER_EVENT)
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
            PhotonNetwork.RaiseEvent(PhotonEventCode.SPAWN_PLAYER_EVENT, actorNum, options, new SendOptions { Reliability = true });

            yield return new WaitForSeconds(0.1f);
        }
    }

    public void NotifySceneLoaded()
    {
        RaiseEventOptions options = new RaiseEventOptions { Receivers = ReceiverGroup.MasterClient };
        SendOptions sendOptions = new SendOptions { Reliability = true };
        PhotonNetwork.RaiseEvent(PhotonEventCode.LOAD_COMPLETE_EVENT, null, options, sendOptions);
    }

    public void ArriveDropship()
    {
        RaiseEventOptions options = new RaiseEventOptions{Receivers = ReceiverGroup.MasterClient};
        SendOptions sendOptions = new SendOptions { Reliability = true };
        PhotonNetwork.RaiseEvent(PhotonEventCode.ARRIVE_DROPSHIP_EVENT, null, options, sendOptions);
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

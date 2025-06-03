using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;

public partial class PhotonManager : MonoBehaviourPunCallbacks
{
    public static PhotonManager instance;

    //기본설정
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        PhotonNetwork.GameVersion = "1.0.0";
        PhotonNetwork.SendRate = 30;
        PhotonNetwork.SerializationRate = 10;
        PhotonNetwork.AutomaticallySyncScene = false;

        PhotonNetwork.ConnectUsingSettings();
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
        Debug.Log("매칭큐시작");
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
        Debug.Log("방생성");
    }

    //매칭 취소 시 로비로 돌아가기
    public void CancleMatching()
    {
        if(PhotonNetwork.InRoom) 
        {
            PhotonNetwork.LeaveRoom();
            Debug.Log("방 떠나기");
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
            StartCoroutine(StartGame());
        }
    }

    //게임시작
    IEnumerator StartGame()
    {
        MatchingUI.instance.CompleteMatch();

        yield return new WaitForSeconds(1f);

        if(PhotonNetwork.IsMasterClient) 
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            GameModeManager.instance.ChangeGameMode(EGameMode.MULTI);
            PhotonNetwork.LoadLevel("Loading");
            Debug.Log("게임시작");
        }
    }
}

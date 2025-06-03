using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;

public partial class PhotonManager : MonoBehaviourPunCallbacks
{
    public static PhotonManager instance;

    //�⺻����
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
        Debug.Log("��Īť����");
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
        Debug.Log("�����");
    }

    //��Ī ��� �� �κ�� ���ư���
    public void CancleMatching()
    {
        if(PhotonNetwork.InRoom) 
        {
            PhotonNetwork.LeaveRoom();
            Debug.Log("�� ������");
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
            StartCoroutine(StartGame());
        }
    }

    //���ӽ���
    IEnumerator StartGame()
    {
        MatchingUI.instance.CompleteMatch();

        yield return new WaitForSeconds(1f);

        if(PhotonNetwork.IsMasterClient) 
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            GameModeManager.instance.ChangeGameMode(EGameMode.MULTI);
            PhotonNetwork.LoadLevel("Loading");
            Debug.Log("���ӽ���");
        }
    }
}

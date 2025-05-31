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
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        PhotonNetwork.GameVersion = "1.0.0";
        PhotonNetwork.SendRate = 30;
        PhotonNetwork.SerializationRate = 10;
        PhotonNetwork.AutomaticallySyncScene = true;

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
        Debug.Log("�κ� ����");
    }

    //��Īť ����
    public void StartMatching()
    {
        PhotonNetwork.JoinRandomRoom();
        Debug.Log("��Ī ����");
    }

    //�� ���� �� ����
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("�� ���� ���� ����");

        string id = Guid.NewGuid().ToString();

        string roomName = "Room"+ id;

        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 2;

        PhotonNetwork.CreateRoom(roomName, options);

        Debug.Log("�� ����");

        base.OnJoinRandomFailed(returnCode, message);
    }

    //��Ī ��� �� �κ�� ���ư���
    public void CancleMatching()
    {
        if(PhotonNetwork.InRoom) 
        {
            PhotonNetwork.LeaveRoom();

            Debug.Log("�� ����");
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

        Debug.Log("���ο� �÷��̾� ����");

        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            StartGame();
        }
    }

    //���ӽ���
    private void StartGame()
    {
        if(PhotonNetwork.IsMasterClient) 
        {
            PhotonNetwork.LoadLevel("Game");
        }
    }
}

using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            CheckPlayer();
        }
    }

    private void CheckPlayer()
    {
        foreach (var player in PhotonNetwork.CurrentRoom.Players)
        {
            Debug.Log($"플레이어 ID: {player.Value.ActorNumber}, 닉네임: {player.Value.NickName}");
        }
    }
}

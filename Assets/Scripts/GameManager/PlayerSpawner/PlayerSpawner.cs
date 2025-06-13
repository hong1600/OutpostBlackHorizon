using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] GameObject playerPref;

    [SerializeField] Transform singlePlayerTrs;
    [SerializeField] Transform[] mutiPlayerTrs;

    public GameObject player { get; private set; }

    private void Awake()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        if (GameModeManager.instance.eGameMode == EGameMode.SINGLE) 
        {
            player = Instantiate(playerPref, singlePlayerTrs.position, Quaternion.identity);
        }
        else if(GameModeManager.instance.eGameMode == EGameMode.MULTI)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                player = PhotonNetwork.Instantiate("Prefabs/Obj/Player/MultiPlayer", mutiPlayerTrs[0].transform.position, Quaternion.identity);
            }
            else
            {
                player = PhotonNetwork.Instantiate("Prefabs/Obj/Player/MultiPlayer", mutiPlayerTrs[1].transform.position, Quaternion.identity);
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawner : MonoBehaviour
{
    public event Action onSpawnPlayer;

    [SerializeField] GameObject playerPref;

    [SerializeField] Transform singlePlayerTrs;
    [SerializeField] Transform[] multiPlayerTrs;

    public GameObject player;

    public void SpawnPlayer()
    {
        if (GameModeManager.instance.eGameMode == EGameMode.SINGLE) 
        {
            player = Instantiate(playerPref, singlePlayerTrs.position, Quaternion.identity);
        }
        else if(GameModeManager.instance.eGameMode == EGameMode.MULTI)
        {
            int index = PhotonNetwork.LocalPlayer.ActorNumber - 1;
            Vector3 spawnPos = multiPlayerTrs[index].position;

            player = PhotonNetwork.Instantiate("Prefabs/Obj/Player/MultiPlayer", spawnPos, Quaternion.Euler(0, -125, 0));
        }

        StartCoroutine(StartInitPlayer());
    }

    IEnumerator StartInitPlayer()
    {
        onSpawnPlayer?.Invoke();

        yield return new WaitForSeconds(0.5f);

        if (player != null)
        {
            GameManager.instance.PlayerSpawner.player.GetComponent<PlayerCombat>().enabled = false;
            GameManager.instance.PlayerSpawner.player.GetComponent<PlayerMovement>().enabled = false;

            StartDropshipCut dropship = FindObjectOfType<StartDropshipCut>();
            dropship.WaitingSpawn();
        }
    }
}

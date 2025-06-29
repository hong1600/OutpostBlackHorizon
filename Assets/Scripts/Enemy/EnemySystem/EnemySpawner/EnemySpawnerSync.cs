using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawnerSync : EnemySpawner, IOnEventCallback
{
    const byte ENEMY_SPAWN_EVENT = 10;

    protected override void SpawnEnemy()
    {
        if (!PhotonNetwork.IsMasterClient) return;
        base.SpawnEnemy();
    }

    protected override IEnumerator StartSpawn(int _spawnPoint)
    {
        for (int i = 0; i < 3; i++)
        {
            EEnemy eEnemy = (EEnemy)Random.Range(0, 4);

            Vector3[] spawnPos = new Vector3[4];
            Vector3 basePos = enemySpawnPointList[_spawnPoint].transform.position;

            for (int j = 0; j < 4; j++)
            {
                spawnPos[j] = basePos + (enemySpawnPos[j]);
                spawnPos[j].y = terrain.SampleHeight(spawnPos[j]);
            }

            for (int j = 0; j < 3; j++)
            {
                enemyFactory.Create(eEnemy, spawnPos[j], Quaternion.identity, null, null);

                object[] data = new object[] { (int)eEnemy, spawnPos[j].x, spawnPos[j].y, spawnPos[j].z };
                RaiseEventOptions options = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
                PhotonNetwork.RaiseEvent(ENEMY_SPAWN_EVENT, data, options);
            }

            yield return new WaitForSeconds(2f);
        }

    }

    public void OnEvent(EventData _photonEvent)
    {
        if (_photonEvent.Code == ENEMY_SPAWN_EVENT)
        {
            object[] data = (object[])_photonEvent.CustomData;

            EEnemy type = (EEnemy)(int)data[0];
            Vector3 pos = new Vector3((float)data[1], (float)data[2], (float)data[3]);

            enemyFactory.Create(type, pos, Quaternion.identity, null, null);
        }
    }
}

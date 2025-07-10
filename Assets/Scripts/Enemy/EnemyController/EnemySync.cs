using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySync : MonoBehaviour, IOnEventCallback
{
    EnemyView enemyView;
    EnemyBase enemyBase;

    Vector3 pos;
    Quaternion rot;

    public int ID{ get; private set; }

    private void Awake()
    {
        enemyView = GetComponent<EnemyView>();
    }

    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        this.enabled = false;
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, pos, Time.fixedDeltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.fixedDeltaTime);
    }

    public void Init(int _id)
    {
        ID = _id;
    }

    public void OnEvent(EventData _photonEvent)
    {
        if (_photonEvent.Code == PhotonEventCode.ENEMY_SYNC_EVENT)
        {
            EnemySyncData data = (EnemySyncData)_photonEvent.CustomData;

            if (data != null && data.id == this.ID)
            {
                MoveSync(data.pos, data.rot);
            }
        }
        else if (_photonEvent.Code == PhotonEventCode.ENEMY_STATE_EVENT)
        {
            object[] data = (object[])_photonEvent.CustomData;

            int aiState = (int)data[0];
            int id = (int)data[1];

            if (data != null && id == this.ID)
            {
                ChangeAnim(aiState);
            }
        }
        
    }

    private void MoveSync(Vector3 _pos, Quaternion _rot)
    {
        pos = _pos;
        rot = _rot;
    }

    private void ChangeAnim(int _aiState)
    {
        EEnemyAI curState = (EEnemyAI)_aiState;

        enemyView.ChangeAnim(curState);
    }
}

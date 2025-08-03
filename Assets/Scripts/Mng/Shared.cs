using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shared : MonoBehaviour
{
    public static Shared Instance;

    public IObjectPoolManager poolManager;

    private void Awake()
    {
        Instance = this;

        if (PhotonNetwork.InRoom)
        {
            poolManager = ObjectPoolManagerSync.instance;
        }
        else
        {
            poolManager = ObjectPoolManager.instance;
        }
    }
}

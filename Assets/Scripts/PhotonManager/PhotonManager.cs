using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonManager : Singleton<PhotonManager>
{
    [SerializeField] PhotonChat photonChat;
    [SerializeField] PhotonMatching photonMatching;

    public PhotonChat PhotonChat { get {return photonChat; } }
    public PhotonMatching PhotonMaching { get {return photonMatching; } }

    protected override void Awake()
    {
        base.Awake();
    }

}

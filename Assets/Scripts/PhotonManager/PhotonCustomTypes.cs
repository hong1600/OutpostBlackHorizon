using ExitGames.Client.Photon;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

[Serializable]
public class EnemySyncData
{
    public int id;
    public Vector3 pos;
    public Quaternion rot;
}

public static class PhotonCustomTypes
{
    public static readonly byte EnemySyncDataCode = (byte)'E';

    public static byte[] SerializeEnemySyncData(object obj)
    {
        EnemySyncData data = (EnemySyncData)obj;
        byte[] bytes = new byte[sizeof(int) + sizeof(float) * 7];

        int index = 0;

        Protocol.Serialize(data.id, bytes, ref index);

        Protocol.Serialize(data.pos.x, bytes, ref index);
        Protocol.Serialize(data.pos.y, bytes, ref index);
        Protocol.Serialize(data.pos.z, bytes, ref index);

        Protocol.Serialize(data.rot.x, bytes, ref index);
        Protocol.Serialize(data.rot.y, bytes, ref index);
        Protocol.Serialize(data.rot.z, bytes, ref index);
        Protocol.Serialize(data.rot.w, bytes, ref index);

        return bytes;
    }

    public static object DeserializeEnemySyncData(byte[] bytes)
    {
        EnemySyncData data = new EnemySyncData();

        int index = 0;

        Protocol.Deserialize(out data.id, bytes, ref index);

        Protocol.Deserialize(out data.pos.x, bytes, ref index);
        Protocol.Deserialize(out data.pos.y, bytes, ref index);
        Protocol.Deserialize(out data.pos.z, bytes, ref index);

        Protocol.Deserialize(out data.rot.x, bytes, ref index);
        Protocol.Deserialize(out data.rot.y, bytes, ref index);
        Protocol.Deserialize(out data.rot.z, bytes, ref index);
        Protocol.Deserialize(out data.rot.w, bytes, ref index);

        return data;
    }
}

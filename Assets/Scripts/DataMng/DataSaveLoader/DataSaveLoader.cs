using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataSaveLoader
{
    public string path;

    public DataSaveLoader() 
    {
        path = Application.persistentDataPath + "/save.json";
    }

    public void SaveData()
    {
        string data = JsonUtility.ToJson(DataMng.instance.playerData, true);
        File.WriteAllText(path, data);
    }

    public void LoadData()
    {
        if (File.Exists(path))
        {
            string data = File.ReadAllText(path);
            DataMng.instance.playerData = JsonUtility.FromJson<PlayerData>(data);
            DataMng.instance.playerDataLoader.Initialize();
        }
        else
        {
            DataMng.instance.playerDataLoader.Initialize();
        }
    }
}

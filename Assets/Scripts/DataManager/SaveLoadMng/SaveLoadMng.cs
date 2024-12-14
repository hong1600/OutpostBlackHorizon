using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadMng
{
    public string path;

    public SaveLoadMng() 
    {
        path = Application.persistentDataPath + "/save.json";
    }

    public void saveData()
    {
        string data = JsonUtility.ToJson(DataManager.instance.playerdata, true);
        File.WriteAllText(path, data);
    }

    public void loadData()
    {
        if (File.Exists(path))
        {
            string data = File.ReadAllText(path);
            DataManager.instance.playerdata = JsonUtility.FromJson<PlayerData>(data);
            DataManager.instance.playerDataMng.initialized();
        }
        else
        {
            DataManager.instance.playerDataMng.initialized();
        }
    }
}

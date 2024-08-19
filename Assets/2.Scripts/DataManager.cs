using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerData
{
    public string name = "";
    public int level = 1;
    public float curExp = 0f;
    public float maxExp = 100f;
    public int gold = 0;
    public int gem = 0;
    public bool first = true;
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public PlayerData playerdata = new PlayerData();

    public string path;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        path = Application.persistentDataPath + "/save";
    }

    public void saveData()
    {
        string data = JsonUtility.ToJson(playerdata);
        File.WriteAllText(path, data);
    }

    public void loadData()
    {
        string data = File.ReadAllText(path);
        playerdata = JsonUtility.FromJson<PlayerData>(data);
    }
}
